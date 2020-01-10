using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IKnownCombinationCollection : IEnumerable<IKnownCombination>
    {
        bool HasAlwaysValidKnownScenario { get; }

        bool HasAlwaysInvalidKnownScenario { get; }

        void Add(IKnownCombination knownCombination);
    }
}