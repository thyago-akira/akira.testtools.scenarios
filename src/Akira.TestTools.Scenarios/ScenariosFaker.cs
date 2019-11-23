using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;

namespace Akira.TestTools.Scenarios
{
    public class ScenariosFaker<T> : IScenariosBuilder<T>
        where T : class
    {
        #region Fields

        private readonly ConcurrentDictionary<string, InternalFaker<T>> existingFakers =
            new ConcurrentDictionary<string, InternalFaker<T>>(StringComparer.OrdinalIgnoreCase);

        private readonly ScenarioContextSet scenarioContexts = new ScenarioContextSet(Defaults.ScenarioContextName);

        private readonly Dictionary<string, Action<IScenarioRuleSet<T>>> scenariosActions =
            new Dictionary<string, Action<IScenarioRuleSet<T>>>(StringComparer.OrdinalIgnoreCase);

        #endregion Fields

        #region Properties

        private ScenarioContext CurrentContext => this.scenarioContexts.CurrentScenarioContext;

        #endregion Properties

        #region Methods

        #region Public Methods

        #region Generate

        public T Generate(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioCombinationConfiguration = null)
        {
            return this.Generate(
                scenarioBuilderType,
                scenarioCombinationConfiguration,
                true);
        }

        public IEnumerable<T> Generate(
            int count,
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioCombinationConfiguration = null)
        {
            if (count <= 0)
            {
                throw new ArgumentException(
                    Errors.InvalidNumberOfRows);
            }

            return Enumerable
                .Range(1, count)
                .Select(_ => this.Generate(
                    scenarioBuilderType,
                    scenarioCombinationConfiguration));
        }

        public IEnumerable<T> GenerateMinimumTestingScenarios(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All)
        {
            foreach (var scenarioCombinationConfiguration in this.GetMinimumTestingScenarioCombinations(
                scenarioBuilderType))
            {
                yield return this.Generate(
                    scenarioBuilderType,
                    scenarioCombinationConfiguration,
                    false);
            }
        }

        #endregion Generate

        #region ScenarioContext

        /// <summary>
        /// Add a new Scenario Context to the <see cref="IScenariosBuilder{T}" />
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context. Must be unique.</param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> ScenarioContext(string scenarioContextName)
        {
            this.scenarioContexts.Add(scenarioContextName);

            return this;
        }

        #endregion ScenarioContext

        #region Scenario

        /// <summary>
        /// Add a new Valid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="action">The actions that will be executed to the Valid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> DefaultContextValidScenario(
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
        {
            return this.Scenario(
                true,
                Defaults.ScenarioValidName,
                scenarioType,
                action);
        }

        /// <summary>
        /// Add a new Invalid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="action">The actions that will be executed to the Invalid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> DefaultContextInvalidScenario(
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
        {
            return this.Scenario(
                true,
                Defaults.ScenarioInvalidName,
                scenarioType,
                action);
        }

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="action">The actions that will be executed to the current Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> Scenario(
            string scenarioName,
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
        {
            return this.Scenario(
                false,
                scenarioName,
                scenarioType,
                action);
        }

        #endregion Scenario

        #region Add Known Scenario Builder

        /// <summary>
        /// Add a Known Valid Scenario Combination to the <see cref="IScenariosBuilder{T}" />
        /// </summary>
        /// <param name="scenarioCombinationConfiguration">
        /// A dictionary with the builder parameters that can used to build an <see cref="ScenarioCombinationType.AlwaysValid"/> model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <param name="scenarioCombinationType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> KnownScenarioCombination(
            IDictionary<string, string> scenarioCombinationConfiguration,
            ScenarioCombinationType scenarioCombinationType = ScenarioCombinationType.Unknown)
        {
            this.scenarioContexts.AddKnownCombination(
                scenarioCombinationType,
                scenarioCombinationConfiguration);

            return this;
        }

        #endregion Add Known Scenario Builder

        #endregion Public Methods

        #region Private Methods

        private T Generate(
            ScenarioBuilderType scenarioBuilderType,
            IDictionary<string, string> scenarioCombinationConfiguration,
            bool validateBuilderConfiguration)
        {
            if (scenarioCombinationConfiguration == null)
            {
                scenarioCombinationConfiguration = new Dictionary<string, string>();
            }

            if (validateBuilderConfiguration)
            {
                scenarioCombinationConfiguration = this.ValidateScenarioBuilderConfiguration(
                    scenarioBuilderType,
                    scenarioCombinationConfiguration);
            }

            var fullScenarioBuilderRules = this.scenarioContexts.GetFullScenarioBuilderRules(
                scenarioCombinationConfiguration);

            var scenarioFaker = this.GetOrCreateFakerScenario(
                fullScenarioBuilderRules);

            return scenarioFaker.Generate();
        }

        private InternalFaker<T> GetOrCreateFakerScenario(
            IEnumerable<ScenarioKey> fullScenarioBuilderRules)
        {
            var fullScenarioKey = string.Concat(fullScenarioBuilderRules.Select(k => k.KeyValue));

            if (!this.existingFakers.TryGetValue(
                fullScenarioKey,
                out var scenarioFaker))
            {
                scenarioFaker = new InternalFaker<T>();

                foreach (var builderRule in fullScenarioBuilderRules)
                {
                    var action = this.scenariosActions[builderRule.KeyValue];

                    action(scenarioFaker);
                }

                _ = this.existingFakers.TryAdd(fullScenarioKey, scenarioFaker);
            }

            return scenarioFaker;
        }

        private IDictionary<string, string> ValidateScenarioBuilderConfiguration(
            ScenarioBuilderType scenarioBuilderType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            if (!EnumExtensions.AllowedScenarioBuilderTypes.Contains((int)scenarioBuilderType))
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderTypeInvalid);
            }

