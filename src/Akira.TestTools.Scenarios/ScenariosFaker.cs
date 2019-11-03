using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;

namespace Akira.TestTools.Scenarios
{
    public class ScenariosFaker<T> : IScenariosBuilder<T>
        where T : class
    {
        #region Constants

        private static readonly int MinimumScenariosByScenarioContext = 2;

        #endregion Constants

        #region Fields

        private static readonly HashSet<int> AllowedScenarioBuilderTypes = new HashSet<int>((int[])Enum.GetValues(typeof(ScenarioBuilderType)));

        private static readonly HashSet<int> AllowedScenarioTypes = new HashSet<int>((int[])Enum.GetValues(typeof(ScenarioCombinationType)));

        private readonly ConcurrentDictionary<string, InternalFaker<T>> existingFakers =
            new ConcurrentDictionary<string, InternalFaker<T>>();

        private readonly Bogus.Faker faker =
            new Bogus.Faker();

        private readonly Dictionary<string, (ScenarioCombinationType, Dictionary<string, string>)> knownScenarioCombinationConfiguration =
            new Dictionary<string, (ScenarioCombinationType, Dictionary<string, string>)>();

        private readonly Dictionary<string, Dictionary<string, Action<IScenarioRuleSet<T>>>> scenarioContextsActions =
            new Dictionary<string, Dictionary<string, Action<IScenarioRuleSet<T>>>>();

        private string currentScenarioContextName;

        private int currentScenarioContextIndex;

        private Dictionary<string, Action<IScenarioRuleSet<T>>> currentScenarioContextDictionary;

        #endregion Fields

        #region Constructors

        public ScenariosFaker()
        {
            this.AddNewScenarioContext(Defaults.ScenarioContextName);
        }

        #endregion Constructors

        #region Properties

        private bool IsCurrentScenarioContextDefaultContext => this.currentScenarioContextName == Defaults.ScenarioContextName;

        #endregion Properties

        #region Methods

        #region Public Methods

        #region Generate

        public T Generate(
            ScenarioBuilderType scenarioType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioBuilderContext = null)
        {
            this.ValidateScenarioBuilderContext(
                scenarioType,
                scenarioBuilderContext);

            (var fullScenarioKey, var fullScenarioBuilderRules) = this.GetFullScenarioBuilderRules(
                scenarioBuilderContext);

            var scenarioFaker = this.GetOrCreateFakerScenario(
                fullScenarioKey,
                fullScenarioBuilderRules);

            return scenarioFaker.Generate();
        }

        public IEnumerable<T> Generate(
            int count,
            ScenarioBuilderType scenarioType = ScenarioBuilderType.All,
            IDictionary<string, string> scenarioBuilderContext = null)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Invalid number of rows to generate");
            }

