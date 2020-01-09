using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios
{
    /// <summary>
    /// This contract defines the properties/methods used to create instance(s) of <see cref="{T}"/> using the
    /// Scenarios Pattern (<see cref="!:https://medium.com/@thyakira/what-is-your-testing-universe-1dce22d82eb">Article Link</see>)
    /// </summary>
    /// <typeparam name="T">Model that will be built by this interface</typeparam>
    public interface IScenariosBuilder<T>
        where T : class
    {
        IScenariosBuilderConfiguration<T> BuilderConfiguration { get; }

        /// <summary>
        /// Generate a new instance of Model (<see cref="{T}" />)
        /// </summary>
        /// <param name="scenarioBuilderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be created. Could be
        /// <see cref="ScenarioBuilderType.All"/>, <see cref="ScenarioBuilderType.ValidOnly"/> or
        /// <see cref="ScenarioBuilderType.InvalidOnly"/>
        /// </param>
        /// <param name="scenarioBuilderCombinationConfiguration">
        /// A dictionary with the builder combination configuration that will be used to build the new model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A Model (<see cref="{T}" />) object based on the scenario builder configuration</returns>
        T Generate(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioBuilderCombinationConfiguration = null);

        /// <summary>
        /// Generate multiple instances of Model (<see cref="{T}" />)
        /// </summary>
        /// <param name="count">Number of instances to be generated</param>
        /// <param name="scenarioBuilderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be created. Could be
        /// <see cref="ScenarioBuilderType.All"/>, <see cref="ScenarioBuilderType.ValidOnly"/> or
        /// <see cref="ScenarioBuilderType.InvalidOnly"/>
        /// </param>
        /// <param name="scenarioBuilderCombinationConfiguration">
        /// A dictionary with the builder combination configuration that will be used to build the new model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A list of Model (<see cref="{T}" />) objects based on the scenario builder configuration</returns>
        IEnumerable<T> Generate(
            int count,
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioBuilderCombinationConfiguration = null);

        /// <summary>
        /// Generate a list of Model (<see cref="{T}" />), containing the Minimum Testing Scenarios
        /// </summary>
        /// <param name="scenarioBuilderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be returned. Could be
        /// <see cref="ScenarioBuilderType.All"/>, <see cref="ScenarioBuilderType.ValidOnly"/> or
        /// <see cref="ScenarioBuilderType.InvalidOnly"/>
        /// </param>
        /// <returns>A list of Model (<see cref="{T}" />) objects based on the scenario builder type filter</returns>
        IEnumerable<T> GenerateMinimumTestingScenarios(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All);
    }
}