            if (scenarioBuilderType == ScenarioBuilderType.ValidOnly &&
                !this.scenarioContexts.HasAlwaysValidKnownScenario)
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderDoesnotContainAlwaysValidKnownScenario);
            }

            if (scenarioBuilderType == ScenarioBuilderType.InvalidOnly &&
                !this.scenarioContexts.HasAlwaysInvalidKnownScenario)
            {
                throw new ArgumentException(
                    Errors.ScenarioBuilderDoesnotContainAlwaysInvalidKnownScenario);
            }

            return this.scenarioContexts.ValidateScenarioConfigurationBuilder(
                scenarioBuilderType,
                scenarioCombinationConfiguration);
        }

        /// <summary>
        /// Validate the Action that will be executed for the given Scenario
        /// </summary>
        /// <param name="action">
        /// Action that will be executed for the given Scenario
        /// </param>
        private void ValidateAction(Action<IScenarioRuleSet<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentException(Errors.ScenarioActionIsnotSet);
            }
        }

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="hasDefaultScenarioContext">Indicates if the method was called by a Default Scenario Context (true) or a Custom Scenario Context (false)</param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <param name="action">The actions that will be executed to the current Scenario</param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        private ScenariosFaker<T> Scenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType,
            Action<IScenarioRuleSet<T>> action)
        {
            this.ValidateAction(action);

            var scenarioKey = this.CurrentContext.AddScenario(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            this.scenariosActions.Add(scenarioKey.KeyValue, action);

            this.scenarioContexts.AddKnownCombination(
                scenarioType,
                scenarioKey);

            return this;
        }

        private IEnumerable<IDictionary<string, string>> GetMinimumTestingScenarioCombinations(
            ScenarioBuilderType scenarioType = ScenarioBuilderType.All)
        {
            if (scenarioType == ScenarioBuilderType.All)
            {
                yield return null;

                yield return new Dictionary<string, string>();
            }

            // You should test all known scenarios
            foreach (var knownCombination in this.scenarioContexts.KnownCombinations)
            {
                if (scenarioType == ScenarioBuilderType.All ||
                   (scenarioType == ScenarioBuilderType.ValidOnly &&
                    knownCombination.CombinationType == ScenarioCombinationType.AlwaysValid) ||
                   (scenarioType == ScenarioBuilderType.InvalidOnly &&
                    knownCombination.CombinationType == ScenarioCombinationType.AlwaysInvalid))
                {
                    yield return knownCombination.ScenariosKeys.ToDictionary(
                        kv => kv.ContextName,
                        kv => kv.ScenarioName);
                }
            }
        }

        #endregion Private Methods

        #endregion Methods
    }
}