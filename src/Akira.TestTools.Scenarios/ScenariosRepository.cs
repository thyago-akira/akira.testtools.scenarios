using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;
using Akira.TestTools.Scenarios.Collections;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;
using Akira.TestTools.Scenarios.Models;

namespace Akira.TestTools.Scenarios
{
    public class ScenariosRepository<T> : IScenariosRepository<T>
        where T : class
    {
        #region Fields

        private readonly IContextSet scenarioContextSet =
            new ContextSet();

        private readonly IScenarioActionSet<T> scenarioActionSet =
            new CachedScenarioActionSet<T, InternalFaker<T>>();

        private readonly IKnownCombinationSet knownCombinationSet =
            new KnownCombinationSet();

        private Random random;

        #endregion Fields

        #region Constructors

        public ScenariosRepository()
        {
            this.scenarioContextSet.AddContext(Defaults.ContextName);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of distinct possible <see cref="ICompletedModelBuilder<T>"/> for the current <see cref="ScenariosRepository{T}" />
        /// </summary>
        public ulong CountModelBuilders => this.scenarioContextSet.CountScenariosCombinations;

        private Random Random
        {
            get
            {
                if (this.random == null)
                {
                    this.random = new Random();
                }

                return this.random;
            }
        }

        #endregion Properties

        #region Methods

        #region Public Methods

        /// <summary>
        /// Add a new Scenario Context, initializing all the information related to it
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        public void AddContext(string scenarioContextName) =>
            this.scenarioContextSet.AddContext(scenarioContextName);

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="hasDefaultScenarioContext">Indicates if the method was called by a Default Scenario Context (true) or a Custom Scenario Context (false)</param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioType.Unknown"/>, <see cref="ScenarioType.AlwaysValid"/> or <see cref="ScenarioType.AlwaysInvalid"/>
        /// </param>
        /// <param name="scenarioAction">The actions that will be executed to the current Scenario</param>
        public void AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            Action<IScenarioRuleSet<T>> scenarioAction,
            ScenarioType scenarioType = ScenarioType.Unknown)
        {
            this.scenarioActionSet.ValidateScenarioAction(
                scenarioAction);

            var scenario = this.scenarioContextSet.AddScenario(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            this.scenarioActionSet.AddValidScenarioAction(
                scenario.Key,
                scenarioAction);

            this.knownCombinationSet.AddScenario(
                scenario,
                scenarioType);
        }

        /// <summary>
        /// Add a Known Valid Scenario Combination to the <see cref="ScenariosRepository{T}" />
        /// </summary>
        /// <param name="combination">
        /// A dictionary with the Known Scenario Combination Configuration that can used to build a model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <param name="combinationType">
        /// Indicates if the current Known Scenario Combination Configuration will be
        /// <see cref="ScenarioType.Unknown"/>, <see cref="ScenarioType.AlwaysValid"/> or
        /// <see cref="ScenarioType.AlwaysInvalid"/>
        /// </param>
        public void AddKnownCombination(
            IDictionary<string, string> combination,
            ScenarioType combinationType = ScenarioType.Unknown)
        {
            var knownCombination = this.ValidateKnownCombinationConfiguration(
                combinationType,
                combination,
                this.scenarioContextSet.CurrentScenarioContextIsDefault);

            this.knownCombinationSet.AddKnownCombination(
                knownCombination);
        }

        public IEnumerable<IModelBuilder<T>> GetMinimumTestingModelBuilders(
            BuilderType scenarioType = BuilderType.All)
        {
            if (scenarioType == BuilderType.All)
            {
                yield return this.GetModelBuilder(
                    scenarioType,
                    null,
                    false);

                yield return this.GetModelBuilder(
                    scenarioType,
                    new Dictionary<string, string>(),
                    false);
            }

            // You should test all known scenarios
            foreach (var knownCombination in this.knownCombinationSet)
            {
                if (scenarioType == BuilderType.All ||
                   (scenarioType == BuilderType.ValidOnly &&
                    knownCombination.CombinationType == ScenarioType.AlwaysValid) ||
                   (scenarioType == BuilderType.InvalidOnly &&
                    knownCombination.CombinationType == ScenarioType.AlwaysInvalid))
                {
                    yield return this.GetModelBuilder(
                        scenarioType,
                        knownCombination.Combination,
                        false);
                }
            }
        }

        public IModelBuilder<T> GetModelBuilder(
            BuilderType scenarioBuilderType,
            IDictionary<string, string> scenarioCombinationConfiguration) =>
            this.GetModelBuilder(
                scenarioBuilderType,
                scenarioCombinationConfiguration,
                true);

        public void ValidateBuilderConfiguration(
            BuilderType scenarioBuilderType,
            ref IDictionary<string, string> scenarioBuilderConfiguration)
        {
            if (!EnumExtensions.AllowedScenarioBuilderTypes.Contains((int)scenarioBuilderType))
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderTypeInvalid);
            }

            if (scenarioBuilderType == BuilderType.ValidOnly &&
                !this.knownCombinationSet.HasAlwaysValidKnownScenario)
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderDoesnotContainAlwaysValidKnownScenario);
            }

            if (scenarioBuilderType == BuilderType.InvalidOnly &&
                !this.knownCombinationSet.HasAlwaysInvalidKnownScenario)
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderDoesnotContainAlwaysInvalidKnownScenario);
            }

            scenarioBuilderConfiguration = this.ValidateScenarioConfigurationBuilder(
                scenarioBuilderType,
                scenarioBuilderConfiguration);
        }

        #endregion Public Methods

        #region Internal Methods

        internal IEnumerable<IScenario> GetFullScenarioBuilderRules(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            this.scenarioContextSet.ValidateCurrentContextCompleted();

            var buildRules = new List<IScenario>();

            foreach (var scenarioContext in this.scenarioContextSet)
            {
                if (scenarioCombinationConfiguration.TryGetValue(
                    scenarioContext.Name,
                    out var scenarioBuilderName))
                {
                    scenarioBuilderName = scenarioBuilderName.Trim();
                }
                else
                {
                    scenarioBuilderName = string.Empty;
                }

                buildRules.Add(
                    scenarioContext.GetScenarioOrRandom(
                        scenarioBuilderName));
            }

            return buildRules;
        }

        internal IDictionary<string, string> ValidateScenarioConfigurationBuilder(
            BuilderType builderType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            this.ValidateScenarioCombinationConfigurationKeys(
                scenarioCombinationConfiguration);

            return this.ValidateCombinationConfigurationBuilderValues(
                builderType,
                scenarioCombinationConfiguration);
        }

        #endregion Internal Methods

        #region Private Methods

        private IModelBuilder<T> GetModelBuilder(
            BuilderType scenarioBuilderType,
            IDictionary<string, string> scenarioCombinationConfiguration,
            bool validateBuilderConfiguration)
        {
            if (scenarioCombinationConfiguration == null)
            {
                scenarioCombinationConfiguration = new Dictionary<string, string>();
            }

            if (validateBuilderConfiguration)
            {
                this.ValidateBuilderConfiguration(
                    scenarioBuilderType,
                    ref scenarioCombinationConfiguration);
            }

            var fullScenarioBuilderRules = this.GetFullScenarioBuilderRules(
                scenarioCombinationConfiguration);

            var modelBuilder = this.scenarioActionSet.GetModelBuilder(
                fullScenarioBuilderRules.Select(b => b.Key));

            return modelBuilder;
        }

        private KnownCombination ValidateKnownCombinationConfiguration(
            ScenarioType combinationType,
            IDictionary<string, string> scenarioCombinationConfiguration,
            bool currentIsDefaultContext)
        {
            this.ValidateKnownCombinationConfigurationBuilder(
                currentIsDefaultContext,
                scenarioCombinationConfiguration);

            this.ValidateScenarioCombinationConfigurationKeys(
                scenarioCombinationConfiguration);

            this.scenarioContextSet.ValidateCurrentContextCompleted();

            return this.ValidateKnownCombinationConfigurationBuilderValues(
                combinationType,
                scenarioCombinationConfiguration);
        }

        private void ValidateKnownCombinationConfigurationBuilder(
            bool currentIsDefaultContext,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            if (currentIsDefaultContext)
            {
                throw new ArgumentException(
                    Errors.ScenarioFakerWithNoAdditionalScenariosForKnownScenarioCombinationConfig);
            }

            if (scenarioCombinationConfiguration == null ||
                scenarioCombinationConfiguration.Count == 0)
            {
                throw new ArgumentException(
                    Errors.KnownScenarioCombinationConfigNotSet);
            }

            if (scenarioCombinationConfiguration.Count == 1)
            {
                throw new ArgumentException(
                    Errors.KnownScenarioCombinationConfigWithOnlyOneCondition);
            }
        }

        private void ValidateScenarioCombinationConfigurationKeys(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            foreach (var builderContextName in scenarioCombinationConfiguration.Keys)
            {
                if (string.IsNullOrEmpty(builderContextName) ||
                    !this.scenarioContextSet.ContainsContext(builderContextName))
                {
                    throw new ArgumentException(
                        string.Format(
                            Errors.KnownScenarioCombinationConfigWithInvalidScenarioContext,
                            builderContextName));
                }
            }
        }

        private void ValidateScenarioCombinationConfigurationValues(
            IDictionary<string, string> scenarioCombinationConfiguration,
            Action<IScenario> existingConfigurationAction,
            Action<int> missingConfigurationAction = null)
        {
            var localDictionary = new Dictionary<string, string>(
                scenarioCombinationConfiguration,
                StringComparer.OrdinalIgnoreCase);

            foreach (var scenarioContext in this.scenarioContextSet)
            {
                if (localDictionary.TryGetValue(
                    scenarioContext.Name,
                    out var builderScenarioName))
                {
                    var builderScenarioKey = scenarioContext.GetScenario(builderScenarioName);

                    if (builderScenarioKey == null)
                    {
                        throw new ArgumentException(
                            string.Format(
                                Errors.ScenarioCombinationConfigWithInvalidScenario,
                                scenarioContext.Name,
                                builderScenarioName));
                    }

                    existingConfigurationAction(builderScenarioKey);
                }
                else
                {
                    missingConfigurationAction?.Invoke(scenarioContext.Index);
                }
            }
        }

        private IDictionary<string, string> ValidateCombinationConfigurationBuilderValues(
            BuilderType scenarioCombinationType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            var parentGroupsRegex = string.Empty;

            this.ValidateScenarioCombinationConfigurationValues(
                scenarioCombinationConfiguration,
                (builderScenarioKey) =>
                {
                    if (scenarioCombinationType != BuilderType.All)
                    {
                        parentGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                            builderScenarioKey.Context.Index,
                            builderScenarioKey.Name);
                    }
                });

            if (scenarioCombinationType == BuilderType.All)
            {
                return scenarioCombinationConfiguration;
            }

            var compatiblesCombinations = this.ValidateParentScenarioCombinationBuilderConfiguration(
                scenarioCombinationType,
                parentGroupsRegex);

            return this.GetCompatibleScenarioCombinationConfiguration(
                compatiblesCombinations,
                scenarioCombinationConfiguration);
        }

        private KnownCombination ValidateKnownCombinationConfigurationBuilderValues(
            ScenarioType scenarioCombinationType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            var scenariosKeys = new List<IScenario>();
            var parentGroupsRegex = string.Empty;
            var childrenGroupsRegex = string.Empty;

            this.ValidateScenarioCombinationConfigurationValues(
                scenarioCombinationConfiguration,
                (builderScenarioKey) =>
                {
                    scenariosKeys.Add(builderScenarioKey);

                    parentGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                        builderScenarioKey.Context.Index,
                        builderScenarioKey.Name);

                    childrenGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                        builderScenarioKey.Context.Index,
                        builderScenarioKey.Name,
                        false);
                },
                (index) =>
                {
                    childrenGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                        index);
                });

            var knownCombination = new KnownCombination(
                string.Concat(scenariosKeys.Select(k => k.Key)),
                scenariosKeys.ToDictionary(k => k.Context.Name, k => k.Name),
                scenarioCombinationType);

            this.ValidateParentKnownScenarioCombinationConfiguration(
                knownCombination.Key,
                parentGroupsRegex);

            if (scenarioCombinationType != ScenarioType.Unknown)
            {
                this.ValidateChildrenKnownScenarioCombinationConfiguration(
                    childrenGroupsRegex);
            }

            return knownCombination;
        }

        private List<IKnownCombination> ValidateParentScenarioCombinationBuilderConfiguration(
            BuilderType scenarioCombinationType,
            string parentGroupsRegex)
        {
            var compatiblesCombinations = new List<IKnownCombination>();

            var parentsRegex = Keys.GetKeyRegex(parentGroupsRegex);

            var allowedCombinationType = scenarioCombinationType == BuilderType.ValidOnly
                ? ScenarioType.AlwaysValid
                : ScenarioType.AlwaysInvalid;
            var disallowedCombinationType = scenarioCombinationType == BuilderType.ValidOnly
                ? ScenarioType.AlwaysInvalid
                : ScenarioType.AlwaysValid;

            foreach (var knownCombination in this.knownCombinationSet
                .Where(kv =>
                    (string.IsNullOrWhiteSpace(parentGroupsRegex) &&
                     kv.CombinationType == allowedCombinationType) ||
                    (parentsRegex.IsMatch(kv.Key) &&
                     kv.CombinationType != ScenarioType.Unknown)))
            {
                if (knownCombination.CombinationType == disallowedCombinationType)
                {
                    throw new ArgumentException(
                        string.Format(
                            Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                            scenarioCombinationType,
                            knownCombination.Key,
                            knownCombination.CombinationType));
                }
                else
                {
                    compatiblesCombinations.Add(knownCombination);
                }
            }

            if (compatiblesCombinations.Count == 0)
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioCombinationConfigHasNoCompatibleKnownScenarioCombinationConfig,
                        scenarioCombinationType));
            }

            return compatiblesCombinations;
        }

        private void ValidateParentKnownScenarioCombinationConfiguration(
            string currentKey,
            string parentGroupsRegex)
        {
            var parentsRegex = Keys.GetKeyRegex(parentGroupsRegex);

            var collisionKnownScenarios = this.knownCombinationSet
                .Where(kv => parentsRegex.IsMatch(kv.Key) &&
                    (kv.Key.Equals(currentKey, StringComparison.OrdinalIgnoreCase) ||
                     kv.CombinationType != ScenarioType.Unknown))
                .ToList();

            if (collisionKnownScenarios.Count > 0)
            {
                var firstConfig = collisionKnownScenarios.FirstOrDefault();

                throw new ArgumentException(
                    string.Format(
                        Errors.KnownScenarioCombinationConfigContainsParentConfigurationCollision,
                        firstConfig.Key,
                        firstConfig.CombinationType));
            }
        }

        private void ValidateChildrenKnownScenarioCombinationConfiguration(
            string childrenGroupsRegex)
        {
            var childrenRegex = Keys.GetKeyRegex(childrenGroupsRegex);

            var collisionKnownScenarios = this.knownCombinationSet
                .Where(kv => childrenRegex.IsMatch(kv.Key))
                .ToList();

            if (collisionKnownScenarios.Count > 0)
            {
                var firstConfig = collisionKnownScenarios.FirstOrDefault();

                throw new ArgumentException(
                    string.Format(
                        Errors.KnownScenarioCombinationConfigContainsChildConfigurationCollision,
                        firstConfig.Key,
                        firstConfig.CombinationType));
            }
        }

        private IDictionary<string, string> GetCompatibleScenarioCombinationConfiguration(
            List<IKnownCombination> compatiblesCombinations,
            IDictionary<string, string> originalScenarioConfiguration)
        {
            var randomIndex = 0;

            if (compatiblesCombinations.Count > 1)
            {
                randomIndex = this.Random.Next(compatiblesCombinations.Count);
            }

            var combinationConfiguration = compatiblesCombinations[randomIndex].Combination;

            foreach (var configuration in originalScenarioConfiguration)
            {
                if (!combinationConfiguration.ContainsKey(configuration.Key))
                {
                    combinationConfiguration.Add(
                        configuration.Key,
                        configuration.Value);
                }
            }

            return combinationConfiguration;
        }

        #endregion Private Methods

        #endregion Methods
    }
}