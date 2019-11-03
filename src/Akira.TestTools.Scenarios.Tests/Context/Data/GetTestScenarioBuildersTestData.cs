using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context.Data
{
    internal static class GetTestScenarioBuildersTestData
    {
        private const string ScenarioContextTestName = nameof(ScenarioContextTestName);

        private const string ScenarioContextTestNameAlternative = nameof(ScenarioContextTestNameAlternative);

        private const string ScenarioTestName = nameof(ScenarioTestName);

        private const string Scenariotestname = nameof(Scenariotestname);

        private const string ScenarioTestNameAlternative = nameof(ScenarioTestNameAlternative);

        internal static IEnumerable<object[]> GetTestData()
        {
            var testData = GetValidData();

            foreach (var context in testData)
            {
                yield return new object[] { context };
            }
        }

        private static IEnumerable<TestBuilderContext> GetValidData()
        {
            yield return new TestBuilderContext
            {
                CaseNumber = 1,
                CaseDescription = "Empty Faker",
                GetFaker = () => new ScenariosFaker<SimpleModel>(),
                ExpectedCountAll = 2,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 2,
                CaseDescription = "Valid Default RuleSet Only",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 3,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 3,
                CaseDescription = "Valid Default RuleSet Only - Always valid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysValid),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 1,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 4,
                CaseDescription = "Valid Default RuleSet Only - explicit undefined",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.Unknown),
                ExpectedCountAll = 3,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 5,
                CaseDescription = "Invalid Default RuleSet Only",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 3,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 6,
                CaseDescription = "Invalid Default RuleSet Only - explicit undefined",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.Unknown),
                ExpectedCountAll = 3,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 7,
                CaseDescription = "Invalid Default RuleSet Only - Always invalid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 1
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 8,
                CaseDescription = "Defaults RuleSet - First Valid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 9,
                CaseDescription = "Defaults RuleSet - First Valid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysInvalid),
                ExpectedCountAll = 6,
                ExpectedCountValidOnly = 1,
                ExpectedCountInvalidOnly = 1
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 10,
                CaseDescription = "Defaults RuleSet - First Invalid",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 4,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 11,
                CaseDescription = "Custom Scenario RuleSet - One RuleSet Only",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 5,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 12,
                CaseDescription = "Custom Scenario RuleSet - Two RuleSets",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
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
                CaseNumber = 13,
                CaseDescription = "Custom Scenario RuleSet with Alternate Scenario - One RuleSet Only",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 7,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };

            yield return new TestBuilderContext
            {
                CaseNumber = 14,
                CaseDescription = "Custom Scenario RuleSet with Alternate Scenario - Two RuleSets",
                GetFaker = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextTestNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id)),
                ExpectedCountAll = 8,
                ExpectedCountValidOnly = 0,
                ExpectedCountInvalidOnly = 0
            };
        }
    }
}