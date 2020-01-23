using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context.Data
{
    internal static class ScenarioTestData
    {
        private const string ScenarioContextName = nameof(ScenarioContextName);

        private const string ScenarioContextNameAlternative = nameof(ScenarioContextNameAlternative);

        private const string ScenarioTestName = nameof(ScenarioTestName);

        private const string Scenariotestname = nameof(Scenariotestname);

        private const string ScenarioTestNameAlternative = nameof(ScenarioTestNameAlternative);

        internal enum TestDataType
        {
            /// <summary>
            /// Valid Data Test Scenario
            /// </summary>
            ValidData,

            /// <summary>
            /// Invalid Data Test Scenario
            /// Try to set Default Scenario Context - Scenario after a Custom Scenario Context
            /// </summary>
            DefaultScenarioContextScenarioAfterCustomScenarioContext,

            /// <summary>
            /// Invalid Data Test Scenario
            /// Try to set Default Scenario Context - Scenario using a Custom Scenario Context - Scenario method
            /// </summary>
            DefaultScenarioContextScenarioUsingCustomScenarioContextScenario,

            /// <summary>
            /// Invalid Data Test Scenario
            /// Scenario Null Action
            /// </summary>
            ScenarioWithNullAction,

            /// <summary>
            /// Invalid Data Test Scenario
            /// Invalid Scenario Type for Scenario
            /// </summary>
            ScenarioWithInvalidScenarioType,

            /// <summary>
            /// Invalid Data Test Scenario
            /// Empty Scenario Name
            /// </summary>
            ScenarioWithEmptyScenarioName,

            /// <summary>
            /// Invalid Data Test Scenario
            /// Existing Scenario Name
            /// </summary>
            ScenarioWithExistingScenarioName
        }

        internal static IEnumerable<TestContext> GetTestDataByDataType(TestDataType testDataType)
        {
            switch (testDataType)
            {
                case TestDataType.ValidData:
                    return GetValidData();

                case TestDataType.DefaultScenarioContextScenarioAfterCustomScenarioContext:
                    return GetDefaultScenarioContextScenarioAfterCustomScenarioContext();

                case TestDataType.DefaultScenarioContextScenarioUsingCustomScenarioContextScenario:
                    return GetDefaultScenarioContextScenarioUsingCustomScenarioContextScenario();

                case TestDataType.ScenarioWithNullAction:
                    return GetScenarioWithNullAction();

                case TestDataType.ScenarioWithInvalidScenarioType:
                    return GetScenarioWithInvalidScenarioType();

                case TestDataType.ScenarioWithEmptyScenarioName:
                    return GetScenarioWithEmptyScenarioName();

                case TestDataType.ScenarioWithExistingScenarioName:
                    return GetScenarioWithExistingScenarioName();
            }

            return default;
        }

        private static IEnumerable<TestContext> GetValidData()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context - Valid Scenario Only",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context - Invalid Scenario Only",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Default Scenario Context - First Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Default Scenario Context - First Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Custom Scenario Context - One Scenario Only",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Custom Scenario Context - Two Scenarios",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 7,
                CaseDescription = "Two Custom Scenario Contexts - One Scenario Only",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 8,
                CaseDescription = "Two Custom Scenario Contexts - Two Scenarios",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
            };

            yield return new TestContext
            {
                CaseNumber = 9,
                CaseDescription = "Default Scenario Context - RuleFor - Func<TProperty>",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.RuleFor(f => f.Id, () => 1))
            };

            yield return new TestContext
            {
                CaseNumber = 10,
                CaseDescription = "Default Scenario Context - RuleFor - TProperty",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.RuleFor(f => f.Id, 2))
            };

            yield return new TestContext
            {
                CaseNumber = 11,
                CaseDescription = "Default Scenario Context - RuleFor - Func<Bogus.Faker, TProperty>",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.RuleFor(f => f.Id, (f) => f.Random.Int(1)))
            };

            yield return new TestContext
            {
                CaseNumber = 12,
                CaseDescription = "Default Scenario Context - RuleFor - Func<Bogus.Faker, T, TProperty>",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.RuleFor(f => f.Id, (f, m) => m.Id ?? f.Random.Int(1)))
            };
        }

        private static IEnumerable<TestContext> GetDefaultScenarioContextScenarioAfterCustomScenarioContext()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Faker With Empty Custom Scenario Context - Set Default Context Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Faker With Empty Custom Scenario Context - Set Default Context Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Faker With Incompleted Custom Scenario Context - Set Default Context Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Faker With Incompleted Custom Scenario Context - Set Default Context Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Faker With Complete Custom Scenario Context - Set Default Context Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Faker With Complete Custom Scenario Context - Set Default Context Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 7,
                CaseDescription = "Faker With Incompleted Custom Scenario Alternative Context - Set Default Context Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 8,
                CaseDescription = "Faker With Incompleted Custom Scenario Alternative Context - Set Default Context Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 9,
                CaseDescription = "Faker With Complete Custom Scenario Alternative Context - Set Default Context Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };

            yield return new TestContext
            {
                CaseNumber = 10,
                CaseDescription = "Faker With Complete Custom Scenario Alternative Context - Set Default Context Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeCalledFirst
            };
        }

        private static IEnumerable<TestContext> GetDefaultScenarioContextScenarioUsingCustomScenarioContextScenario()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Empty Default Scenario Context",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeSetInOtherMethods
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context with Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeSetInOtherMethods
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Default Scenario Context with Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeSetInOtherMethods
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Default Scenario Context completed",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenariosForDefaultScenarioContextMustBeSetInOtherMethods
            };
        }

        private static IEnumerable<TestContext> GetScenarioWithNullAction()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context - Valid Scenario Null",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(null),
                ExpectedExceptionMessage = Errors.ScenarioRuleSetActionIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context - Invalid Scenario Null",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(null),
                ExpectedExceptionMessage = Errors.ScenarioRuleSetActionIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Default Scenario Context - Valid Scenario Null after setting Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(null),
                ExpectedExceptionMessage = Errors.ScenarioRuleSetActionIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Default Scenario Context - Invalid Scenario Null after setting Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(null),
                ExpectedExceptionMessage = Errors.ScenarioRuleSetActionIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Custom Scenario Context - Scenario Action Null",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        null),
                ExpectedExceptionMessage = Errors.ScenarioRuleSetActionIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Custom Scenario Context - Scenario Action Null with previous Scenario Defined",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        null),
                ExpectedExceptionMessage = Errors.ScenarioRuleSetActionIsnotSet
            };
        }

        private static IEnumerable<TestContext> GetScenarioWithInvalidScenarioType()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context - Valid Scenario Wrong Type",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        (ScenarioType)999),
                ExpectedExceptionMessage = Errors.ScenarioTypeInvalid
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context - Invalid Scenario Wrong Type",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        (ScenarioType)999),
                ExpectedExceptionMessage = Errors.ScenarioTypeInvalid
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Custom Scenario Context - Custom Scenario Wrong Type",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        (ScenarioType)999),
                ExpectedExceptionMessage = Errors.ScenarioTypeInvalid
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Custom Scenario Context - Custom Scenario Wrong Type - Previous Scenario Valid",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        (ScenarioType)999),
                ExpectedExceptionMessage = Errors.ScenarioTypeInvalid
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Default Scenario Context - Set as Always Invalid a Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioType.AlwaysInvalid),
                ExpectedExceptionMessage = Errors.ScenarioTypeInvalidForValidDefaultContextScenario
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Default Scenario Context - Set as Always Valid a Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioType.AlwaysValid),
                ExpectedExceptionMessage = Errors.ScenarioTypeInvalidForInvalidDefaultContextScenario
            };
        }

        private static IEnumerable<TestContext> GetScenarioWithEmptyScenarioName()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Custom Scenario Context - Scenario Name Empty",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        string.Empty,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Custom Scenario Context - Scenario Name Null",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        null,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Custom Scenario Context - Scenario Name Empty - Second Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        string.Empty,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Custom Scenario Context - Scenario Name Null - Second Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        null,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Two Custom Scenario Contexts - Scenario Name Empty",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        string.Empty,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Two Custom Scenario Contexts - Scenario Name Null",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        null,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 7,
                CaseDescription = "Two Custom Scenario Contexts - Scenario Name Empty - Second Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        string.Empty,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };

            yield return new TestContext
            {
                CaseNumber = 8,
                CaseDescription = "Two Custom Scenario Contexts - Scenario Name Null - Second Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        null,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = Errors.ScenarioNameIsnotSet
            };
        }

        private static IEnumerable<TestContext> GetScenarioWithExistingScenarioName()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context - Existing Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Defaults.ScenarioValidName,
                    Defaults.ContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context - Existing Valid Scenario - After Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Defaults.ScenarioValidName,
                    Defaults.ContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Default Scenario Context - Existing Valid Scenario - Invalid Scenario first",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Defaults.ScenarioValidName,
                    Defaults.ContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Default Scenario Context - Existing Invalid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Defaults.ScenarioInvalidName,
                    Defaults.ContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Default Scenario Context - Existing Invalid Scenario - After setting Valid Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Defaults.ScenarioInvalidName,
                    Defaults.ContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Default Scenario Context - Existing Invalid Scenario - Valid Scenario first",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Defaults.ScenarioInvalidName,
                    Defaults.ContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 7,
                CaseDescription = "Custom Scenario Context - Custom Existing Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    ScenarioTestName,
                    ScenarioContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 8,
                CaseDescription = "Custom Scenario Context - Custom Existing Scenario - after setting Alternative Scenario",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    ScenarioTestName,
                    ScenarioContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 9,
                CaseDescription = "Custom Scenario Context - Custom Existing Scenario - Alternative Scenario first",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    ScenarioTestName,
                    ScenarioContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 10,
                CaseDescription = "Custom Scenario Context - Custom Existing Scenario - ignoring case",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        Scenariotestname,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Scenariotestname,
                    ScenarioContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 11,
                CaseDescription = "Custom Scenario Context - Custom Existing Scenario - After setting Alternative Scenario - ignoring case",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        Scenariotestname,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Scenariotestname,
                    ScenarioContextName)
            };

            yield return new TestContext
            {
                CaseNumber = 12,
                CaseDescription = "Custom Scenario Context - Custom Existing Scenario - Alternative Scenario first - ignoring case",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        Scenariotestname,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioNameAlreadyExists,
                    Scenariotestname,
                    ScenarioContextName)
            };
        }
    }
}