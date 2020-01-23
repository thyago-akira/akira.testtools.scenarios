using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Enums;
using static Akira.TestTools.Scenarios.Constants.Errors.ScenariosBuilder;

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

        public ScenariosBuilder()
        {
            this.BuilderRepository = new ScenariosRepository<T>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the reference to the current <see cref="IScenariosRepository{T}"/>
        /// </summary>
        public IScenariosRepository<T> BuilderRepository { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Generate a new instance of Model (<see cref="{T}" />)
        /// </summary>
        /// <param name="builderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be created. Could be
        /// <see cref="BuilderType.All"/>, <see cref="BuilderType.ValidOnly"/> or
        /// <see cref="BuilderType.InvalidOnly"/>
        /// </param>
        /// <param name="builderCombination">
        /// A dictionary with the builder combination configuration that will be used to build the new model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A Model (<see cref="{T}" />) object based on the scenario builder configuration</returns>
        public T Generate(
            BuilderType builderType = BuilderType.All,
            IDictionary<string, string> builderCombination = null)
        {
            var modelBuilder = this.BuilderRepository.GetModelBuilder(
                builderType,
                builderCombination);

            return modelBuilder.Generate();
        }

        /// <summary>
        /// Generate multiple instances of Model (<see cref="{T}" />)
        /// </summary>
        /// <param name="count">Number of instances to be generated</param>
        /// <param name="builderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be created. Could be
        /// <see cref="BuilderType.All"/>, <see cref="BuilderType.ValidOnly"/> or
        /// <see cref="BuilderType.InvalidOnly"/>
        /// </param>
        /// <param name="builderCombination">
        /// A dictionary with the builder combination configuration that will be used to build the new model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A list of Model (<see cref="{T}" />) objects based on the scenario builder configuration</returns>
        public IEnumerable<T> Generate(
            int count,
            BuilderType builderType = BuilderType.All,
            IDictionary<string, string> builderCombination = null)
        {
            if (count <= 0)
            {
                throw new ArgumentException(InvalidNumberOfRows);
            }

            return Enumerable
                .Range(1, count)
                .Select(_ => this.Generate(
                    builderType,
                    builderCombination));
        }

        /// <summary>
        /// Generate a list of Model (<see cref="{T}" />), containing the Minimum Testing Scenarios
        /// </summary>
        /// <param name="builderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be returned. Could be
        /// <see cref="BuilderType.All"/>, <see cref="BuilderType.ValidOnly"/> or
        /// <see cref="BuilderType.InvalidOnly"/>
        /// </param>
        /// <returns>A list of Model (<see cref="{T}" />) objects based on the scenario builder type filter</returns>
        public IEnumerable<T> GenerateMinimumTestingScenarios(
            BuilderType builderType = BuilderType.All)
        {
            foreach (var modelBuilder in
                this.BuilderRepository.GetMinimumTestingModelBuilders(
                    builderType))
            {
                yield return modelBuilder.Generate();
            }
        }

        #endregion Methods
    }
}