using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;

namespace Akira.Contracts.TestTools.Scenarios.Collections
{
    public interface IContextSet : IEnumerable<IContext>
    {
        ulong CountScenariosCombinations { get; }

        bool CurrentScenarioContextIsDefault { get; }

        /// <summary>
        /// Add a new Scenario Context to the <see cref="IContextSet" />
        /// </summary>
        /// <param name="contextName">The name of new scenario context. Must be unique.</param>
        void AddContext(
            string contextName);

        /// <summary>
        /// Do the needed validation to check if the current context is completed
        /// </summary>
        void ValidateCurrentContextCompleted();

        bool ContainsContext(
            string scenarioContextName);

        IScenario AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioType scenarioType);
    }
}