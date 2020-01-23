using System;
using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios.Collections
{
    public interface IScenarioActionSet<T>
        where T : class
    {
        IModelBuilder<T> GetModelBuilder(
            IEnumerable<string> scenarioActionKeys);

        void ValidateScenarioAction(
            Action<IScenarioRuleSet<T>> scenarioAction);

        void AddValidScenarioAction(
            string scenarioActionKey,
            Action<IScenarioRuleSet<T>> scenarioAction);
    }
}