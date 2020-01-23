using System;
using System.Collections;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;
using Akira.TestTools.Scenarios.Models;

namespace Akira.TestTools.Scenarios.Collections
{
    public class KnownCombinationSet : IKnownCombinationSet
    {
        private readonly IDictionary<string, IKnownCombination> knownCombinations =
            new Dictionary<string, IKnownCombination>(StringComparer.OrdinalIgnoreCase);

        public bool HasAlwaysValidKnownScenario { get; private set; }

        public bool HasAlwaysInvalidKnownScenario { get; private set; }

        public void AddKnownCombination(
            IKnownCombination knownCombination)
        {
            this.Add(knownCombination);
        }

        public void AddScenario(
            IScenario scenario,
            ScenarioType scenarioType)
        {
            var knownCombination = new KnownCombination(
                scenario.Key,
                scenario.Context.Name,
                scenario.Name,
                scenarioType);

            this.Add(knownCombination);
        }

        public IEnumerator<IKnownCombination> GetEnumerator() => this.knownCombinations.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.knownCombinations.Values.GetEnumerator();

        private void Add(
            IKnownCombination knownCombination)
        {
            this.knownCombinations.Add(
                knownCombination.Key,
                knownCombination);

            if (knownCombination.CombinationType == ScenarioType.AlwaysValid &&
                !this.HasAlwaysValidKnownScenario)
            {
                this.HasAlwaysValidKnownScenario = true;
            }

            if (knownCombination.CombinationType == ScenarioType.AlwaysInvalid &&
                !this.HasAlwaysInvalidKnownScenario)
            {
                this.HasAlwaysInvalidKnownScenario = true;
            }
        }
    }
}