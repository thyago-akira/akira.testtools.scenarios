using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.InternalModels
{
    internal class ScenarioContextSet<T>
        where T : class
    {
        #region Fields

        private readonly Dictionary<string, int> scenarioContextsIndexes =
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        private int currentIndex = -1;

        #endregion Fields

        #region Constructors

        internal ScenarioContextSet(string firstContextName)
        {
            this.AddScenarioContext(firstContextName);
        }

        internal ScenarioContextSet()
        {
        }

        #endregion Constructors

        #region Properties

        internal readonly List<ScenarioContext<T>> ScenarioContexts =
            new List<ScenarioContext<T>>();

        internal readonly Dictionary<string, Action<IScenarioRuleSet<T>>> ScenariosActions =
            new Dictionary<string, Action<IScenarioRuleSet<T>>>(StringComparer.OrdinalIgnoreCase);

        private bool HasScenarioContext => this.currentIndex >= 0;

        private ScenarioContext<T> CurrentScenarioContext
        {
            get
            {
                if (this.currentIndex >= 0)
                {
                    return this.ScenarioContexts[this.currentIndex];
                }

                return null;
            }
        }

        #endregion Properties

        #region Methods

        #region Internal Methods

        internal void AddScenarioContext(
            string scenarioContextName)
        {
            this.CurrentScenarioContext?.ValidateContextCompleted();

            var cleanedContextName = this.ValidateContextName(scenarioContextName);

            this.AddContext(cleanedContextName);
        }

        internal bool ContainsScenarioContext(
            string cleanedScenarioContextName)
        {
            return this.scenarioContextsIndexes.ContainsKey(
                cleanedScenarioContextName);
        }

        internal string AddScenario(
            string scenarioName,
            Action<IScenarioRuleSet<T>> action)
        {
            this.ValidateAction(action);

            var scenarioKey = this.CurrentScenarioContext.AddScenario(
                scenarioName);

            this.ScenariosActions.Add(
                scenarioKey,
                action);

            return scenarioKey;
        }

        internal string AddValidScenario(
            Action<IScenarioRuleSet<T>> action)
        {
            return this.AddScenario(
                Defaults.ScenarioValidName,
                action);
        }

        internal string AddInvalidScenario(
            Action<IScenarioRuleSet<T>> action)
        {
            return this.AddScenario(
                Defaults.ScenarioInvalidName,
                action);
        }

        #endregion Internal Methods

        #region Private Methods

        /// <summary>
        /// Validate the new Scenario Context Name
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        /// <returns>
        /// Returns the cleaned Scenario Context Name
        /// </returns>
        private string ValidateContextName(string scenarioContextName)
        {
            if (string.IsNullOrWhiteSpace(scenarioContextName))
            {
                throw new ArgumentException(
                    Errors.ScenarioContextNameIsnotSet);
            }

            var cleanedScenarioContextName = scenarioContextName.Trim();

            if (this.HasScenarioContext &&
                string.Equals(
                Defaults.ScenarioContextName,
                cleanedScenarioContextName,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioContextNameAsDefaultIsnotAllowed,
                        cleanedScenarioContextName));
            }

            if (this.ContainsScenarioContext(cleanedScenarioContextName))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioContextNameAlreadyExists,
                        cleanedScenarioContextName));
            }

            return cleanedScenarioContextName;
        }

        private void ValidateAction(Action<IScenarioRuleSet<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentException(Errors.ScenarioActionIsnotSet);
            }
        }

        private void AddContext(string scenarioContextName)
        {
            var scenarioContext = new ScenarioContext<T>(
                ++this.currentIndex,
                scenarioContextName);

            this.ScenarioContexts.Add(scenarioContext);

            this.scenarioContextsIndexes.Add(
                scenarioContext.Name,
                scenarioContext.Index);
        }

        #endregion Private Methods

        #endregion Methods
    }
}