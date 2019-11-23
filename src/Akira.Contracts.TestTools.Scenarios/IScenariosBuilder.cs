using System;
using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IScenariosBuilder<T>
        where T : class
    {
        /// <summary>
        /// Add a new Scenario Context to the <see cref="IScenariosBuilder{T}" />
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context. Must be unique.</param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        IScenariosBuilder<T> ScenarioContext(
            string scenarioContextName);

        /// <summary>
        /// Add a new Valid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="action">The actions that will be executed to the Valid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        IScenariosBuilder<T> DefaultContextValidScenario(
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown);

        /// <summary>
        /// Add a new Invalid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="action">The actions that will be executed to the Invalid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        IScenariosBuilder<T> DefaultContextInvalidScenario(
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown);

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="action">The actions that will be executed to the current Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        IScenariosBuilder<T> Scenario(
            string scenarioName,
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown);

        /// <summary>
        /// Add a Known Valid Scenario Combination to the <see cref="IScenariosBuilder{T}" />
        /// </summary>
        /// <param name="scenarioCombinationConfiguration">
        /// A dictionary with the builder parameters that can used to build an <see cref="ScenarioCombinationType.AlwaysValid"/> model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <param name="scenarioCombinatioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        IScenariosBuilder<T> KnownScenarioCombination(
            IDictionary<string, string> scenarioCombinationConfiguration,
            ScenarioCombinationType scenarioCombinatioType = ScenarioCombinationType.Unknown);

        /// <summary>
        /// Generate a new instance of model
        /// </summary>
        /// <param name="scenarioBuilderType">
        /// Indicates the kind of model you will create
        /// </param>
        /// <param name="scenarioCombinationConfiguration">
        /// Dictionary with the builder parameters that will be used to build the new model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A model object based on the scenario builder configuration</returns>
        T Generate(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioCombinationConfiguration = null);

        /// <summary>
        /// Generate multiples instances of model
        /// </summary>
        /// <param name="count">Number of model's instances to be generated</param>
        /// <param name="scenarioBuilderType">
        /// Indicates the kind of model you will create
        /// </param>
        /// <param name="scenarioCombinationConfiguration">
        /// Dictionary with the builder parameters that will be used to build the new model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A list of model objects based on the scenario builder configuration</returns>
        IEnumerable<T> Generate(
            int count,
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioCombinationConfiguration = null);

        /// <summary>
        /// Generate multiples instances of model based on kind of model you need to create
        /// </summary>
        /// <param name="scenarioBuilderType">
        /// Indicates the kind of model you will create
        /// </param>
        /// <returns>A list of model objects based on the scenario builder configuration</returns>
        IEnumerable<T> GenerateMinimumTestingScenarios(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All);
    }
}