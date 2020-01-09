using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios
{
    public abstract class ScenariosBuilder<T> : IScenariosBuilder<T>
        where T : class
    {
        #region Constructors

        public ScenariosBuilder(IScenariosBuilderConfiguration<T> builderConfiguration)
        {
            this.BuilderConfiguration = builderConfiguration;
        }

        #endregion Constructors

        #region Properties

        public IScenariosBuilderConfiguration<T> BuilderConfiguration { get; }

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
            foreach (var scenarioCombinationConfiguration in
                this.BuilderConfiguration.GetMinimumTestingScenarioCombinations(
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
            if (scenarioCombinationConfiguration == null)
            {
                scenarioCombinationConfiguration = new Dictionary<string, string>();
            }

            if (validateBuilderConfiguration)
            {
                this.BuilderConfiguration.ValidateBuilderConfiguration(
                    scenarioBuilderType,
                    ref scenarioCombinationConfiguration);
            }

            var scenarioFaker = this.BuilderConfiguration.GetModelBuilder(
                scenarioBuilderType,
                scenarioCombinationConfiguration);

            return scenarioFaker.Generate();
        }

        #endregion Private Methods

        #endregion Methods
    }
}