            return Enumerable
                .Range(1, count)
                .Select(_ => this.Generate(
                    scenarioType,
                    scenarioBuilderContext));
        }

        public IEnumerable<T> GenerateMinimumTestingScenarios(
            ScenarioBuilderType scenarioType = ScenarioBuilderType.All)
        {
            foreach (var scenarioCombinationConfiguration in this.GetMinimumTestingScenarioCombinations(
                scenarioType))
            {
                yield return this.Generate(
                    scenarioType,
                    scenarioCombinationConfiguration);
            }
        }

        #endregion Generate

        #region ScenarioContext

        /// <summary>
        /// Add a new Scenario Context to the <see cref="IScenariosBuilder{T}" />
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context. Must be unique.</param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> ScenarioContext(string scenarioContextName)
        {
            this.ValidatePreviousScenarioContext();

            var cleanedScenarionName = this.ValidateAndCleanupScenarioContextName(scenarioContextName);

            this.AddNewScenarioContext(cleanedScenarionName);

            return this;
        }

        #endregion ScenarioContext

        #region Scenario

        /// <summary>
        /// Add a new Valid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="action">The actions that will be executed to the Valid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> DefaultContextValidScenario(
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
        {
            return this.Scenario(
                true,
                Defaults.ScenarioValidName,
                scenarioType,
                action);
        }

        /// <summary>
        /// Add a new Invalid Scenario to the current Default Scenario Context
        /// </summary>
        /// <param name="action">The actions that will be executed to the Invalid Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> DefaultContextInvalidScenario(
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
        {
            return this.Scenario(
                true,
                Defaults.ScenarioInvalidName,
                scenarioType,
                action);
        }

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="action">The actions that will be executed to the current Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> Scenario(
            string scenarioName,
            Action<IScenarioRuleSet<T>> action,
            ScenarioCombinationType scenarioType = ScenarioCombinationType.Unknown)
        {
            return this.Scenario(
                false,
                scenarioName,
                scenarioType,
                action);
        }

        #endregion Scenario

        #region Add Known Scenario Builder

        /// <summary>
        /// Add a Known Valid Scenario Combination to the <see cref="IScenariosBuilder{T}" />
        /// </summary>
        /// <param name="scenarioCombinationConfiguration">
        /// A dictionary with the builder parameters that can used to build an <see cref="ScenarioCombinationType.AlwaysValid"/> model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> AddKnownValidScenarioCombination(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            this.AddKnownScenarioCombinationWithValidation(
                ScenarioCombinationType.AlwaysValid,
                scenarioCombinationConfiguration);

            return this;
        }

        /// <summary>
        /// Add a Known Invalid Scenario Combination to the <see cref="IScenariosBuilder{T}" />
        /// </summary>
        /// <param name="scenarioCombinationConfiguration">
        /// A dictionary with the builder parameters that can used to build an <see cref="ScenarioCombinationType.AlwaysInvalid"/> model.
        /// Key: Scenario Context Name
        /// Value: Scenario Name
        /// </param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        public IScenariosBuilder<T> AddKnownInvalidScenarioCombination(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            this.AddKnownScenarioCombinationWithValidation(
                ScenarioCombinationType.AlwaysInvalid,
                scenarioCombinationConfiguration);

            return this;
        }

        #endregion Add Known Scenario Builder

        #endregion Public Methods

        public IEnumerable<IDictionary<string, string>> GetMinimumTestingScenarioCombinations(
            ScenarioBuilderType scenarioType = ScenarioBuilderType.All)
        {
            if (scenarioType == ScenarioBuilderType.All)
            {
                yield return null;

                yield return new Dictionary<string, string>();

                // You should test each rule for each scenario
                foreach (var scenarioAction in this.scenarioContextsActions)
                {
                    foreach (var rule in scenarioAction.Value)
                    {
                        yield return new Dictionary<string, string>
                        {
                            { scenarioAction.Key, rule.Key }
                        };
                    }
                }
            }

            // You should test all known scenarios
            foreach (var knownScenarios in this.knownScenarioCombinationConfiguration)
            {
                if (scenarioType == ScenarioBuilderType.All ||
                   (scenarioType == ScenarioBuilderType.ValidOnly &&
                    knownScenarios.Value.Item1 == ScenarioCombinationType.AlwaysValid) ||
                   (scenarioType == ScenarioBuilderType.InvalidOnly &&
                    knownScenarios.Value.Item1 == ScenarioCombinationType.AlwaysInvalid))
                {
                    yield return knownScenarios.Value.Item2;
                }
            }
        }

        #region Private Methods

        private InternalFaker<T> GetOrCreateFakerScenario(
            string fullScenarioKey,
            IDictionary<string, string> fullScenarioBuilderRules)
        {
            if (!this.existingFakers.TryGetValue(
                fullScenarioKey,
                out var scenarioFaker))
            {
                scenarioFaker = new InternalFaker<T>();

                foreach (var builderRule in fullScenarioBuilderRules)
                {
                    var action = this.scenarioContextsActions[builderRule.Key][builderRule.Value];

                    action(scenarioFaker);
                }

                _ = this.existingFakers.TryAdd(fullScenarioKey, scenarioFaker);
            }

            return scenarioFaker;
        }

        private (string, IDictionary<string, string>) GetFullScenarioBuilderRules(
            IDictionary<string, string> scenarioBuilderContext = null)
        {
            var scenarioKey = string.Empty;
            var buildRules = new Dictionary<string, string>();

            this.ForEachScenarioWithScenarioCompleteValidation((scenarioContextName, keyIndex, scenarioNames) =>
            {
                if (scenarioBuilderContext == null ||
                    !scenarioBuilderContext.TryGetValue(scenarioContextName, out var scenarioBuilderName) ||
                    !scenarioNames.Contains(scenarioBuilderName.NormalizeName()))
                {
                    scenarioBuilderName = this.faker.PickRandom(scenarioNames.ToArray());
                }

                scenarioKey += Keys.GetScenarioContextKeyValue(
                    keyIndex,
                    scenarioBuilderName.NormalizeName());

                buildRules.Add(
                    scenarioContextName,
                    scenarioBuilderName);
            });

            return (scenarioKey, buildRules);
        }

        #region Validate Methods

        private void ValidateScenarioBuilderContext(
            ScenarioBuilderType scenarioType,
            IDictionary<string, string> scenarioBuilderContext)
        {
            if (!AllowedScenarioBuilderTypes.Contains((int)scenarioType))
            {
                throw new ArgumentException(Errors.ScenarioBuilderTypeInvalid);
            }

            if (scenarioType == ScenarioBuilderType.All ||
                scenarioBuilderContext == null)
            {
                return;
            }
        }

        private (string, Dictionary<string, string>) ValidateKnownScenarioBuilder(
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            if (this.IsCurrentScenarioContextDefaultContext)
            {
                throw new ArgumentException(Errors.ScenarioFakerWithNoAdditionalScenariosForKnownScenarioBuilderContext);
            }

            if (scenarioCombinationConfiguration == null ||
                scenarioCombinationConfiguration.Count == 0)
            {
                throw new ArgumentException(Errors.KnownScenarioBuilderContextNotSet);
            }

            if (scenarioCombinationConfiguration.Count == 1)
            {
                throw new ArgumentException(Errors.KnownScenarioBuilderWithOnlyOneCondition);
            }

            return this.ValidateAndCleanupKnownScenarioBuilder(
                new Dictionary<string, string>(
                    scenarioCombinationConfiguration,
                    StringComparer.OrdinalIgnoreCase));
        }

        private (string, Dictionary<string, string>) ValidateAndCleanupKnownScenarioBuilder(
            Dictionary<string, string> scenarioCombinationConfiguration)
        {
            foreach (var builderScenarioContextName in scenarioCombinationConfiguration.Keys)
            {
                if (string.IsNullOrEmpty(builderScenarioContextName) ||
                    !this.scenarioContextsActions.ContainsKey(
                        builderScenarioContextName.NormalizeName()))
                {
                    throw new ArgumentException(
                        string.Format(
                            Errors.KnownScenarioBuilderWithInvalidScenarioContext,
                            builderScenarioContextName));
                }
            }

            var cleanedDictionary = new Dictionary<string, string>();
            var scenarioKey = string.Empty;

            this.ForEachScenarioWithScenarioCompleteValidation((scenarioContextName, keyIndex, scenarioNames) =>
            {
                if (scenarioCombinationConfiguration.TryGetValue(scenarioContextName, out var builderScenarioName))
                {
                    var cleanedBuilderScenarioName = builderScenarioName.NormalizeName();

                    if (!scenarioNames.Contains(cleanedBuilderScenarioName))
                    {
                        throw new ArgumentException(
                            string.Format(
                                Errors.KnownScenarioBuilderWithInvalidScenario,
                                scenarioContextName,
                                builderScenarioName));
                    }

                    scenarioKey += Keys.GetScenarioContextKeyValue(
                            keyIndex,
                            cleanedBuilderScenarioName);

                    if (this.knownScenarioCombinationConfiguration.TryGetValue(
                        scenarioKey,
                        out var scenarioCombinationType))
                    {
                        throw new ArgumentException(
                            string.Format(
                                Errors.KnownScenarioBuilderHasAnExistingKnownCondition,
                                scenarioContextName,
                                builderScenarioName,
                                scenarioKey,
                                scenarioCombinationType));
                    }

                    cleanedDictionary.Add(
                        scenarioContextName,
                        builderScenarioName);
                }
            });

            return (scenarioKey, cleanedDictionary);
        }

        #region Validate Scenario

        /// <summary>
        /// Validate the Action that will be executed for the given Scenario
        /// </summary>
        /// <param name="action">
        /// Action that will be executed for the given Scenario
        /// </param>
        private void ValidateAction(Action<IScenarioRuleSet<T>> action)
        {
            if (action == null)
            {
                throw new ArgumentException(Errors.ScenarioActionIsnotSet);
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
        /// Validate the Scenario name and the Scenario Type
        /// </summary>
        /// <param name="useDefaultScenarioContext">
        /// Flag that indicates if the method was called by a Default Scenario Context method (true)
        /// or a Custom Scenario Context method (false)
        /// </param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <returns>The name of new Scenario, normalized</returns>
        private string ValidateAndCleanupScenarioName(
            bool useDefaultScenarioContext,
            ScenarioCombinationType scenarioType,
            string scenarioName)
        {
            if (!AllowedScenarioTypes.Contains((int)scenarioType))
            {
                throw new ArgumentException(Errors.ScenarioTypeInvalid);
            }

            if (useDefaultScenarioContext)
            {
                if (scenarioType == ScenarioCombinationType.AlwaysInvalid &&
                    scenarioName == Defaults.ScenarioValidName)
                {
                    throw new ArgumentException(Errors.ScenarioTypeInvalidForValidDefaultContextScenario);
                }

                if (scenarioType == ScenarioCombinationType.AlwaysValid &&
                    scenarioName == Defaults.ScenarioInvalidName)
                {
                    throw new ArgumentException(Errors.ScenarioTypeInvalidForInvalidDefaultContextScenario);
                }
            }

            if (string.IsNullOrWhiteSpace(scenarioName))
            {
                throw new ArgumentException(Errors.ScenarioNameIsnotSet);
            }

            var cleanedScenarioName = scenarioName.NormalizeName();

            if (this.currentScenarioContextDictionary.TryGetValue(
                cleanedScenarioName,
                out _))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioNameAlreadyExists,
                        scenarioName,
                        this.currentScenarioContextName));
            }

            return cleanedScenarioName;
        }

        #endregion Validate Scenario

        #region Validate Scenario Context

        /// <summary>
        /// Validate the previous Scenario Context, checking if it has the minimum required scenarios
        /// </summary>
        private void ValidatePreviousScenarioContext()
        {
            if (this.currentScenarioContextDictionary.Count < MinimumScenariosByScenarioContext)
            {
                if (!this.IsCurrentScenarioContextDefaultContext)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            Errors.ScenarioContextIncomplete,
                            this.currentScenarioContextName));
                }

                if (!this.currentScenarioContextDictionary.TryGetValue(
                    Defaults.ScenarioValidName, out var _))
                {
                    throw new InvalidOperationException(
                        Errors.DefaultScenarioContextWithoutValidScenario);
                }

                throw new InvalidOperationException(
                    Errors.DefaultScenarioContextWithoutInvalidScenario);
            }
        }

        /// <summary>
        /// Validate the new Scenario Context Name
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context</param>
        /// <returns>The name of new scenario context, normalized</returns>
        private string ValidateAndCleanupScenarioContextName(string scenarioContextName)
        {
            if (string.IsNullOrWhiteSpace(scenarioContextName))
            {
                throw new ArgumentException(
                    Errors.ScenarioContextNameIsnotSet);
            }

            var cleanedScenarioContextName = scenarioContextName.NormalizeName();

            if (string.Equals(
                cleanedScenarioContextName,
                Defaults.ScenarioContextName))
            {
                throw new ArgumentException(
                    Errors.ScenarioContextNameAsDefaultIsnotAllowed);
            }

            if (this.scenarioContextsActions.TryGetValue(
                cleanedScenarioContextName,
                out _))
            {
                throw new ArgumentException(
                    string.Format(
                        Errors.ScenarioContextNameAlreadyExists,
                        scenarioContextName));
            }

            return cleanedScenarioContextName;
        }

        #endregion Validate Scenario Context

        #endregion Validate Methods

        /// <summary>
        /// Add a new Scenario to the current Scenario Context
        /// </summary>
        /// <param name="useDefaultScenarioContext">Indicates if the method was called by a Default Scenario Context (true) or a Custom Scenario Context (false)</param>
        /// <param name="scenarioName">Indicates the name of the Scenario</param>
        /// <param name="scenarioType">
        /// Indicates if the Current Scenario will be <see cref="ScenarioCombinationType.Unknown"/>, <see cref="ScenarioCombinationType.AlwaysValid"/> or <see cref="ScenarioCombinationType.AlwaysInvalid"/>
        /// </param>
        /// <param name="action">The actions that will be executed to the current Scenario</param>
        /// <returns>Returns the current instance of the <see cref="IScenariosBuilder{T}" /> (fluent interface)</returns>
        private ScenariosFaker<T> Scenario(
            bool useDefaultScenarioContext,
            string scenarioName,
            ScenarioCombinationType scenarioType,
            Action<IScenarioRuleSet<T>> action)
        {
            this.ValidateDefaultScenarioContext(useDefaultScenarioContext);

            this.ValidateAction(action);

            var cleanedScenarioName = this.ValidateAndCleanupScenarioName(
                useDefaultScenarioContext,
                scenarioType,
                scenarioName);

            this.currentScenarioContextDictionary.Add(cleanedScenarioName, action);

            if (scenarioType != ScenarioCombinationType.Unknown)
            {
                this.AddKnownScenarioBuilder(
                    scenarioType,
                    Keys.GetScenarioContextKeyValue(this.currentScenarioContextIndex, cleanedScenarioName),
                    new Dictionary<string, string> { { this.currentScenarioContextName, cleanedScenarioName } });
            }

            return this;
        }

        private void AddKnownScenarioCombinationWithValidation(
            ScenarioCombinationType scenarioCombinationType,
            IDictionary<string, string> scenarioCombinationConfiguration)
        {
            (var scenarioKey, var cleanedScenarioBuilder) = this.ValidateKnownScenarioBuilder(
                scenarioCombinationConfiguration);

            this.AddKnownScenarioBuilder(
                scenarioCombinationType,
                scenarioKey,
                cleanedScenarioBuilder);
        }

        private void AddKnownScenarioBuilder(
            ScenarioCombinationType scenarioCombinationType,
            string knownScenariosBuilderKey,
            Dictionary<string, string> scenarioBuilderContext)
        {
            this.knownScenarioCombinationConfiguration.Add(
                knownScenariosBuilderKey,
                (scenarioCombinationType, scenarioBuilderContext));
        }

        /// <summary>
        /// Add a new Scenario Context, initializing all the information related to it
        /// </summary>
        /// <param name="scenarioContextName">The name of new scenario context, normalized and validated</param>
        private void AddNewScenarioContext(string scenarioContextName)
        {
            this.currentScenarioContextName = scenarioContextName;

            this.currentScenarioContextIndex++;

            this.currentScenarioContextDictionary = new Dictionary<string, Action<IScenarioRuleSet<T>>>();

            this.scenarioContextsActions.Add(this.currentScenarioContextName, this.currentScenarioContextDictionary);
        }

        private void ForEachScenarioWithScenarioCompleteValidation(
            Action<string, int, HashSet<string>> action)
        {
            var keyIndex = 0;

            foreach (var scenarioContextAction in this.scenarioContextsActions)
            {
                var scenarioContextName = scenarioContextAction.Key;
                var scenarioNames = new HashSet<string>(scenarioContextAction.Value.Keys);

                if (scenarioNames.Count <= 1)
                {
                    throw new InvalidOperationException(
                        string.Format(
                            Errors.ScenarioContextIncomplete,
                            scenarioContextName));
                }

                action(scenarioContextName, ++keyIndex, scenarioNames);
            }
        }

        #endregion Private Methods

        #endregion Methods
    }
}