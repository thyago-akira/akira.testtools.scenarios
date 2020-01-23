using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.Collections
{
    public class ScenarioActionSet<T, B> : IScenarioActionSet<T>
        where T : class
        where B : IModelBuilder<T>, new()
    {
        #region Fields

        private readonly Dictionary<string, Action<IScenarioRuleSet<T>>> scenariosRuleSetActions =
            new Dictionary<string, Action<IScenarioRuleSet<T>>>(
                StringComparer.OrdinalIgnoreCase);

        #endregion Fields

        #region Methods

        public void AddValidScenarioAction(
            string scenarioRuleSetActionKey,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction)
        {
            this.scenariosRuleSetActions.Add(
                scenarioRuleSetActionKey,
                scenarioRuleSetAction);
        }

        public virtual IModelBuilder<T> GetModelBuilder(
            IEnumerable<string> scenarioActionKeys)
        {
            var completedModelBuilder = new B();

            foreach (var scenarioRuleSetActionKey in scenarioActionKeys)
            {
                if (this.scenariosRuleSetActions.TryGetValue(
                    scenarioRuleSetActionKey,
                    out var scenarioRuleSetAction))
                {
                    completedModelBuilder.ExecuteAction(
                        scenarioRuleSetActionKey,
                        scenarioRuleSetAction);
                }
            }

            return completedModelBuilder;
        }

        public void ValidateScenarioAction(
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