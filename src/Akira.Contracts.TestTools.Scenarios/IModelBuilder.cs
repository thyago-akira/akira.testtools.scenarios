using System;
using Akira.Contracts.TestTools.Scenarios.Collections;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IModelBuilder<T>
        where T : class
    {
        string Key { get; }

        void ExecuteAction(
            string scenarioActionKey,
            Action<IScenarioRuleSet<T>> action);

        T Generate();
    }
}