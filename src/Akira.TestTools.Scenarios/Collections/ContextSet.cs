using System;
using System.Collections;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;
using Akira.TestTools.Scenarios.Models;
using static Akira.TestTools.Scenarios.Constants.Errors.Context;

namespace Akira.TestTools.Scenarios.Collections
{
    public class ContextSet : IContextSet
    {
        #region Fields

        private readonly Dictionary<string, IContext> contexts =
            new Dictionary<string, IContext>(StringComparer.OrdinalIgnoreCase);

        private string currentContextName;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the number of distinct possible Scenarios Combinations
        /// </summary>
        public ulong CountScenariosCombinations { get; private set; } = 1;

        public bool CurrentScenarioContextIsDefault =>
            this.CurrentScenarioContext.ContextIsDefault;

        private IContext CurrentScenarioContext
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.currentContextName))
                {
                    return this.contexts[this.currentContextName];
                }

                return null;
            }
        }

        #endregion Properties

        #region Methods

        #region Public Methods

        public IEnumerator<IContext> GetEnumerator() =>
            this.contexts.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            this.contexts.Values.GetEnumerator();

        /// <summary>
        /// Add a new Scenario Context to the <see cref="ContextSet" />
        /// </summary>
        /// <param name="contextName">The name of new scenario context. Must be unique.</param>
        public void AddContext(
            string contextName)
        {
            this.currentContextName = this.ValidateContext(contextName);

            this.contexts.Add(
                this.currentContextName,
                new Context(
                    this.currentContextName,
                    this.contexts.Count + 1));
        }

        public bool ContainsContext(string contextName) =>
            this.contexts.ContainsKey(contextName);

        /// <summary>
        /// Validate the Scenario and returns the Scenario Key
        /// </summary>
        /// <param name="hasDefaultScenarioContext">
        /// Flag that indicates if the method was called by a Default Scenario Context method (true)
        /// or a Custom Scenario Context method (false)
        /// </param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioType.Unknown"/>, <see cref="ScenarioType.AlwaysValid"/> or <see cref="ScenarioType.AlwaysInvalid"/>
        /// </param>
        /// <returns>The Scenario Key</returns>
        public IScenario AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioType scenarioType)
        {
            var scenario = this.CurrentScenarioContext.AddScenario(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            if (this.CurrentScenarioContext.ScenariosCount > 1)
            {
                if (this.CurrentScenarioContext.ScenariosCount > 2)
                {
                    this.CountScenariosCombinations /= (ulong)(this.CurrentScenarioContext.ScenariosCount - 1);
                }

                this.CountScenariosCombinations *= (ulong)this.CurrentScenarioContext.ScenariosCount;
            }

            return scenario;
        }

        public void ValidateCurrentContextCompleted() =>
            this.CurrentScenarioContext.ValidateContextCompleted();

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Validate the new Scenario Context Information
        /// </summary>
        /// <param name="contextName">The name of new scenario context</param>
        /// <returns>
        /// Returns the cleaned Scenario Context Name
        /// </returns>
        private string ValidateContext(string contextName)
        {
            // Before adding a new context, check the previous one
            this.CurrentScenarioContext?.ValidateContextCompleted();

            if (string.IsNullOrWhiteSpace(contextName))
            {
                throw new ArgumentException(NameIsnotSet);
            }

            var cleanedScenarioContextName = contextName.Trim();

            if (this.contexts.Count > 0 &&
                string.Equals(
                    Defaults.ContextName,
                    cleanedScenarioContextName,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(NameAsDefaultIsnotAllowed.Format(cleanedScenarioContextName));
            }

            if (this.ContainsContext(
                cleanedScenarioContextName))
            {
                throw new ArgumentException(NameAlreadyExists.Format(cleanedScenarioContextName));
            }

            return cleanedScenarioContextName;
        }

        #endregion Private Methods

        #endregion Methods
    }
}