using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Akira.Contracts.TestTools.Scenarios.Enums;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IScenariosRepository<T>
        where T : class
    {
        /// <summary>
        /// Gets the number of distinct possible <see cref="IModelBuilder<T>"/> for the
        /// current <see cref="IScenariosRepository{T}" />
        /// </summary>
        ulong CountModelBuilders { get; }

        /// <summary>
        /// Add a new Scenario Context to the <see cref="IScenariosRepository{T}" />
        /// </summary>
        /// <param name="contextName">The name of new scenario context. Must be unique.</param>
        void AddContext(
            string contextName);

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="onlyForDefaultContext">
        /// This flag indicates if this scenario applies to the default context or not
        /// </param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioAction">
        /// The action that will be executed on the model <see cref="{T}"/> to set the scenario
        /// </param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioType.Unknown"/>, <see cref="ScenarioType.AlwaysValid"/> or <see cref="ScenarioType.AlwaysInvalid"/>
        /// </param>
        void AddScenario(
            bool onlyForDefaultContext,
            string scenarioName,
            Action<IScenarioRuleSet<T>> scenarioAction,
            ScenarioType scenarioType = ScenarioType.Unknown);

        /// <summary>
        /// Add a Known Scenario Combination to the <see cref="IScenariosRepository{T}" />
        /// </summary>
        /// <param name="combination">
        /// A dictionary with the Known Scenario Combination that can be used to build a model.
        /// Key: Context Name
        /// Value: Scenario Name
        /// </param>
        /// <param name="combinationType">
        /// Indicates if the current Known Scenario Combination will be
        /// <see cref="ScenarioType.Unknown"/>, <see cref="ScenarioType.AlwaysValid"/> or
        /// <see cref="ScenarioType.AlwaysInvalid"/>
        /// </param>
        void AddKnownCombination(
            IDictionary<string, string> combination,
            ScenarioType combinationType = ScenarioType.Unknown);

        IModelBuilder<T> GetModelBuilder(
            BuilderType builderType,
            IDictionary<string, string> builderCombination);

        IEnumerable<IModelBuilder<T>> GetMinimumTestingModelBuilders(
            BuilderType builderType);
    }
}