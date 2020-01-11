using System;
using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IScenarioRuleSetActionCollection<T>
        where T : class
    {
        ICompletedModelBuilder<T> GetCompletedModelBuilderByKey(
            IEnumerable<string> scenarioRuleSetActionKeys);

        void ValidateScenarioRuleSetAction(
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction);

        void AddValidScenarioRuleSetAction(
            string scenarioRuleSetActionKey,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction);
    }
}