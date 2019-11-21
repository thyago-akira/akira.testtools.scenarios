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

        public const string ScenarioBuilderDoesnotContainAlwaysValidKnownScenario =
            "Scenario Builder doesn't contain an Always Valid Known Scenario";

        public const string ScenarioBuilderDoesnotContainAlwaysInvalidKnownScenario =
            "Scenario Builder doesn't contain an Always Invalid Known Scenario";

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

        public const string ScenarioFakerWithNoAdditionalScenariosForKnownScenarioCombinationConfig =
            "Scenario Faker has no additional scenarios. Please use the parameter 'scenarioType' in Scenario operations.";

        public const string KnownScenarioCombinationConfigNotSet =
            "Known Scenario Combination Configuration is not set";

        public const string KnownScenarioCombinationConfigWithOnlyOneCondition =
            "Known Scenario Combination Configuration with only one builder condition. Please use the parameter 'scenarioType' in Scenario operations.";

        public const string ScenarioCombinationConfigWithInvalidScenarioContext =
            "Scenario Combination Configuration with invalid Scenario Context ({0})";

        public const string ScenarioCombinationConfigWithInvalidScenario =
            "Scenario Combination Configuration for Scenario Context '{0}' has an invalid Scenario ({1})";

        public const string KnownScenarioCombinationConfigContainsParentConfigurationCollision =
            "Known Scenario Combination Configuration contains a Parent Configuration Collision " +
            "with Key '{0}' and ScenarioCombinationType '{1}'";

        public const string KnownScenarioCombinationConfigContainsChildConfigurationCollision =
            "Known Scenario Combination Configuration contains a Child Configuration Collision " +
            "with Key '{0}' and ScenarioCombinationType '{1}'";

        public const string ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig =
            "Scenario Combination Configuration of type '{0}' has conflict with an existing " +
            "Known Scenario Combination Configuration with Key '{1}' and ScenarioCombinationType '{2}'";

        private const string DefaultContextValidScenario = nameof(ScenariosFaker<object>.DefaultContextValidScenario);

        private const string DefaultContextInvalidScenario = nameof(ScenariosFaker<object>.DefaultContextInvalidScenario);
    }
}