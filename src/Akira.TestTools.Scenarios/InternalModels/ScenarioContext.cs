using System;
using System.Collections.Generic;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.InternalModels
{
    internal class ScenarioContext<T>
        where T : class
    {
        internal readonly int MinimumScenariosByScenarioContext;

        internal readonly int Index;

        internal readonly string Name;

        private readonly HashSet<Scenario<T>> scenarios =
            new HashSet<Scenario<T>>();

        internal ScenarioContext(
            int index,
            string name,
            int minimumScenariosByScenarioContext = 2)
        {
            this.Index = index;
            this.Name = name;
            this.MinimumScenariosByScenarioContext = minimumScenariosByScenarioContext;
        }

        internal string AddScenario(string scenarioName)
        {
            var scenario = new Scenario<T>(
                this,
                scenarioName.Trim());

            _ = this.scenarios.Add(
                scenario);

            return scenario.Key;
        }

        internal virtual void ValidateContextCompleted()
        {
            if (this.scenarios.Count >= this.MinimumScenariosByScenarioContext)
            {
                return;
            }

            throw new InvalidOperationException(
                string.Format(
                    Errors.ScenarioContextIncomplete,
                    this.Name));
        }
    }
}