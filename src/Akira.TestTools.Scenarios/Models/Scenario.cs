using Akira.Contracts.TestTools.Scenarios.Models;
using static Akira.TestTools.Scenarios.Constants.Keys;

namespace Akira.TestTools.Scenarios.Models
{
    public class Scenario : IScenario
    {
        public Scenario(
            IScenarioContext scenarioContext,
            string scenarioName)
        {
            this.ScenarioContext = scenarioContext;
            this.Name = scenarioName;
        }

        public IScenarioContext ScenarioContext { get; private set; }

        public string Name { get; private set; }

        public string Key =>
            GetScenarioContextKeyValue(this.ScenarioContext.Index, this.Name);
    }
}