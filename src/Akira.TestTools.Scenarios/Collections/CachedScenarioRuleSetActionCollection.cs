using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios.Collections
{
    public class CachedScenarioRuleSetActionCollection<T> : ScenarioRuleSetActionCollection<T>
        where T : class
    {
        private readonly ConcurrentDictionary<string, ICompletedModelBuilder<T>> cachedCompletedModelBuilder =
            new ConcurrentDictionary<string, ICompletedModelBuilder<T>>(StringComparer.OrdinalIgnoreCase);

        public override ICompletedModelBuilder<T> GetCompletedModelBuilderByKey(
            IEnumerable<string> scenarioRuleSetActionKeys)
        {
            var fullActionsKey = string.Concat(scenarioRuleSetActionKeys);

            if (!this.cachedCompletedModelBuilder.TryGetValue(
                fullActionsKey,
                out var completedModelBuilder))
            {
                completedModelBuilder = base.GetCompletedModelBuilderByKey(scenarioRuleSetActionKeys);

                _ = this.cachedCompletedModelBuilder.TryAdd(
                    completedModelBuilder.Key,
                    completedModelBuilder);
            }

            return completedModelBuilder;
        }
    }
}