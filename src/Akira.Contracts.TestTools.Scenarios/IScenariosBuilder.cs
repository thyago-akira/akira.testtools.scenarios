using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;

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
        /// <summary>
        /// Gets the reference to the current <see cref="IScenariosRepository{T}"/>
        /// </summary>
        IScenariosRepository<T> BuilderRepository { get; }

        /// <summary>
        /// Generate a new instance of Model (<see cref="{T}" />)
        /// </summary>
        /// <param name="builderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be created. It could be
        /// <see cref="BuilderType.All"/>, <see cref="BuilderType.ValidOnly"/> or
        /// <see cref="BuilderType.InvalidOnly"/>
        /// </param>
        /// <param name="builderCombination">
        /// A dictionary with the builder combination that will be used to build the new model.
        /// Key: Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A Model (<see cref="{T}" />) object based on the scenario builder parameters</returns>
        T Generate(
            BuilderType builderType = BuilderType.All,
            IDictionary<string, string> builderCombination = null);

        /// <summary>
        /// Generate multiple instances of Model (<see cref="{T}" />)
        /// </summary>
        /// <param name="count">Number of instances to be generated</param>
        /// <param name="builderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be created. It could be
        /// <see cref="BuilderType.All"/>, <see cref="BuilderType.ValidOnly"/> or
        /// <see cref="BuilderType.InvalidOnly"/>
        /// </param>
        /// <param name="builderCombination">
        /// A dictionary with the builder combination that will be used to build the new model.
        /// Key: Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>A list of Model (<see cref="{T}" />) objects based on the scenario builder parameters</returns>
        IEnumerable<T> Generate(
            int count,
            BuilderType builderType = BuilderType.All,
            IDictionary<string, string> builderCombination = null);

        /// <summary>
        /// Generate a list of Model (<see cref="{T}" />), containing the Minimum Testing Scenarios
        /// </summary>
        /// <param name="builderType">
        /// Indicates the kind of Model (<see cref="{T}" />) that will be returned. It could be
        /// <see cref="BuilderType.All"/>, <see cref="BuilderType.ValidOnly"/> or
        /// <see cref="BuilderType.InvalidOnly"/>
        /// </param>
        /// <returns>A list of Model (<see cref="{T}" />) objects based on the scenario builder type</returns>
        IEnumerable<T> GenerateMinimumTestingScenarios(
            BuilderType builderType = BuilderType.All);
    }
}