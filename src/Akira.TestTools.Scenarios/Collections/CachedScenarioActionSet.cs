using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios.Collections
{
    public class CachedScenarioActionSet<T, B> : ScenarioActionSet<T, B>
        where T : class
        where B : IModelBuilder<T>, new()
    {
        private readonly ConcurrentDictionary<string, IModelBuilder<T>> cachedCompletedModelBuilder =
            new ConcurrentDictionary<string, IModelBuilder<T>>(StringComparer.OrdinalIgnoreCase);

        public override IModelBuilder<T> GetModelBuilder(
            IEnumerable<string> scenarioRuleSetActionKeys)
        {
            var fullActionsKey = string.Concat(scenarioRuleSetActionKeys);

            if (!this.cachedCompletedModelBuilder.TryGetValue(
                fullActionsKey,
                out var completedModelBuilder))
            {
                completedModelBuilder = base.GetModelBuilder(scenarioRuleSetActionKeys);

                _ = this.cachedCompletedModelBuilder.TryAdd(
                    completedModelBuilder.Key,
                    completedModelBuilder);
            }

            return completedModelBuilder;
        }
    }
}