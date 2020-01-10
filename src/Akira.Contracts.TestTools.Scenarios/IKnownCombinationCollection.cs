using System;
using System.Collections.Generic;
using System.Text;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IKnownCombinationCollection
    {
        bool HasAlwaysValidKnownScenario { get; }

        bool HasAlwaysInvalidKnownScenario { get; }
    }
}