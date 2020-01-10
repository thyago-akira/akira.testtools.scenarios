using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IKnownCombination
    {
        string Key { get; }

        IDictionary<string, string> CombinationConfiguration { get; }

        ScenarioCombinationType CombinationType { get; }

        bool SingleConfiguration { get; }
    }
}