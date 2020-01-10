using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios
{
    public class KnownCombination : IKnownCombination
    {
        public KnownCombination(
            string key,
            IDictionary<string, string> combinationConfiguration,
            ScenarioCombinationType combinationType)
        {
            this.Key = key;
            this.CombinationConfiguration = combinationConfiguration;
            this.CombinationType = combinationType;
        }

        public KnownCombination(
            string key,
            string contextName,
            string scenarioName,
            ScenarioCombinationType combinationType) :
            this(
                key,
                new Dictionary<string, string> { { contextName, scenarioName } },
                combinationType)
        {
            this.SingleConfiguration = true;
        }

        public string Key { get; }

        public IDictionary<string, string> CombinationConfiguration { get; }

        public ScenarioCombinationType CombinationType { get; }

        public bool SingleConfiguration { get; }
    }
}