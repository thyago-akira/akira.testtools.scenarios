using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.Collections
{
    public class ScenarioActionsCollection<T> : IScenarioActionsCollection<T>
        where T : class
    {
        #region Fields

        private readonly Dictionary<string, Action<IScenarioRuleSet<T>>> scenariosActions =
            new Dictionary<string, Action<IScenarioRuleSet<T>>>(StringComparer.OrdinalIgnoreCase);

        #endregion Fields

        #region Methods

        public void AddValidAction(
            string key,
            Action<IScenarioRuleSet<T>> action)
        {
            this.scenariosActions.Add(
                key,
                action);
        }

        public virtual ICompletedModelBuilder<T> GetCompletedModelBuilderByKey(
            IEnumerable<string> actionKeys)
        {
            var fullActionsKey = string.Concat(actionKeys);

            var scenarioFaker = new InternalFaker<T>(fullActionsKey);

            foreach (var actionKey in actionKeys)
            {
                var action = this.scenariosActions[actionKey];

                action(scenarioFaker);
            }

            return scenarioFaker;
        }

        public void ValidateAction(
            Action<IScenarioRuleSet<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentException(
                    Errors.ScenarioActionIsnotSet);
            }
        }

        #endregion Methods
    }
}