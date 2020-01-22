using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IScenariosRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets the number of distinct possible <see cref="ICompletedModelBuilder<T>"/> for the current <see cref="IScenariosRepository{T}" />
        /// </summary>
        ulong CountCompletedModelBuilders { get; }

        /// <summary>
        /// Add a new Scenario Context to the <see cref="IScenariosRepository{T}" />
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context. Must be unique.</param>
        void AddScenarioContext(
            string scenarioContextName);

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioRuleSetAction">
        /// The action that will be executed on the model <see cref="{T}"/> to set the current Scenario
        /// </param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        void AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown);

        /// <summary>
        /// Add a Known Valid Scenario Combination to the <see cref="IScenariosRepository{T}" />
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
        void AddKnownScenarioCombination(
            IDictionary<string, string> knownScenarioCombinationConfiguration,
            ScenarioCombinationType scenarioCombinatioType = ScenarioCombinationType.Unknown);

        ICompletedModelBuilder<T> GetModelBuilder(
            ScenarioBuilderType scenarioBuilderType,
            IDictionary<string, string> scenarioBuilderConfiguration,
            bool validateBuilderConfiguration = true);

        IEnumerable<IDictionary<string, string>> GetMinimumTestingScenarioCombinations(
            ScenarioBuilderType scenarioBuilderType);
    }
}