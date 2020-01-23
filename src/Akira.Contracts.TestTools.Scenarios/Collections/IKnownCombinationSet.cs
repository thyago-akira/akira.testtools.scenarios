using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;

namespace Akira.Contracts.TestTools.Scenarios.Collections
{
    public interface IKnownCombinationSet : IEnumerable<IKnownCombination>
    {
        bool HasAlwaysValidKnownScenario { get; }

        bool HasAlwaysInvalidKnownScenario { get; }

        void AddKnownCombination(
            IKnownCombination knownCombination);

        void AddScenario(
            IScenario scenario,
            ScenarioType scenarioType);
    }
}