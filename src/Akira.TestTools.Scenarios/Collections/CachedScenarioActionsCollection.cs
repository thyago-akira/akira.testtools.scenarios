using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;

namespace Akira.TestTools.Scenarios.Collections
{
    public class CachedScenarioActionsCollection<T> : ScenarioActionsCollection<T>
        where T : class
    {
        private readonly ConcurrentDictionary<string, ICompletedModelBuilder<T>> cachedCompletedModelBuilder =
            new ConcurrentDictionary<string, ICompletedModelBuilder<T>>(StringComparer.OrdinalIgnoreCase);

        public override ICompletedModelBuilder<T> GetCompletedModelBuilderByKey(
            IEnumerable<string> actionKeys)
        {
            var fullActionsKey = string.Concat(actionKeys);

            if (!this.cachedCompletedModelBuilder.TryGetValue(
                fullActionsKey,
                out var completedModelBuilder))
            {
                completedModelBuilder = base.GetCompletedModelBuilderByKey(actionKeys);

                _ = this.cachedCompletedModelBuilder.TryAdd(
                    completedModelBuilder.Key,
                    completedModelBuilder);
            }

            return completedModelBuilder;
        }
    }
}