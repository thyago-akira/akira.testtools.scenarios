using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.Collections
{
    public class ScenarioRuleSetActionCollection<T> : IScenarioRuleSetActionCollection<T>
        where T : class
    {
        #region Fields

        private readonly Dictionary<string, Action<IScenarioRuleSet<T>>> scenariosRuleSetActions =
            new Dictionary<string, Action<IScenarioRuleSet<T>>>(StringComparer.OrdinalIgnoreCase);

        #endregion Fields

        #region Methods

        public void AddValidScenarioRuleSetAction(
            string scenarioRuleSetActionKey,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction)
        {
            this.scenariosRuleSetActions.Add(
                scenarioRuleSetActionKey,
                scenarioRuleSetAction);
        }

        public virtual ICompletedModelBuilder<T> GetCompletedModelBuilderByKey(
            IEnumerable<string> scenarioRuleSetActionKeys)
        {
            var completedModelBuilderKey = string.Concat(scenarioRuleSetActionKeys);

            var completedModelBuilder = new InternalFaker<T>(completedModelBuilderKey);

            foreach (var scenarioRuleSetActionKey in scenarioRuleSetActionKeys)
            {
                var scenarioRuleSetAction = this.scenariosRuleSetActions[scenarioRuleSetActionKey];

                scenarioRuleSetAction(completedModelBuilder);
            }

            return completedModelBuilder;
        }

        public void ValidateScenarioRuleSetAction(
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction)
        {
            if (scenarioRuleSetAction == null)
            {
                throw new ArgumentException(
                    Errors.ScenarioRuleSetActionIsnotSet);
            }
        }

        #endregion Methods
    }
}