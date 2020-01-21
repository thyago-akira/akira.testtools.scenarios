using System;
using System.Collections;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.InternalModels;

namespace Akira.TestTools.Scenarios.Collections
{
    public class ScenarioContextSet : IEnumerable<ScenarioContext>
    {
        #region Fields

        private readonly Dictionary<string, ScenarioContext> contexts =
            new Dictionary<string, ScenarioContext>(StringComparer.OrdinalIgnoreCase);

        private int currentScenarioContextIndex;

        private string currentScenarioContextName;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the number of distinct possible <see cref="ICompletedModelBuilder<T>"/> for the current <see cref="ScenariosRepository{T}" />
        /// </summary>
        public ulong CountCompletedModelBuilders { get; private set; } = 1;

        public bool CurrentScenarioContextIsDefault =>
            this.CurrentScenarioContext.CurrentScenarioContextIsDefault;

        private ScenarioContext CurrentScenarioContext
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.currentScenarioContextName))
                {
                    return this.contexts[this.currentScenarioContextName];
                }

                return null;
            }
        }

        private bool HasScenarioContext =>
            this.currentScenarioContextIndex > 0;

        #endregion Properties

        #region Methods

        #region Public Methods

        public IEnumerator<ScenarioContext> GetEnumerator() =>
            this.contexts.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            this.contexts.Values.GetEnumerator();

        public void AddScenarioContext(
            string scenarioContextName)
        {
            this.CurrentScenarioContext?.ValidateContextCompleted();

            var cleanedContextName = this.ValidateContextName(scenarioContextName);

            this.AddContext(cleanedContextName);
        }

        public bool ContainsScenarioContext(string scenarioContextName)
        {
            return this.contexts.ContainsKey(scenarioContextName);
        }

        /// <summary>
        /// Validate the Scenario and returns the Scenario Key
        /// </summary>
        /// <param name="hasDefaultScenarioContext">
        /// Flag that indicates if the method was called by a Default Scenario Context method (true)
        /// or a Custom Scenario Context method (false)
        /// </param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>The Scenario Key</returns>
        public ScenarioKey AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType)
        {
            var scenarioKey = this.CurrentScenarioContext.AddScenario(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            if (this.CurrentScenarioContext.ScenariosCount > 1)
            {
                if (this.CurrentScenarioContext.ScenariosCount > 2)
                {
                    this.CountCompletedModelBuilders /= (ulong)(this.CurrentScenarioContext.ScenariosCount - 1);
                }

                this.CountCompletedModelBuilders *= (ulong)this.CurrentScenarioContext.ScenariosCount;
            }

            return scenarioKey;
        }

        public void ValidateCurrentContextCompleted() =>
            this.CurrentScenarioContext.ValidateContextCompleted();

        #endregion Public Methods

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

            if (this.ContainsScenarioContext(
                cleanedScenarioContextName))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioContextNameAlreadyExists,
                        cleanedScenarioContextName));
            }

            return cleanedScenarioContextName;
        }

        /// <summary>
        /// Add a new Scenario Context, initializing all the information related to it
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        /// <param name="validateName">Indicates if the name will be validated</param>
        private void AddContext(string scenarioContextName)
        {
            this.currentScenarioContextName = scenarioContextName;

            this.contexts.Add(
                this.currentScenarioContextName,
                new ScenarioContext(
                    this.currentScenarioContextName,
                    ++this.currentScenarioContextIndex));
        }

        #endregion Private Methods

        #endregion Methods
    }
}