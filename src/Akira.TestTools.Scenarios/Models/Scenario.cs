using Akira.Contracts.TestTools.Scenarios.Models;
using static Akira.TestTools.Scenarios.Constants.Keys;

namespace Akira.TestTools.Scenarios.Models
{
    public class Scenario : IScenario
    {
        public Scenario(
            IContext scenarioContext,
            string scenarioName)
        {
            this.Context = scenarioContext;
            this.Name = scenarioName;
        }

        public IContext Context { get; private set; }

        public string Name { get; private set; }

        public string Key =>
            GetScenarioContextKeyValue(this.Context.Index, this.Name);
    }
}