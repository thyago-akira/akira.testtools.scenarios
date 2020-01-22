using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;

namespace Akira.Contracts.TestTools.Scenarios.Collections
{
    public interface IScenarioContextCollection : IEnumerable<IScenarioContext>
    {
        ulong CountCompletedModelBuilders { get; }

        bool CurrentScenarioContextIsDefault { get; }

        void AddScenarioContext(
            string scenarioContextName);

        void ValidateCurrentContextCompleted();

        bool ContainsScenarioContext(
            string scenarioContextName);

        IScenario AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType);
    }
}