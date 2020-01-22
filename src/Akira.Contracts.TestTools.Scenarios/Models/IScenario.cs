namespace Akira.Contracts.TestTools.Scenarios.Models
{
    public interface IScenario
    {
        IScenarioContext ScenarioContext { get; }

        string Name { get; }

        string Key { get; }
    }
}