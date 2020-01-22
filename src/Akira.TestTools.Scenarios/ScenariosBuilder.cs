using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios
{
    /// <summary>
    /// This is an implementation of <see cref="IScenariosBuilder{T}"/>, that follows the
    /// Scenarios Pattern (<see cref="!:https://medium.com/@thyakira/what-is-your-testing-universe-1dce22d82eb">Article Link</see>)
    /// </summary>
    /// <typeparam name="T">Model that will be built by this class</typeparam>
    public class ScenariosBuilder<T> : IScenariosBuilder<T>
        where T : class
    {
        #region Constructors

        public ScenariosBuilder(IScenariosRepository<T> builderConfiguration)
        {
            this.BuilderRepository = builderConfiguration;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the reference to the current <see cref="IScenariosRepository{T}"/>
        /// </summary>
        public IScenariosRepository<T> BuilderRepository { get; }

        #endregion Properties

        #region Methods

        #region Public Methods

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
        public T Generate(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioBuilderCombinationConfiguration = null)
        {
            return this.Generate(
                scenarioBuilderType,
                scenarioBuilderCombinationConfiguration,
                true);
        }

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
        public IEnumerable<T> Generate(
            int count,
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioBuilderCombinationConfiguration = null)
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
                    scenarioBuilderCombinationConfiguration));
        }

        /// <summary>
        /// Generate a list of Model (<see cref="{T}" />), containing the Minimum Testing Scenarios
        /// </summary>
        /// <param name="scenarioBuilderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be returned. Could be
        /// <see cref="ScenarioBuilderType.All"/>, <see cref="ScenarioBuilderType.ValidOnly"/> or
        /// <see cref="ScenarioBuilderType.InvalidOnly"/>
        /// </param>
        /// <returns>A list of Model (<see cref="{T}" />) objects based on the scenario builder type filter</returns>
        public IEnumerable<T> GenerateMinimumTestingScenarios(
            ScenarioBuilderType scenarioBuilderType = ScenarioBuilderType.All)
        {
            foreach (var scenarioCombinationConfiguration in
                this.BuilderRepository.GetMinimumTestingScenarioCombinations(
                    scenarioBuilderType))
            {
                yield return this.Generate(
                    scenarioBuilderType,
                    scenarioCombinationConfiguration,
                    false);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private T Generate(
            ScenarioBuilderType scenarioBuilderType,
            IDictionary<string, string> scenarioCombinationConfiguration,
            bool validateBuilderConfiguration)
        {
            var scenarioFaker = this.BuilderRepository.GetModelBuilder(
                scenarioBuilderType,
                scenarioCombinationConfiguration,
                validateBuilderConfiguration);

            return scenarioFaker.Generate();
        }

        #endregion Private Methods

        #endregion Methods
    }
}