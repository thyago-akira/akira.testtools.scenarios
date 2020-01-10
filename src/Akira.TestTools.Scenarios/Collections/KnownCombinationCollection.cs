using System;
using System.Collections;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios.Collections
{
    public class KnownCombinationCollection : IKnownCombinationCollection
    {
        private readonly IDictionary<string, IKnownCombination> knownCombinations =
            new Dictionary<string, IKnownCombination>(StringComparer.OrdinalIgnoreCase);

        public bool HasAlwaysValidKnownScenario { get; private set; }

        public bool HasAlwaysInvalidKnownScenario { get; private set; }

        public void Add(
            IKnownCombination knownCombination)
        {
            this.knownCombinations.Add(
                knownCombination.Key,
                knownCombination);

            if (knownCombination.CombinationType == ScenarioCombinationType.AlwaysValid &&
                !this.HasAlwaysValidKnownScenario)
            {
                this.HasAlwaysValidKnownScenario = true;
            }

            if (knownCombination.CombinationType == ScenarioCombinationType.AlwaysInvalid &&
                !this.HasAlwaysInvalidKnownScenario)
            {
                this.HasAlwaysInvalidKnownScenario = true;
            }
        }

        public IEnumerator<IKnownCombination> GetEnumerator() => this.knownCombinations.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.knownCombinations.Values.GetEnumerator();
    }
}