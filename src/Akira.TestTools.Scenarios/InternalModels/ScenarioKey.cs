using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.InternalModels
{
    public struct ScenarioKey
    {
        public ScenarioKey(
            string contextName,
            int contextIndex,
            string scenarioName)
        {
            this.ContextName = contextName;
            this.ContextIndex = contextIndex;
            this.ScenarioName = scenarioName;

            if (this.ContextIndex > 0 &&
                !string.IsNullOrWhiteSpace(this.ScenarioName))
            {
                this.KeyValue = Keys.GetScenarioContextKeyValue(
                    this.ContextIndex,
                    this.ScenarioName);
            }
            else
            {
                this.KeyValue = string.Empty;
            }
        }

        internal string ContextName { get; private set; }

        internal int ContextIndex { get; private set; }

        internal string ScenarioName { get; private set; }

        internal string KeyValue { get; private set; }
    }
}