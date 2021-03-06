﻿using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios
{
    internal class ScenarioContextSet
    {
        #region Fields

        private readonly Dictionary<string, ScenarioContext> contexts =
            new Dictionary<string, ScenarioContext>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, KnownCombination> knownCombinations =
            new Dictionary<string, KnownCombination>(StringComparer.OrdinalIgnoreCase);

        private int currentScenarioContextIndex;

        private Random random;

        #endregion Fields

        #region Constructors

        internal ScenarioContextSet(string defaultScenarioContextName)
        {
            this.Add(defaultScenarioContextName, false);
        }

        #endregion Constructors

        #region Properties

        internal ulong CountPossibleScenariosCombinations { get; private set; } = 1;

        internal ScenarioContext CurrentScenarioContext { get; private set; }

        internal bool HasAlwaysValidKnownScenario { get; private set; }

        internal bool HasAlwaysInvalidKnownScenario { get; private set; }

        internal IEnumerable<ScenarioContext> Contexts
        {
            get
            {
                foreach (var context in this.contexts.Values)
                {
                    yield return context;
                }
            }
        }

        internal IEnumerable<KnownCombination> KnownCombinations
        {
            get
            {
                foreach (var knownCombination in this.knownCombinations.Values)
                {
                    yield return knownCombination;
                }
            }
        }

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

        #region Internal Methods

        /// <summary>
        /// Add a new Scenario Context, initializing all the information related to it
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        internal void Add(string scenarioContextName)
        {
            this.Add(scenarioContextName, true);
        }

        /// <summary>
        /// Validate the Scenario and returns the Scenario Key
        /// </summary>
        /// <param name="hasDefaultScenarioContext">
        /// Flag that indicates if the method was called by a Default Scenario Context method (true)
        /// or a Custom Scenario Context method (false)
        /// </param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>The Scenario Key</returns>
        internal ScenarioKey AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType)
        {
            var scenarioKey = this.CurrentScenarioContext.AddScenario(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            if (this.CurrentScenarioContext.ScenariosCount > 1)
            {
                if (this.CurrentScenarioContext.ScenariosCount > 2)
                {
                    this.CountPossibleScenariosCombinations /= (ulong)(this.CurrentScenarioContext.ScenariosCount - 1);
                }

                this.CountPossibleScenariosCombinations *= (ulong)this.CurrentScenarioContext.ScenariosCount;
            }

            return scenarioKey;
        }

        internal IEnumerable<ScenarioKey> GetFullScenarioBuilderRules(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            this.CurrentScenarioContext.ValidateContextCompleted();

            var buildRules = new List<ScenarioKey>();

            foreach (var scenarioContext in this.Contexts)
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
                    scenarioContext.GetScenarioKey(
                        scenarioBuilderName,
                        true));
            }

            return buildRules;
        }

        internal void AddKnownCombination(
            ScenarioCombinationType combinationType,
            ScenarioKey scenarioKey)
        {
            var knownCombination = new KnownCombination(combinationType, scenarioKey);

            this.AddKnownCombination(knownCombination);
        }

        internal void AddKnownCombination(
            ScenarioCombinationType combinationType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            var knownCombination = this.ValidateKnownCombinationConfiguration(
                combinationType,
                scenarioCombinationConfiguration);

            this.AddKnownCombination(knownCombination);
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
        /// Check if the current set has the given scenario context name
        /// </summary>
        /// <param name="scenarioContextName">The scenario name that will be checked</param>
        /// <returns>Returns true if the context name exists</returns>
        private bool ContainsName(string scenarioContextName)
        {
            return this.contexts.ContainsKey(scenarioContextName);
        }

        /// <summary>
        /// Add a new Scenario Context, initializing all the information related to it
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        /// <param name="validateName">Indicates if the name will be validated</param>
        private void Add(
            string scenarioContextName,
            bool validateName)
        {
            if (validateName)
            {
                this.CurrentScenarioContext.ValidateContextCompleted();

                this.ValidateContextName(scenarioContextName);
            }

            this.CurrentScenarioContext = new ScenarioContext(
                scenarioContextName,
                ++this.currentScenarioContextIndex);

            this.contexts.Add(scenarioContextName, this.CurrentScenarioContext);
        }

        private void AddKnownCombination(
            KnownCombination knownCombination)
        {
            this.knownCombinations.Add(
                knownCombination.CombinationKey,
                knownCombination);

            if (knownCombination.CombinationType == ScenarioCombinationType.AlwaysValid &&
                !this.HasAlwaysValidKnownScenario)
            {
                this.HasAlwaysValidKnownScenario = true;
            }

            if (knownCombination.CombinationType == ScenarioCombinationType.AlwaysInvalid &&
                !this.HasAlwaysInvalidKnownScenario)
            {
                this.HasAlwaysInvalidKnownScenario = true;
            }
        }

        /// <summary>
        /// Validate the new Scenario Context Name
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        private void ValidateContextName(string scenarioContextName)
        {
            if (string.IsNullOrWhiteSpace(scenarioContextName))
            {
                throw new ArgumentException(
                    Errors.ScenarioContextNameIsnotSet);
            }

            if (string.Equals(
                scenarioContextName,
                Defaults.ScenarioContextName,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    Errors.ScenarioContextNameAsDefaultIsnotAllowed);
            }

            if (this.ContainsName(scenarioContextName))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioContextNameAlreadyExists,
                        scenarioContextName));
            }
        }

        private KnownCombination ValidateKnownCombinationConfiguration(
            ScenarioCombinationType combinationType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            this.ValidateKnownCombinationConfigurationBuilder(
                scenarioCombinationConfiguration);

            this.ValidateScenarioCombinationConfigurationKeys(
                scenarioCombinationConfiguration);

            this.CurrentScenarioContext.ValidateContextCompleted();

            return this.ValidateKnownCombinationConfigurationBuilderValues(
                combinationType,
                scenarioCombinationConfiguration);
        }

        private void ValidateKnownCombinationConfigurationBuilder(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            if (this.CurrentScenarioContext.IsCurrentScenarioContextDefaultContext)
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
                    !this.ContainsName(builderContextName))
                {
                    throw new ArgumentException(
                        string.Format(
                            Errors.ScenarioCombinationConfigWithInvalidScenarioContext,
                            builderContextName));
                }
            }
        }

        private void ValidateScenarioCombinationConfigurationValues(
            IDictionary<string, string> scenarioCombinationConfiguration,
            Action<ScenarioKey> existingConfigurationAction,
            Action<int> missingConfigurationAction = null)
        {
            var localDictionary = new Dictionary<string, string>(
                scenarioCombinationConfiguration,
                StringComparer.OrdinalIgnoreCase);

            foreach (var scenarioContext in this.Contexts)
            {
                if (localDictionary.TryGetValue(
                    scenarioContext.Name,
                    out var builderScenarioName))
                {
                    var builderScenarioKey = scenarioContext.GetScenarioKey(builderScenarioName);

                    if (string.IsNullOrWhiteSpace(builderScenarioKey.KeyValue))
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
                            builderScenarioKey.ContextIndex,
                            builderScenarioKey.ScenarioName);
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
            var scenariosKeys = new List<ScenarioKey>();
            var parentGroupsRegex = string.Empty;
            var childrenGroupsRegex = string.Empty;

            this.ValidateScenarioCombinationConfigurationValues(
                scenarioCombinationConfiguration,
                (builderScenarioKey) =>
                {
                    scenariosKeys.Add(builderScenarioKey);

                    parentGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                        builderScenarioKey.ContextIndex,
                        builderScenarioKey.ScenarioName);

                    childrenGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                        builderScenarioKey.ContextIndex,
                        builderScenarioKey.ScenarioName,
                        false);
                },
                (index) =>
                {
                    childrenGroupsRegex += Keys.GetScenarioContextKeyValueRegexValue(
                        index);
                });

            var knownCombination = new KnownCombination(
                scenarioCombinationType,
                scenariosKeys.ToArray());

            this.ValidateParentKnownScenarioCombinationConfiguration(
                knownCombination.CombinationKey,
                parentGroupsRegex);

            if (scenarioCombinationType != ScenarioCombinationType.Unknown)
            {
                this.ValidateChildrenKnownScenarioCombinationConfiguration(
                    childrenGroupsRegex);
            }

            return knownCombination;
        }

        private List<KnownCombination> ValidateParentScenarioCombinationBuilderConfiguration(
            ScenarioBuilderType scenarioCombinationType,
            string parentGroupsRegex)
        {
            var compatiblesCombinations = new List<KnownCombination>();

            var parentsRegex = Keys.GetKeyRegex(parentGroupsRegex);

            var allowedCombinationType = scenarioCombinationType == ScenarioBuilderType.ValidOnly
                ? ScenarioCombinationType.AlwaysValid
                : ScenarioCombinationType.AlwaysInvalid;
            var disallowedCombinationType = scenarioCombinationType == ScenarioBuilderType.ValidOnly
                ? ScenarioCombinationType.AlwaysInvalid
                : ScenarioCombinationType.AlwaysValid;

            foreach (var knownCombination in this.knownCombinations
                .Where(kv =>
                    (string.IsNullOrWhiteSpace(parentGroupsRegex) &&
                     kv.Value.CombinationType == allowedCombinationType) ||
                    (parentsRegex.IsMatch(kv.Key) &&
                     kv.Value.CombinationType != ScenarioCombinationType.Unknown)))
            {
                if (knownCombination.Value.CombinationType == disallowedCombinationType)
                {
                    throw new ArgumentException(
                        string.Format(
                            Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                            scenarioCombinationType,
                            knownCombination.Key,
                            knownCombination.Value.CombinationType));
                }
                else
                {
                    compatiblesCombinations.Add(knownCombination.Value);
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

            var collisionKnownScenarios = this.knownCombinations
                .Where(kv => parentsRegex.IsMatch(kv.Key) &&
                    (kv.Key.Equals(currentKey, StringComparison.OrdinalIgnoreCase) ||
                     kv.Value.CombinationType != ScenarioCombinationType.Unknown))
                .ToList();

            if (collisionKnownScenarios.Count > 0)
            {
                var firstConfig = collisionKnownScenarios.FirstOrDefault();

                throw new ArgumentException(
                    string.Format(
                        Errors.KnownScenarioCombinationConfigContainsParentConfigurationCollision,
                        firstConfig.Key,
                        firstConfig.Value.CombinationType));
            }
        }

        private void ValidateChildrenKnownScenarioCombinationConfiguration(
            string childrenGroupsRegex)
        {
            var childrenRegex = Keys.GetKeyRegex(childrenGroupsRegex);

            var collisionKnownScenarios = this.knownCombinations
                .Where(kv => childrenRegex.IsMatch(kv.Key))
                .ToList();

            if (collisionKnownScenarios.Count > 0)
            {
                var firstConfig = collisionKnownScenarios.FirstOrDefault();

                throw new ArgumentException(
                    string.Format(
                        Errors.KnownScenarioCombinationConfigContainsChildConfigurationCollision,
                        firstConfig.Key,
                        firstConfig.Value.CombinationType));
            }
        }

        private IDictionary<string, string> GetCompatibleScenarioCombinationConfiguration(
            List<KnownCombination> compatiblesCombinations,
            IDictionary<string, string> originalScenarioConfiguration)
        {
            var randomIndex = 0;

            if (compatiblesCombinations.Count > 1)
            {
                randomIndex = this.Random.Next(compatiblesCombinations.Count);
            }

            var combinationConfiguration = compatiblesCombinations[randomIndex].ScenariosKeys
                .ToDictionary(
                    kv => kv.ContextName,
                    kv => kv.ScenarioName);

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