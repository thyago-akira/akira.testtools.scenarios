using System;
using System.Collections.Generic;

namespace Akira.TestTools.Scenarios.InternalModels
{
    internal class KnownCombinationDictionary : Dictionary<string, KnownCombination>
    {
        internal KnownCombinationDictionary() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public void Add(KnownCombination knownCombination)
        {
            this.Add(
                knownCombination.CombinationKey,
                knownCombination);
        }
    }
}