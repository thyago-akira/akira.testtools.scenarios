using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;

namespace Akira.TestTools.Scenarios.Models
{
    public class KnownCombination : IKnownCombination
    {
        public KnownCombination(
            string key,
            IDictionary<string, string> combination,
            ScenarioType combinationType)
        {
            this.Key = key;
            this.Combination = combination;
            this.CombinationType = combinationType;
        }

        public KnownCombination(
            string key,
            string contextName,
            string scenarioName,
            ScenarioType combinationType) :
            this(
                key,
                new Dictionary<string, string> { { contextName, scenarioName } },
                combinationType)
        {
            this.SingleConfiguration = true;
        }

        public string Key { get; }

        public IDictionary<string, string> Combination { get; }

        public ScenarioType CombinationType { get; }

        public bool SingleConfiguration { get; }
    }
}