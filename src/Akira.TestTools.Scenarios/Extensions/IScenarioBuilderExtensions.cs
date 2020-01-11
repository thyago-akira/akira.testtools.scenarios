using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;

namespace Akira.TestTools.Scenarios.Extensions
{
    public static class IScenarioBuilderExtensions
    {
        /// <summary>
        /// Add a new Scenario Context to the <see cref="IScenariosBuilder{T}.BuilderRepository" />
        /// </summary>
        /// <param name="scenariosBuilder">Builder that will store a new Scenario Context</param>
        /// <param name="scenarioContextName">The name of new scenario context. Must be unique.</param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public static IScenariosBuilder<T> ScenarioContext<T>(
            this IScenariosBuilder<T> scenariosBuilder,
            string scenarioContextName)
            where T : class
        {
            scenariosBuilder.BuilderRepository.AddScenarioContext(scenarioContextName);

            return scenariosBuilder;
        }

        /// <summary>
        /// Add a new Valid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="scenarioRuleSetAction">The actions that will be executed to the Valid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public static IScenariosBuilder<T> DefaultContextValidScenario<T>(
            this IScenariosBuilder<T> scenariosBuilder,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
            where T : class
        {
            scenariosBuilder.BuilderRepository.AddScenario(
                true,
                Defaults.ScenarioValidName,
                scenarioRuleSetAction,
                scenarioType);

            return scenariosBuilder;
        }

        /// <summary>
        /// Add a new Invalid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="scenarioRuleSetAction">The actions that will be executed to the Invalid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public static IScenariosBuilder<T> DefaultContextInvalidScenario<T>(
            this IScenariosBuilder<T> scenariosBuilder,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
            where T : class
        {
            scenariosBuilder.BuilderRepository.AddScenario(
                true,
                Defaults.ScenarioInvalidName,
                scenarioRuleSetAction,
                scenarioType);

            return scenariosBuilder;
        }

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioRuleSetAction">The actions that will be executed to the current Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public static IScenariosBuilder<T> Scenario<T>(
            this IScenariosBuilder<T> scenariosBuilder,
            string scenarioName,
            Action<IScenarioRuleSet<T>> scenarioRuleSetAction,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
            where T : class
        {
            scenariosBuilder.BuilderRepository.AddScenario(
                false,
                scenarioName,
                scenarioRuleSetAction,
                scenarioType);

            return scenariosBuilder;
        }

        /// <summary>
        /// Add a Known Valid Scenario Combination to the <see cref="IScenariosBuilder{T}.BuilderRepository" />
        /// </summary>
        /// <param name="knownScenarioCombinationConfiguration">
        /// A dictionary with the Known Scenario Combination Configuration that can used to build a model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <param name="scenarioCombinationType">
        /// Indicates if the current Known Scenario Combination Configuration will be
        /// <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or
        /// <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public static IScenariosBuilder<T> KnownScenarioCombination<T>(
            this IScenariosBuilder<T> scenariosBuilder,
            IDictionary<string, string> knownScenarioCombinationConfiguration,
            ScenarioCombinationType scenarioCombinationType = ScenarioCombinationType.Unknown)
            where T : class
        {
            scenariosBuilder.BuilderRepository.AddKnownScenarioCombination(
                knownScenarioCombinationConfiguration,
                scenarioCombinationType);

            return scenariosBuilder;
        }
    }
}