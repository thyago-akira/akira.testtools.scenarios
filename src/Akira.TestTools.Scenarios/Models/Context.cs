using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.Contracts.TestTools.Scenarios.Models;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;

namespace Akira.TestTools.Scenarios.Models
{
    public class Context : IContext
    {
        #region Fields

        private readonly Dictionary<string, Scenario> scenarios =
            new Dictionary<string, Scenario>(StringComparer.OrdinalIgnoreCase);

        private Random random;

        #endregion Fields

        #region Constructors

        internal Context(
            string name,
            int index,
            int minimumScenariosByScenarioContext = 2)
        {
            this.Name = name.Trim();
            this.Index = index;
            this.MinimumScenariosByScenarioContext = minimumScenariosByScenarioContext;
            this.ContextIsDefault = string.Equals(
                Defaults.ContextName,
                this.Name,
                StringComparison.OrdinalIgnoreCase);
        }

        #endregion Constructors

        #region Properties

        public int MinimumScenariosByScenarioContext { get; }

        public int Index { get; }

        public string Name { get; }

        public int ScenariosCount => this.scenarios.Count;

        public bool ContextIsDefault { get; }

        private Random Random
        {
            get
            {
                if (this.random == null)
                {
                    this.random = new Random();
                }

                return this.random;
            }
        }

        #endregion Properties

        #region Methods

        public IScenario GetScenario(string scenarioName)
        {
            if (this.scenarios.ContainsKey(scenarioName.Trim()))
            {
                return this.scenarios[scenarioName.Trim()];
            }

            return default;
        }

        public IScenario GetScenarioOrRandom(
            string scenarioName)
        {
            var scenario = this.GetScenario(scenarioName);

            if (scenario != default)
            {
                return scenario;
            }

            if (this.scenarios.Count == 1)
            {
                return this.scenarios.First().Value;
            }

            var randomIndex = this.Random.Next(this.ScenariosCount);

            return this.scenarios.ToArray()[randomIndex].Value;
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
        /// Indicates if the Current Scenario will be <see cref="ScenarioType.Unknown"/>, <see cref="ScenarioType.AlwaysValid"/> or <see cref="ScenarioType.AlwaysInvalid"/>
        /// </param>
        /// <returns>The Scenario Key</returns>
        public IScenario AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioType scenarioType)
        {
            this.ValidateDefaultScenarioContext(
                hasDefaultScenarioContext);

            this.ValidateScenarioType(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            var cleanedScenarioName = this.ValidateScenarioName(scenarioName);

            return this.AddScenario(cleanedScenarioName);
        }

        /// <summary>
        /// Validate the Current Scenario Context, checking if it has the minimum required scenarios
        /// </summary>
        public void ValidateContextCompleted()
        {
            if (this.scenarios.Count >= this.MinimumScenariosByScenarioContext)
            {
                return;
            }

            if (!this.ContextIsDefault)
            {
                throw new InvalidOperationException(
                    string.Format(
                        Errors.ScenarioContextIncomplete,
                        this.Name));
            }

            if (!this.scenarios.ContainsKey(
                Defaults.ScenarioValidName))
            {
                throw new InvalidOperationException(
                    Errors.DefaultScenarioContextWithoutValidScenario);
            }

            throw new InvalidOperationException(
                Errors.DefaultScenarioContextWithoutInvalidScenario);
        }

        private string ValidateScenarioName(string scenarioName)
        {
            if (string.IsNullOrWhiteSpace(scenarioName))
            {
                throw new ArgumentException(Errors.ScenarioNameIsnotSet);
            }

            var cleanedScenarioName = scenarioName.Trim();

            if (this.scenarios.ContainsKey(cleanedScenarioName))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioNameAlreadyExists,
                        cleanedScenarioName,
                        this.Name));
            }

            return cleanedScenarioName;
        }

        /// <summary>
        /// Validate the Scenario Type
        /// </summary>
        /// <param name="hasDefaultScenarioContext">
        /// Flag that indicates if the method was called by a Default Scenario Context method (true)
        /// or a Custom Scenario Context method (false)
        /// </param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioType.Unknown"/>, <see cref="ScenarioType.AlwaysValid"/> or <see cref="ScenarioType.AlwaysInvalid"/>
        /// </param>
        private void ValidateScenarioType(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioType scenarioType)
        {
            if (!EnumExtensions.AllowedScenarioTypes.Contains((int)scenarioType))
            {
                throw new ArgumentException(Errors.ScenarioTypeInvalid);
            }

            if (hasDefaultScenarioContext)
            {
                if (scenarioType == ScenarioType.AlwaysInvalid &&
                    scenarioName == Defaults.ScenarioValidName)
                {
                    throw new ArgumentException(
                        Errors.ScenarioTypeInvalidForValidDefaultContextScenario);
                }

                if (scenarioType == ScenarioType.AlwaysValid &&
                    scenarioName == Defaults.ScenarioInvalidName)
                {
                    throw new ArgumentException(
                        Errors.ScenarioTypeInvalidForInvalidDefaultContextScenario);
                }
            }
        }

        /// <summary>
        /// Validate if the add Scenario method was called in the wrong Scenario Context
        /// </summary>
        /// <param name="useDefaultScenarioContext">
        /// Flag that indicates if the method was called by a Default Scenario Context method (true)
        /// or a Custom Scenario Context method (false)
        /// </param>
        private void ValidateDefaultScenarioContext(bool useDefaultScenarioContext)
        {
            // Check the add Scenario calls done for the Default Scenario Context
            // when the Current Scenario Context is a Custom Scenario Context
            if (useDefaultScenarioContext &&
                !this.ContextIsDefault)
            {
                throw new InvalidOperationException(
                    Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst);
            }

            // Check the add Scenario calls done for the Custom Scenario Context
            // when the Current Scenario Context is the Default Scenario Context
            if (!useDefaultScenarioContext &&
                this.ContextIsDefault)
            {
                throw new InvalidOperationException(
                    Errors.ScenariosForDefaultScenarioContextMustBeSetInOtherMethods);
            }
        }

        /// <summary>
        /// Validate and Add the Scenario name, and then returns the Scenario Key
        /// </summary>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <returns>The Scenario Key</returns>
        private IScenario AddScenario(string scenarioName)
        {
            var scenario = new Scenario(
                this,
                scenarioName);

            this.scenarios.Add(
                scenario.Name,
                scenario);

            return scenario;
        }

        #endregion Methods
    }
}