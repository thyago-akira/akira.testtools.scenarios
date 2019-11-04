using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context.Data
{
    public class AddKnownScenarioCombinationTestData
    {
        private const string ScenarioContextTestName = nameof(ScenarioContextTestName);

        private const string Scenariocontexttestname = nameof(Scenariocontexttestname);

        private const string ScenarioContextTestName2 = nameof(ScenarioContextTestName2);

        private const string ScenarioContextTestName3 = nameof(ScenarioContextTestName3);

        private const string ScenarioContextTestName4 = nameof(ScenarioContextTestName4);

        private const string ScenarioContextTestName5 = nameof(ScenarioContextTestName5);

        private const string ScenarioContextTestScenarioNameValid = nameof(ScenarioContextTestScenarioNameValid);

        private const string ScenarioContextTestScenarioNameInvalid = nameof(ScenarioContextTestScenarioNameInvalid);

        private const string ScenarioContextTestScenarioName3 = nameof(ScenarioContextTestScenarioName3);

        private const string ScenarioContextTestUnknownScenarioName = nameof(ScenarioContextTestUnknownScenarioName);

        private const string Scenariocontexttestunknownscenarioname = nameof(Scenariocontexttestunknownscenarioname);

        internal enum TestDataType
        {
            /// <summary>
            /// All valid scenarios to be tested
            /// </summary>
            ValidData,

            /// <summary>
            /// All scenarios with InvalidData, except for Collision Exception
            /// </summary>
            InvalidData,

            /// <summary>
            /// All scenarios for Collision invalid data
            /// </summary>
            ScenariosCollisionInvalidData
        }

        internal static IEnumerable<object[]> GetTestData(TestDataType testDataType)
        {
            var testData = GetTestDataByDataType(testDataType);

            foreach (var context in testData)
            {
                yield return new object[] { context };
            }
        }

        private static IEnumerable<TestContext> GetValidData()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Add Known Scenario Combination with 2 Configurations - Unknown",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Add Known Scenario Combination with 3 Configurations - Unknown",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Add Known Scenario Combination with 4 Configurations - Unknown",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Add Known Scenario Combination with 5 Configurations - Unknown",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Add Known Scenario Combination with 6 Configurations - Unknown",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Add Known Scenario Combination - Multiple Unknown",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 7,
                CaseDescription = "Add Known Scenario Combination - Multiple Unknown - Reverse order",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 8,
                CaseDescription = "Add Known Scenario Combination - Multiple Unknown - Without order",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 9,
                CaseDescription = "Add Known Scenario Combination - Multiple Unknown - With Always Valid Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 10,
                CaseDescription = "Add Known Scenario Combination - Multiple Unknown - With Always Invalid Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker(false, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid }
                        })
            };

            yield return new TestContext
            {
                CaseNumber = 11,
                CaseDescription = "Add Known Scenario Combination - Multiple Unknown - With Always Valid and Invalid Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName5, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName }
                        })
            };
        }

        private static IEnumerable<TestContext> GetInvalidData()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Add Known Scenario Combination for a Faker with only Default Scenario Context",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .AddKnownScenarioCombination(null),
                ExpectedExceptionMessage = Errors.ScenarioFakerWithNoAdditionalScenariosForKnownScenarioCombinationConfig
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Add Null Known Scenario Combination",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioNameValid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .AddKnownScenarioCombination(null),
                ExpectedExceptionMessage = Errors.KnownScenarioCombinationConfigNotSet
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Add Empty Known Scenario Combination",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioNameValid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .AddKnownScenarioCombination(new Dictionary<string, string>()),
                ExpectedExceptionMessage = Errors.KnownScenarioCombinationConfigNotSet
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Add Known Scenario Combination with Only One Condition",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioNameValid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioContextTestUnknownScenarioName }
                        }),
                ExpectedExceptionMessage = Errors.KnownScenarioCombinationConfigWithOnlyOneCondition
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Add Known Scenario Combination with Invalid Scenario Context Name",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioNameValid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioContextTestUnknownScenarioName },
                            { ScenarioContextTestName3, ScenarioContextTestUnknownScenarioName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigWithInvalidScenarioContext,
                    ScenarioContextTestName)
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Add Known Scenario Combination with Invalid Scenario Context Name - ignore case",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioNameValid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Scenariocontexttestname, ScenarioContextTestUnknownScenarioName },
                            { ScenarioContextTestName3, ScenarioContextTestUnknownScenarioName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigWithInvalidScenarioContext,
                    Scenariocontexttestname)
            };

            yield return new TestContext
            {
                CaseNumber = 7,
                CaseDescription = "Add Known Scenario Combination with Invalid Scenario Context Scenario",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioNameValid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .Scenario(
                        ScenarioContextTestScenarioNameInvalid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, ScenarioContextTestUnknownScenarioName },
                            { ScenarioContextTestName2, ScenarioContextTestUnknownScenarioName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigWithInvalidScenario,
                    Defaults.ScenarioContextName,
                    ScenarioContextTestUnknownScenarioName)
            };

            yield return new TestContext
            {
                CaseNumber = 8,
                CaseDescription = "Add Known Scenario Combination with Invalid Scenario Context Scenario - ignore case",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioNameValid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .Scenario(
                        ScenarioContextTestScenarioNameInvalid,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName2, Scenariocontexttestunknownscenarioname }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigWithInvalidScenario,
                    ScenarioContextTestName2.NormalizeName(),
                    Scenariocontexttestunknownscenarioname)
            };
        }

        private static IEnumerable<TestContext> GetScenariosColisionInvalidData()
        {
            yield return new TestContext
            {
                CaseNumber = 1,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Unknown Scenarios - 2 Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 2,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Unknown Scenarios - 2 Scenarios - case insensitive",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 3,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Unknown Scenarios - 3 Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 4,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Unknown Scenarios - 4 Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioNameValid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(4, ScenarioContextTestScenarioName3.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 5,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Unknown Scenarios - 5 Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioNameValid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(4, ScenarioContextTestScenarioName3.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(5, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 6,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Unknown Scenarios - 6 Scenarios",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioNameValid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(4, ScenarioContextTestScenarioName3.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(5, ScenarioContextTestScenarioNameValid.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(6, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 7,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Same with Last as Always Valid Scenario",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 8,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Same with Last as Always Invalid Scenario",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid }
                        })
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.Unknown)
            };

            yield return new TestContext
            {
                CaseNumber = 9,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Same Always Valid Scenario",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 10,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Same Always Invalid Scenario",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 11,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid - Last of 2",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 12,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid - Last of 3",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 13,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid - Last of 4",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioNameValid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(4, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 14,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid - Last of 5",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(5, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 15,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid - Last of 6",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(6, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 16,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Invalid - Last of 2",
                TestedAction = () => GetCompletedScenarioBuilderFaker(false, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        }),
                ExpectedExceptionMessage = string.Format(
                  Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                  Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                  ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 17,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Invalid - Last of 3",
                TestedAction = () => GetCompletedScenarioBuilderFaker(false, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameInvalid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 18,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Invalid - Last of 4",
                TestedAction = () => GetCompletedScenarioBuilderFaker(false, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioNameInvalid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(4, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 19,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Invalid - Last of 5",
                TestedAction = () => GetCompletedScenarioBuilderFaker(false, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameInvalid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(5, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 20,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Invalid - Last of 6",
                TestedAction = () => GetCompletedScenarioBuilderFaker(false, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        },
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(6, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 21,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid and Invalid - Last of 2",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioNameInvalid }
                        }),
                ExpectedExceptionMessage = string.Format(
                  Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                  Keys.GetScenarioContextKeyValue(2, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                  ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 22,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid and Invalid - Last of 3",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 23,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid and Invalid - Last of 4",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioNameInvalid }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(4, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 24,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid and Invalid - Last of 5",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioNameValid }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(5, ScenarioContextTestScenarioNameValid.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };

            yield return new TestContext
            {
                CaseNumber = 25,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Always Valid and Invalid - Last of 6",
                TestedAction = () => GetCompletedScenarioBuilderFaker(true, true)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestName, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName3, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioNameInvalid }
                        },
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(6, ScenarioContextTestScenarioNameInvalid.NormalizeName()),
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new TestContext
            {
                CaseNumber = 26,
                CaseDescription = "Add Known Scenario Combination - Scenario Collision - Scenarios Unknown - Multiple combination",
                TestedAction = () => GetCompletedScenarioBuilderFaker()
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName2, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioName3 }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .AddKnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName2, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName4, ScenarioContextTestScenarioName3 },
                            { ScenarioContextTestName5, ScenarioContextTestScenarioName3 }
                        },
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigHasAColisionWithAnExistingKnownCondition,
                    Keys.GetScenarioContextKeyValue(3, ScenarioContextTestScenarioName3.NormalizeName()) +
                    Keys.GetScenarioContextKeyValue(6, ScenarioContextTestScenarioName3.NormalizeName()),
                    ScenarioCombinationType.AlwaysValid)
            };
        }

        private static IScenariosBuilder<SimpleModel> GetCompletedScenarioBuilderFaker(
            bool useAlwaysValidScenarios = false,
            bool useAlwaysInvalidScenarios = false)
        {
            return new ScenariosFaker<SimpleModel>()
                .DefaultContextValidScenario(
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                .DefaultContextInvalidScenario(
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                .ScenarioContext(ScenarioContextTestName)
                .Scenario(
                    ScenarioContextTestScenarioNameValid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Name),
                    useAlwaysValidScenarios ? ScenarioCombinationType.AlwaysValid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioNameInvalid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Name),
                    useAlwaysInvalidScenarios ? ScenarioCombinationType.AlwaysInvalid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioName3,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                .ScenarioContext(ScenarioContextTestName2)
                .Scenario(
                    ScenarioContextTestScenarioNameValid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total),
                    useAlwaysValidScenarios ? ScenarioCombinationType.AlwaysValid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioNameInvalid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total),
                    useAlwaysInvalidScenarios ? ScenarioCombinationType.AlwaysInvalid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioName3,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                .ScenarioContext(ScenarioContextTestName3)
                .Scenario(
                    ScenarioContextTestScenarioNameValid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id),
                    useAlwaysValidScenarios ? ScenarioCombinationType.AlwaysValid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioNameInvalid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id),
                    useAlwaysInvalidScenarios ? ScenarioCombinationType.AlwaysInvalid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioName3,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                .ScenarioContext(ScenarioContextTestName4)
                .Scenario(
                    ScenarioContextTestScenarioNameValid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total),
                    useAlwaysValidScenarios ? ScenarioCombinationType.AlwaysValid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioNameInvalid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total),
                    useAlwaysInvalidScenarios ? ScenarioCombinationType.AlwaysInvalid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioName3,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id))
                .ScenarioContext(ScenarioContextTestName5)
                .Scenario(
                    ScenarioContextTestScenarioNameValid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id),
                    useAlwaysValidScenarios ? ScenarioCombinationType.AlwaysValid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioNameInvalid,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id),
                    useAlwaysInvalidScenarios ? ScenarioCombinationType.AlwaysInvalid : ScenarioCombinationType.Unknown)
                .Scenario(
                    ScenarioContextTestScenarioName3,
                    scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Id));
        }

        private static IEnumerable<TestContext> GetTestDataByDataType(TestDataType testDataType)
        {
            switch (testDataType)
            {
                case TestDataType.ValidData:
                    return GetValidData();

                case TestDataType.InvalidData:
                    return GetInvalidData();

                case TestDataType.ScenariosCollisionInvalidData:
                    return GetScenariosColisionInvalidData();
            }

            return default;
        }
    }
}