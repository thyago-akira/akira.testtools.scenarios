using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios.InternalModels
{
    internal struct KnownCombination
    {
        internal KnownCombination(
            ScenarioCombinationType combinationType,
            params ScenarioKey[] scenariosKeys)
        {
            this.CombinationType = combinationType;
            this.ScenariosKeys = scenariosKeys;
            this.CombinationKey = string.Concat(this.ScenariosKeys.Select(kv => kv.KeyValue));
        }

        internal ScenarioCombinationType CombinationType { get; private set; }

        internal IEnumerable<ScenarioKey> ScenariosKeys { get; private set; }

        internal string CombinationKey { get; private set; }
    }
}