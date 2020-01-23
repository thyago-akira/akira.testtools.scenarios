using System;
using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context.Data
{
    internal static class ScenarioContextTestData
    {
        private const string ScenarioContextTestName = nameof(ScenarioContextTestName);

        private const string Scenariocontexttestname = nameof(Scenariocontexttestname);

        private const string ScenarioContextTestName2 = nameof(ScenarioContextTestName2);

        private const string ScenarioContextTestName3 = nameof(ScenarioContextTestName3);

        private const string ScenarioContextTestScenarioName = nameof(ScenarioContextTestScenarioName);

        private const string ScenarioContextTestScenarioName2 = nameof(ScenarioContextTestScenarioName2);

        private const string ScenarioContextTestScenarioName3 = nameof(ScenarioContextTestScenarioName3);

        private const string ScenarioContextTestScenarioName4 = nameof(ScenarioContextTestScenarioName4);

        internal static IEnumerable<TestContext> GetValidData()
        {
            foreach (var testContext in GetTestData(
                 GetCompletedFakers(),
                 (faker) => GetValidCases(faker)))
            {
                yield return testContext;
            }
        }

        internal static IEnumerable<TestContext> GetInvalidDataWithCompletedFakers()
        {
            foreach (var testContext in GetTestData(
                 GetCompletedFakers(),
                 (faker) => GetInvalidCases(faker)))
            {
                yield return testContext;
            }
        }

        internal static IEnumerable<TestContext> GetInvalidDataWithIncompletedFakers()
        {
            foreach (var testContext in GetTestData(
                 GetIncompletedFakers(),
                 (faker) => GetInvalidCases(faker)))
            {
                yield return testContext;
            }
        }

        private static IEnumerable<TestFakerContext> GetIncompletedFakers()
        {
            yield return new TestFakerContext
            {
                FakerDescription = "Empty Faker",
                GetFaker = () => new ScenariosBuilder<SimpleModel>(),
                OverrideMessage = Errors.DefaultScenarioContextWithoutValidScenario
            };

            yield return new TestFakerContext
            {
                FakerDescription = "Default Context Valid Only",
                GetFaker = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id)),
                OverrideMessage = Errors.DefaultScenarioContextWithoutInvalidScenario
            };

            yield return new TestFakerContext
            {
                FakerDescription = "Default Context Invalid Only",
                GetFaker = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id)),
                OverrideMessage = Errors.DefaultScenarioContextWithoutValidScenario
            };

            yield return new TestFakerContext
            {
                FakerDescription = "Empty Custom Scenario Context",
                GetFaker = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2),
                OverrideMessage = string.Format(
                    Errors.ScenarioContextIncomplete,
                    ScenarioContextTestName2)
            };

            yield return new TestFakerContext
            {
                FakerDescription = "Incomplete Custom Scenario Context",
                GetFaker = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total)),
                OverrideMessage = string.Format(
                    Errors.ScenarioContextIncomplete,
                    ScenarioContextTestName2)
            };
        }

        private static IEnumerable<TestFakerContext> GetCompletedFakers()
        {
            yield return new TestFakerContext
            {
                FakerDescription = "Completed Default Scenario Context",
                GetFaker = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
            };

            yield return new TestFakerContext
            {
                FakerDescription = "Completed Custom Scenario Context",
                GetFaker = () => new ScenariosBuilder<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet =>
                        scenarioRuleSet.Ignore(m => m.Id))
                    .ScenarioContext(ScenarioContextTestName2)
                    .Scenario(
                        ScenarioContextTestScenarioName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
                    .Scenario(
                        ScenarioContextTestScenarioName2,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Total))
            };
        }

        private static IEnumerable<TestContext> GetInvalidCases(
            Func<IScenariosBuilder<SimpleModel>> getScenarioFaker)
        {
            yield return new TestContext
            {
                CaseDescription = "Null Scenario Context Name",
                TestedAction = () => getScenarioFaker().ScenarioContext(null),
                ExpectedExceptionMessage = Errors.Context.NameIsnotSet
            };

            yield return new TestContext
            {
                CaseDescription = "Empty Scenario Context Name",
                TestedAction = () => getScenarioFaker().ScenarioContext(string.Empty),
                ExpectedExceptionMessage = Errors.Context.NameIsnotSet
            };

            yield return new TestContext
            {
                CaseDescription = "Scenario Context Name as default",
                TestedAction = () => getScenarioFaker().ScenarioContext(Defaults.ContextName),
                ExpectedExceptionMessage = Errors.Context.NameAsDefaultIsnotAllowed.Format(
                    Defaults.ContextName)
            };

            yield return new TestContext
            {
                CaseDescription = "Add Scenario Context Twice",
                TestedAction = () => getScenarioFaker()
                    .ScenarioContext(ScenarioContextTestName)
                    .Scenario(
                        ScenarioContextTestScenarioName3,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Name))
                    .Scenario(
                        ScenarioContextTestScenarioName4,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Name))
                    .ScenarioContext(ScenarioContextTestName),
                ExpectedExceptionMessage = Errors.Context.NameAlreadyExists.Format(
                    ScenarioContextTestName)
            };

            yield return new TestContext
            {
                CaseDescription = "Add Scenario Context Twice - Ignore Case",
                TestedAction = () => getScenarioFaker()
                    .ScenarioContext(Scenariocontexttestname)
                    .Scenario(
                        ScenarioContextTestScenarioName3,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Name))
                    .Scenario(
                        ScenarioContextTestScenarioName4,
                        scenarioRuleSet => scenarioRuleSet.Ignore(m => m.Name))
                    .ScenarioContext(ScenarioContextTestName),
                ExpectedExceptionMessage = Errors.Context.NameAlreadyExists.Format(
                    ScenarioContextTestName)
            };
        }

        private static IEnumerable<TestContext> GetValidCases(
            Func<IScenariosBuilder<SimpleModel>> getScenarioFaker)
        {
            yield return new TestContext
            {
                CaseDescription = ScenarioContextTestName,
                TestedAction = () => getScenarioFaker().ScenarioContext(ScenarioContextTestName),
            };

            yield return new TestContext
            {
                CaseDescription = Scenariocontexttestname,
                TestedAction = () => getScenarioFaker().ScenarioContext(Scenariocontexttestname),
            };

            yield return new TestContext
            {
                CaseDescription = ScenarioContextTestName3,
                TestedAction = () => getScenarioFaker().ScenarioContext(ScenarioContextTestName3),
            };
        }

        private static IEnumerable<TestContext> GetTestData(
            IEnumerable<TestFakerContext> fakerContexts,
            Func<Func<IScenariosBuilder<SimpleModel>>, IEnumerable<TestContext>> getTestContexts)
        {
            var caseNumber = 0;

            foreach (var fakerContext in fakerContexts)
            {
                foreach (var testContext in getTestContexts(fakerContext.GetFaker))
                {
                    testContext.CaseNumber = ++caseNumber;
                    testContext.CaseDescription = $"{fakerContext.FakerDescription} - {testContext.CaseDescription}";
                    testContext.ExpectedExceptionMessage = fakerContext.OverrideMessage ?? testContext.ExpectedExceptionMessage;

                    yield return testContext;
                }
            }
        }
    }
}