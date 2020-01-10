using System;
using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IScenarioActionsCollection<T>
        where T : class
    {
        ICompletedModelBuilder<T> GetCompletedModelBuilderByKey(IEnumerable<string> actionKeys);

        void ValidateAction(Action<IScenarioRuleSet<T>> action);

        void AddValidAction(string key, Action<IScenarioRuleSet<T>> action);
    }
}