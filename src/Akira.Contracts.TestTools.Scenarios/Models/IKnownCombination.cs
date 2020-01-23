using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;

namespace Akira.Contracts.TestTools.Scenarios.Models
{
    public interface IKnownCombination
    {
        string Key { get; }

        IDictionary<string, string> Combination { get; }

        ScenarioType CombinationType { get; }

        bool SingleConfiguration { get; }
    }
}