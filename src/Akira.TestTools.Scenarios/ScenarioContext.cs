using System;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;

namespace Akira.TestTools.Scenarios
{
    internal class ScenarioContext
    {
        private const int MinimumScenariosByScenarioContext = 2;

        private readonly HashSet<string> scenarios;

        private Random random;

        internal ScenarioContext(
            string name,
            int index)
        {
            this.Name = name?.Trim();
            this.Index = index;
            this.scenarios = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        internal int ScenariosCount => this.scenarios.Count;

        internal bool IsCurrentScenarioContextDefaultContext => this.Index == 1;

        internal string Name { get; private set; }

        internal int Index { get; private set; }

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

        internal ScenarioKey GetScenarioKey(
            string scenarioName,
            bool findRandomScenario = false)
        {
            if (this.scenarios.Contains(scenarioName.Trim()))
            {
                return new ScenarioKey(
                    this.Name,
                    this.Index,
                    scenarioName.Trim());
            }

            if (findRandomScenario)
            {
                var randomIndex = this.Random.Next(this.scenarios.Count);

                return new ScenarioKey(
                    this.Name,
                    this.Index,
                    this.scenarios.ToArray()[randomIndex]);
            }

            return default;
        }

        /// <summary>
        /// Validate the Current Scenario Context, checking if it has the minimum required scenarios
        /// </summary>
        internal void ValidateContextCompleted()
        {
            if (this.scenarios.Count >= MinimumScenariosByScenarioContext)
            {
                return;
            }

            if (!this.IsCurrentScenarioContextDefaultContext)
            {
                throw new InvalidOperationException(
                    string.Format(
                        Errors.ScenarioContextIncomplete,
                        this.Name));
            }

            if (!this.scenarios.Contains(
                Defaults.ScenarioValidName))
            {
                throw new InvalidOperationException(
                    Errors.DefaultScenarioContextWithoutValidScenario);
            }

            throw new InvalidOperationException(
                Errors.DefaultScenarioContextWithoutInvalidScenario);
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
        internal ScenarioKey AddScenario(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType)
        {
            this.ValidateDefaultScenarioContext(
                hasDefaultScenarioContext);

            this.ValidateScenarioType(
                hasDefaultScenarioContext,
                scenarioName,
                scenarioType);

            return this.AddScenario(scenarioName);
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
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        private void ValidateScenarioType(
            bool hasDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType)
        {
            if (!EnumExtensions.AllowedScenarioTypes.Contains((int)scenarioType))
            {
                throw new ArgumentException(Errors.ScenarioTypeInvalid);
            }

            if (hasDefaultScenarioContext)
            {
                if (scenarioType == ScenarioCombinationType.AlwaysInvalid &&
                    scenarioName == Defaults.ScenarioValidName)
                {
                    throw new ArgumentException(
                        Errors.ScenarioTypeInvalidForValidDefaultContextScenario);
                }

                if (scenarioType == ScenarioCombinationType.AlwaysValid &&
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
                !this.IsCurrentScenarioContextDefaultContext)
            {
                throw new InvalidOperationException(
                    Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst);
            }

            // Check the add Scenario calls done for the Custom Scenario Context
            // when the Current Scenario Context is the Default Scenario Context
            if (!useDefaultScenarioContext &&
                this.IsCurrentScenarioContextDefaultContext)
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
        private ScenarioKey AddScenario(string scenarioName)
        {
            if (string.IsNullOrWhiteSpace(scenarioName))
            {
                throw new ArgumentException(Errors.ScenarioNameIsnotSet);
            }

            var cleanedScenarioName = scenarioName.Trim();

            if (!this.scenarios.Add(cleanedScenarioName))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioNameAlreadyExists,
                        cleanedScenarioName,
                        this.Name));
            }

            return new ScenarioKey(
                this.Name,
                this.Index,
                cleanedScenarioName);
        }
    }
}