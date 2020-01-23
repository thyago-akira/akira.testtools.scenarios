using Akira.Contracts.TestTools.Scenarios.Enums;

namespace Akira.Contracts.TestTools.Scenarios.Models
{
    public interface IContext
    {
        string Name { get; }

        int Index { get; }

        bool ContextIsDefault { get; }

        int ScenariosCount { get; }

        IScenario GetScenario(string scenarioName);

        IScenario GetScenarioOrRandom(string scenarioName);

        IScenario AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioType scenarioType);

        void ValidateContextCompleted();
    }
}