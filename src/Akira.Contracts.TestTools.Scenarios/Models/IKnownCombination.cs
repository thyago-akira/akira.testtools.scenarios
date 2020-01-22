using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;

namespace Akira.Contracts.TestTools.Scenarios.Models
{
    public interface IKnownCombination
    {
        string Key { get; }

        IDictionary<string, string> CombinationConfiguration { get; }

        ScenarioCombinationType CombinationType { get; }

        bool SingleConfiguration { get; }
    }
}