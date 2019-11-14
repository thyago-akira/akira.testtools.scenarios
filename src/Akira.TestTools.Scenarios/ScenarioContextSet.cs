using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios
{
    internal class ScenarioContextSet
    {
        private readonly Dictionary<string, ScenarioContext> contexts =
            new Dictionary<string, ScenarioContext>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, KnownCombination> knownCombinations =
            new Dictionary<string, KnownCombination>(StringComparer.OrdinalIgnoreCase);

        private int currentScenarioContextIndex;

        internal ScenarioContextSet(string defaultScenarioContextName)
        {
            this.Add(defaultScenarioContextName, false);
        }

        internal ScenarioContext CurrentScenarioContext { get; private set; }

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

        #region Methods

        #region Internal Methods

        #region Context Methods

        /// <summary>
        /// Add a new Scenario Context, initializing all the information related to it
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        internal void Add(string scenarioContextName)
        {
            this.Add(scenarioContextName, true);
        }

        /// <summary>
        /// Check if the current set has the given scenario context name
        /// </summary>
        /// <param name="scenarioContextName">The scenario name that will be checked</param>
        /// <returns>Returns true if the context name exists</returns>
        internal bool ContainsName(string scenarioContextName)
        {
            return this.contexts.ContainsKey(scenarioContextName);
        }

        #endregion Context Methods

        internal IEnumerable<ScenarioKey> GetFullScenarioBuilderRules(
            IDictionary<string, string> scenarioBuilderContext = null)
        {
            this.CurrentScenarioContext.ValidateContextCompleted();

            var buildRules = new List<ScenarioKey>();

            foreach (var scenarioContext in this.Contexts)
            {
                var scenarioBuilderName = string.Empty;

                if (scenarioBuilderContext != null &&
                    scenarioBuilderContext.TryGetValue(
                        scenarioContext.Name,
                        out scenarioBuilderName))
                {
                    scenarioBuilderName = scenarioBuilderName.Trim();
                }

                buildRules.Add(
                    scenarioContext.GetScenarioKey(
                        scenarioBuilderName,
                        true));
            }

            return buildRules;
        }

        #region Known Combinations Methods

        internal void AddKnownCombination(
            ScenarioCombinationType combinationType,
            ScenarioKey scenarioKey)
        {
            var knownCombination = new KnownCombination(combinationType, scenarioKey);

            this.knownCombinations.Add(
                knownCombination.CombinationKey,
                knownCombination);
        }

        internal void AddKnownCombination(
            ScenarioCombinationType combinationType,
            IDictionary<string, string> combinationBuilder)
        {
            this.ValidateKnownCombinationBuilder(combinationBuilder);

            this.CurrentScenarioContext.ValidateContextCompleted();

            var knownCombination = this.ValidateKnownCombinationBuilderValues(
                combinationType,
                new Dictionary<string, string>(
                    combinationBuilder,
                    StringComparer.OrdinalIgnoreCase));

            this.knownCombinations.Add(
                knownCombination.CombinationKey,
                knownCombination);
        }

        #endregion Known Combinations Methods

        #endregion Internal Methods

        #region Private Methods

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

        private void ValidateKnownCombinationBuilder(
            IDictionary<string, string> combinationBuilder)
        {
            if (this.CurrentScenarioContext.IsCurrentScenarioContextDefaultContext)
            {
                throw new ArgumentException(
                    Errors.ScenarioFakerWithNoAdditionalScenariosForKnownScenarioCombinationConfig);
            }

            if (combinationBuilder == null ||
                combinationBuilder.Count == 0)
            {
                throw new ArgumentException(
                    Errors.KnownScenarioCombinationConfigNotSet);
            }

            if (combinationBuilder.Count == 1)
            {
                throw new ArgumentException(
                    Errors.KnownScenarioCombinationConfigWithOnlyOneCondition);
            }

            foreach (var builderContextName in combinationBuilder.Keys)
            {
                if (string.IsNullOrEmpty(builderContextName) ||
                    !this.ContainsName(builderContextName))
                {
                    throw new ArgumentException(
                        string.Format(
                            Errors.KnownScenarioCombinationConfigWithInvalidScenarioContext,
                            builderContextName));
                }
            }
        }

        private KnownCombination ValidateKnownCombinationBuilderValues(
            ScenarioCombinationType scenarioCombinationType,
            Dictionary<string, string> scenarioCombinationConfiguration)
        {
            var scenariosKeys = new List<ScenarioKey>();
            var parentGroupsRegex = string.Empty;
            var childrenGroupsRegex = string.Empty;

            foreach (var scenarioContext in this.Contexts)
            {
                if (scenarioCombinationConfiguration.TryGetValue(
                    scenarioContext.Name,
                    out var builderScenarioName))
                {
                    var builderScenarioKey = scenarioContext.GetScenarioKey(builderScenarioName);

                    if (string.IsNullOrWhiteSpace(builderScenarioKey.KeyValue))
                    {
                        throw new ArgumentException(
                            string.Format(
                                Errors.KnownScenarioCombinationConfigWithInvalidScenario,
                                scenarioContext.Name,
                                builderScenarioName));
                    }

                    scenariosKeys.Add(builderScenarioKey);

                    var (parents, children) = Keys.GetScenarioContextKeyValueRegexValue(
                        scenarioContext.Index,
                        builderScenarioName);

                    parentGroupsRegex += parents;
                    childrenGroupsRegex += children;
                }
                else
                {
                    childrenGroupsRegex += Keys.GetMissingScenarioContextKeyValueRegexValue(
                        scenarioContext.Index);
                }
            }

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

        private void ValidateParentKnownScenarioCombinationConfiguration(
            string currentKey,
            string parentGroupsRegex)
        {
            var parentsRegex = Keys.GetRegex(parentGroupsRegex);

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
            var childrenRegex = Keys.GetRegex(childrenGroupsRegex);

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

        #endregion Private Methods

        #endregion Methods
    }
}