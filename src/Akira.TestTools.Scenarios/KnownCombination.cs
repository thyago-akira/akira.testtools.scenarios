using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios
{
    internal class KnownCombination
    {
        internal KnownCombination(
            ScenarioCombinationType combinationType,
            params ScenarioKey[] scenariosKeys)
        {
            this.CombinationType = combinationType;
            this.CombinationKey = string.Concat(scenariosKeys.Select(kv => kv.KeyValue));
            this.ScenariosKeys = new List<ScenarioKey>(scenariosKeys);
        }

        internal ScenarioCombinationType CombinationType { get; private set; }

        internal IEnumerable<ScenarioKey> ScenariosKeys { get; private set; }

        internal string CombinationKey { get; private set; }
    }
}