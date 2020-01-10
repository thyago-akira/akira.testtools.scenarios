using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios.InternalModels
{
    internal class KnownCombinationDictionary : Dictionary<string, KnownCombination>
    {
        internal bool HasAlwaysValidKnownScenario { get; private set; }

        internal bool HasAlwaysInvalidKnownScenario { get; private set; }

        internal KnownCombinationDictionary() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        internal void Add(KnownCombination knownCombination)
        {
            this.Add(
                knownCombination.CombinationKey,
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
    }
}