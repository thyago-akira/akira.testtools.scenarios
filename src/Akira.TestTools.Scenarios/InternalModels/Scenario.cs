using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.InternalModels
{
    internal class Scenario<T>
        where T : class
    {
        internal readonly ScenarioContext<T> ScenarioContext;

        internal readonly string Name;

        internal readonly string Key;

        internal Scenario(
            ScenarioContext<T> scenarioContext,
            string name)
        {
            this.ScenarioContext = scenarioContext;
            this.Name = name.Trim();
            this.Key = Keys.GetScenarioContextKeyValue(
                this.ScenarioContext.Index,
                this.Name);
        }

        public override bool Equals(object obj)
        {
            return obj is Scenario<T> scenarioObject &&
                scenarioObject.ScenarioContext == this.ScenarioContext &&
                scenarioObject.Name == this.Name;
        }

        public override int GetHashCode() => this.Name.GetHashCode();
    }
}