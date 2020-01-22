using Akira.Contracts.TestTools.Scenarios.Enums;

namespace Akira.Contracts.TestTools.Scenarios.Models
{
    public interface IScenarioContext
    {
        string Name { get; }

        int Index { get; }

        bool CurrentScenarioContextIsDefault { get; }

        int ScenariosCount { get; }

        IScenario GetScenario(string scenarioName);

        IScenario GetScenarioOrRandom(string scenarioName);

        IScenario AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType);

        void ValidateContextCompleted();
    }
}