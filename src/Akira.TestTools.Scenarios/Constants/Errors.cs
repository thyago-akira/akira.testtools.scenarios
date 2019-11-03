namespace Akira.TestTools.Scenarios.Constants
{
    public static class Errors
    {
        public const string DefaultScenarioContextWithoutValidScenario =
            "Default Scenario Context doesn't have a " + DefaultContextValidScenario;

        public const string DefaultScenarioContextWithoutInvalidScenario =
            "Default Scenario Context doesn't have a " + DefaultContextInvalidScenario;

        public const string ScenarioContextNameIsnotSet =
            "Scenario Context name is not set";

        public const string ScenarioContextNameAsDefaultIsnotAllowed =
            "Scenario Context name as 'default' isn't allowed";

        public const string ScenarioContextNameAlreadyExists =
            "Scenario Context '{0}' already exists";

        public const string ScenarioContextIncomplete =
            "Scenario Context '{0}' incomplete. You must have at least 2 Scenarios for each Scenario Context.";

        public const string ScenarioBuilderTypeInvalid =
            "Scenario Builder Type is not valid";

        public const string ScenariosForDefaultScenarioContextMustBeCalledFirst =
            "Scenarios for Default Scenario Context must be called first";

        public const string ScenariosForDefaultScenarioContextMustBeSetInOtherMethods =
            "Scenarios for Default Scenario Context must be set calling the methods " +
                DefaultContextValidScenario + " and " + DefaultContextInvalidScenario;

        public const string ScenarioNameIsnotSet =
            "Scenario name is not set";

        public const string ScenarioNameAlreadyExists =
            "Scenario '{0}' is already set to the Scenario Context '{1}'";

        public const string ScenarioActionIsnotSet =
            "Action is not set";

        public const string ScenarioTypeInvalid =
            "Scenario Type is not Valid";

        public const string ScenarioTypeInvalidForValidDefaultContextScenario =
            DefaultContextValidScenario + " couldn't be always invalid. Please, use " + DefaultContextInvalidScenario;

        public const string ScenarioTypeInvalidForInvalidDefaultContextScenario =
            DefaultContextInvalidScenario + " couldn't be always valid. Please, use " + DefaultContextValidScenario;

        public const string ScenarioFakerWithNoAdditionalScenariosForKnownScenarioBuilderContext =
            "Scenario Faker has no additional scenarios. Please use the parameter 'scenarioType' in Scenario operations.";

        public const string KnownScenarioBuilderContextNotSet =
            "KnownScenarioBuilderContext is not set";

        public const string KnownScenarioBuilderWithOnlyOneCondition =
            "KnownScenarioBuilderContext with only one builder condition. Please use the parameter 'scenarioType' in Scenario operations.";

        public const string KnownScenarioBuilderWithInvalidScenarioContext =
            "KnownScenarioBuilderContext with invalid Scenario Context ({0})";

        public const string KnownScenarioBuilderWithInvalidScenario =
            "KnownScenarioBuilderContext for Scenario Context '{0}' has an invalid Scenario ({1})";

        public const string KnownScenarioBuilderHasAnExistingKnownCondition =
            "KnownScenarioBuilderContext (Scenario Context '{0}' and Scenario '{1}') has an existing " +
            "known condition (Key '{2}' and ScenarioCombinationType '{3}')";

        private const string DefaultContextValidScenario = nameof(ScenariosFaker<object>.DefaultContextValidScenario);

        private const string DefaultContextInvalidScenario = nameof(ScenariosFaker<object>.DefaultContextInvalidScenario);
    }
}