using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios
{
    public class ScenariosFaker<T> : ScenariosBuilder<T>
        where T : class
    {
        public ScenariosFaker() : base(new ScenariosRepository<T>(Defaults.ScenarioContextName))
        {
        }
    }
}