using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Models;

namespace Akira.Contracts.TestTools.Scenarios.Collections
{
    public interface IKnownCombinationCollection : IEnumerable<IKnownCombination>
    {
        bool HasAlwaysValidKnownScenario { get; }

        bool HasAlwaysInvalidKnownScenario { get; }

        void Add(IKnownCombination knownCombination);
    }
}