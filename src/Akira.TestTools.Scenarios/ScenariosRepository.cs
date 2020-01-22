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

        private readonly IKnownCombinationCollection knownCombinationCollection =
            new KnownCombinationCollection();

        private readonly IScenarioRuleSetActionCollection<T> scenarioRuleSetActionCollection =
            new CachedScenarioRuleSetActionCollection<T>();

        private readonly IScenarioContextCollection scenarioContextCollection =
            new ScenarioContextCollection();

        private Random random;

        #endregion Fields

        #region Constructors

        internal ScenariosRepository(string defaultScenarioContextName)
        {
            this.scenarioContextCollection.AddScenarioContext(defaultScenarioContextName);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of distinct possible <see cref="ICompletedModelBuilder<T>"/> for the current <see cref="ScenariosRepository{T}" />
        /// </summary>
        public ulong CountCompletedModelBuilders => this.scenarioContextCollection.CountCompletedModelBuilders;

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
        public void AddScenarioContext(string scenarioContextName) =>
            this.scenarioContextCollection.AddScenarioContext(scenarioContextName);

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="hasDefaultScenarioContext">Indicates if the method was called by a Default Scenario Context (true) or a Custom Scenario Context (false)</param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <param name="scenarioRuleSetAction">The actions that will be executed to the current Scenario</param>
        public void AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
        {
            this.scenarioRuleSetActionCollection.ValidateScenarioRuleSetAction(
                scenarioRuleSetAction);

            var scenario = this.scenarioContextCollection.AddScenario(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            this.scenarioRuleSetActionCollection.AddValidScenarioRuleSetAction(
                scenario.Key,
                scenarioRuleSetAction);

            this.knownCombinationCollection.Add(
                new KnownCombination(
                    scenario.Key,
                    scenario.ScenarioContext.Name,
                    scenario.Name,
                    scenarioType));
        }

        /// <summary>
        /// Add a Known Valid Scenario Combination to the <see cref="ScenariosRepository{T}" />
        /// </summary>
        /// <param name="knownScenarioCombinationConfiguration">
        /// A dictionary with the Known Scenario Combination Configuration that can used to build a model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <param name="scenarioCombinationType">
        /// Indicates if the current Known Scenario Combination Configuration will be
        /// <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or
        /// <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        public void AddKnownScenarioCombination(
            IDictionary<string, string> knownScenarioCombinationConfiguration,
            ScenarioCombinationType scenarioCombinationType = ScenarioCombinationType.Unknown)
        {
            var knownCombination = this.ValidateKnownCombinationConfiguration(
                scenarioCombinationType,
                knownScenarioCombinationConfiguration,
                this.scenarioContextCollection.CurrentScenarioContextIsDefault);

            this.knownCombinationCollection.Add(
                knownCombination);
        }

        public IEnumerable<IDictionary<string, string>> GetMinimumTestingScenarioCombinations(
            ScenarioBuilderType scenarioType = ScenarioBuilderType.All)
        {
            if (scenarioType == ScenarioBuilderType.All)
            {
                yield return null;

                yield return new Dictionary<string, string>();
            }

            // You should test all known scenarios
            foreach (var knownCombination in this.knownCombinationCollection)
            {
                if (scenarioType == ScenarioBuilderType.All ||
                   (scenarioType == ScenarioBuilderType.ValidOnly &&
                    knownCombination.CombinationType == ScenarioCombinationType.AlwaysValid) ||
                   (scenarioType == ScenarioBuilderType.InvalidOnly &&
                    knownCombination.CombinationType == ScenarioCombinationType.AlwaysInvalid))
                {
                    yield return knownCombination.CombinationConfiguration;
                }
            }
        }

        public void ValidateBuilderConfiguration(
            ScenarioBuilderType scenarioBuilderType,
            ref IDictionary<string, string> scenarioBuilderConfiguration)
        {
            if (!EnumExtensions.AllowedScenarioBuilderTypes.Contains((int)scenarioBuilderType))
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderTypeInvalid);
            }

            if (scenarioBuilderType == ScenarioBuilderType.ValidOnly &&
                !this.knownCombinationCollection.HasAlwaysValidKnownScenario)
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderDoesnotContainAlwaysValidKnownScenario);
            }

            if (scenarioBuilderType == ScenarioBuilderType.InvalidOnly &&
                !this.knownCombinationCollection.HasAlwaysInvalidKnownScenario)
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderDoesnotContainAlwaysInvalidKnownScenario);
            }

            scenarioBuilderConfiguration = this.ValidateScenarioConfigurationBuilder(
                scenarioBuilderType,
                scenarioBuilderConfiguration);
        }

        public ICompletedModelBuilder<T> GetModelBuilder(
            ScenarioBuilderType scenarioBuilderType,
            IDictionary<string, string> scenarioCombinationConfiguration,
            bool validateBuilderConfiguration = true)
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

            var modelBuilder = this.scenarioRuleSetActionCollection.GetCompletedModelBuilderByKey(
                fullScenarioBuilderRules.Select(b => b.Key));

            return modelBuilder;
        }

        #endregion Public Methods

        #region Internal Methods

        internal IEnumerable<IScenario> GetFullScenarioBuilderRules(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            this.scenarioContextCollection.ValidateCurrentContextCompleted();

            var buildRules = new List<IScenario>();

            foreach (var scenarioContext in this.scenarioContextCollection)
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
            ScenarioBuilderType builderType,
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

        /// <summary>
        private KnownCombination ValidateKnownCombinationConfiguration(
            ScenarioCombinationType combinationType,
            IDictionary<string, string> scenarioCombinationConfiguration,
            bool currentIsDefaultContext)
        {
            this.ValidateKnownCombinationConfigurationBuilder(
                currentIsDefaultContext,
                scenarioCombinationConfiguration);

            this.ValidateScenarioCombinationConfigurationKeys(
                scenarioCombinationConfiguration);

            this.scenarioContextCollection.ValidateCurrentContextCompleted();

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
                    !this.scenarioContextCollection.ContainsScenarioContext(builderContextName))
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

            foreach (var scenarioContext in this.scenarioContextCollection)
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
            ScenarioBuilderType scenarioCombinationType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            var parentGroupsRegex = string.Empty;

            this.ValidateScenarioCombinationConfigurationValues(
                scenarioCombinationConfiguration,
                (builderScenarioKey) =>
                {
                    if (scenarioCombinationType != ScenarioBuilderType.All)
                    {
                        parentGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                            builderScenarioKey.ScenarioContext.Index,
                            builderScenarioKey.Name);
                    }
                });

            if (scenarioCombinationType == ScenarioBuilderType.All)
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
            ScenarioCombinationType scenarioCombinationType,
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
                        builderScenarioKey.ScenarioContext.Index,
                        builderScenarioKey.Name);

                    childrenGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                        builderScenarioKey.ScenarioContext.Index,
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
                scenariosKeys.ToDictionary(k => k.ScenarioContext.Name, k => k.Name),
                scenarioCombinationType);

            this.ValidateParentKnownScenarioCombinationConfiguration(
                knownCombination.Key,
                parentGroupsRegex);

            if (scenarioCombinationType != ScenarioCombinationType.Unknown)
            {
                this.ValidateChildrenKnownScenarioCombinationConfiguration(
                    childrenGroupsRegex);
            }

            return knownCombination;
        }

        private List<IKnownCombination> ValidateParentScenarioCombinationBuilderConfiguration(
            ScenarioBuilderType scenarioCombinationType,
            string parentGroupsRegex)
        {
            var compatiblesCombinations = new List<IKnownCombination>();

            var parentsRegex = Keys.GetKeyRegex(parentGroupsRegex);

            var allowedCombinationType = scenarioCombinationType == ScenarioBuilderType.ValidOnly
                ? ScenarioCombinationType.AlwaysValid
                : ScenarioCombinationType.AlwaysInvalid;
            var disallowedCombinationType = scenarioCombinationType == ScenarioBuilderType.ValidOnly
                ? ScenarioCombinationType.AlwaysInvalid
                : ScenarioCombinationType.AlwaysValid;

            foreach (var knownCombination in this.knownCombinationCollection
                .Where(kv =>
                    (string.IsNullOrWhiteSpace(parentGroupsRegex) &&
                     kv.CombinationType == allowedCombinationType) ||
                    (parentsRegex.IsMatch(kv.Key) &&
                     kv.CombinationType != ScenarioCombinationType.Unknown)))
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

            var collisionKnownScenarios = this.knownCombinationCollection
                .Where(kv => parentsRegex.IsMatch(kv.Key) &&
                    (kv.Key.Equals(currentKey, StringComparison.OrdinalIgnoreCase) ||
                     kv.CombinationType != ScenarioCombinationType.Unknown))
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

            var collisionKnownScenarios = this.knownCombinationCollection
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

            var combinationConfiguration = compatiblesCombinations[randomIndex].CombinationConfiguration;

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