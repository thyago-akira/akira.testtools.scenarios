using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context.Data
{
    internal static class GenerateMinimumTestingScenariosTestData
    {
        private const string ScenarioContextTestName = nameof(ScenarioContextTestName);

        private const string ScenarioContextTestNameAlternative = nameof(ScenarioContextTestNameAlternative);

        private const string ScenarioTestName = nameof(ScenarioTestName);

        private const string ScenarioTestNameAlternative = nameof(ScenarioTestNameAlternative);

        private const string ScenarioTestNameAlternative2 = nameof(ScenarioTestNameAlternative2);

        internal enum TestDataType
        {
            /// <summary>
            /// Valid Data Test Scenario
            /// </summary>
            ValidData
        }

        internal static IEnumerable<TestBuilderContext> GetTestDataByDataType(TestDataType testDataType)
        {
            switch (testDataType)
            {
                case TestDataType.ValidData:
                    return GetValidData();
            }

            return default;
        }

        private static IEnumerable<TestBuilderContext> GetValidData()
        {
            yield return new TestBuilderContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total)),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context - With Always Valid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total)),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 1,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 3,
                CaseDescription = "Default Scenario Context - With Always Invalid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 1
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 4,
                CaseDescription = "Default Scenario Context - Both defaults defined",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 1,
                ExpectedCountInvalidOnly = 1
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 5,
                CaseDescription = "Custom Scenario Context - Two Scenarios",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 6,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 6,
                CaseDescription = "Custom Scenario Context - Two Scenarios - With Invalids",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 6,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 2
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 7,
                CaseDescription = "Custom Scenario Context - Two Scenarios - With Valids",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 6,
                ExpectedCountValidOnly = 2,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 8,
                CaseDescription = "Custom Scenario Context - Two Scenarios - With Both",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedCountAll = 6,
                ExpectedCountValidOnly = 2,
                ExpectedCountInvalidOnly = 1
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 9,
                CaseDescription = "Custom Scenario Contexts - Two Scenarios",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total)),
                ExpectedCountAll = 9,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 10,
                CaseDescription = "Custom Scenario Contexts - Multiple Scenarios - With Invalid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedCountAll = 9,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 3
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 11,
                CaseDescription = "Custom Scenario Contexts - Multiple Scenarios - With Valid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid),
                ExpectedCountAll = 9,
                ExpectedCountValidOnly = 4,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 12,
                CaseDescription = "Custom Scenario Contexts - Multiple Scenarios - With Both",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid),
                ExpectedCountAll = 9,
                ExpectedCountValidOnly = 4,
                ExpectedCountInvalidOnly = 2
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 13,
                CaseDescription = "Custom Scenario Contexts - With Known Scenario Combination",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestNameAlternative, ScenarioTestName }
                        }),
                ExpectedCountAll = 10,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 14,
                CaseDescription = "Custom Scenario Contexts - With Known Scenario Combination - Valid Only",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextTestNameAlternative, ScenarioTestNameAlternative }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedCountAll = 10,
                ExpectedCountValidOnly = 1,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 15,
                CaseDescription = "Custom Scenario Contexts - With Known Scenario Combination - Valid Only",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioTestName },
                            { ScenarioContextTestNameAlternative, ScenarioTestNameAlternative }
                        },
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedCountAll = 10,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 1
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 16,
                CaseDescription = "Custom Scenario Contexts - With Known Scenario Combination - Both",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioTestName },
                            { ScenarioContextTestNameAlternative, ScenarioTestNameAlternative }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioTestName },
                            { ScenarioContextTestNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedCountAll = 11,
                ExpectedCountValidOnly = 1,
                ExpectedCountInvalidOnly = 1
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 17,
                CaseDescription = "Custom Scenario Contexts - With Known Scenario Combination - Mixed",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioTestName },
                            { ScenarioContextTestNameAlternative, ScenarioTestNameAlternative }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { ScenarioContextTestName, ScenarioTestName },
                            { ScenarioContextTestNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid),
                ExpectedCountAll = 11,
                ExpectedCountValidOnly = 2,
                ExpectedCountInvalidOnly = 2
            };
        }
    }
}