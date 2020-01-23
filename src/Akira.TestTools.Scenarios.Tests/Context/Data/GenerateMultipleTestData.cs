using System.Collections.Generic;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context.Data
{
    internal class GenerateMultipleTestData
    {
        internal enum TestDataType
        {
            /// <summary>
            /// Valid Data Test Scenario
            /// </summary>
            ValidData,

            /// <summary>
            /// Invalid Argument Exception Test Scenario
            /// </summary>
            InvalidArgumentExceptionData
        }

        internal static IEnumerable<GenerateMultipleTestContext> GetTestDataByDataType(TestDataType testDataType)
        {
            switch (testDataType)
            {
                case TestDataType.ValidData:
                    return GetValidData();

                case TestDataType.InvalidArgumentExceptionData:
                    return GetInvalidArgumentExceptionData();
            }

            return default;
        }

        private static IEnumerable<GenerateMultipleTestContext> GetValidData()
        {
            yield return new GenerateMultipleTestContext
            {
                CaseNumber = 1,
                CaseDescription = "Generate 1 Valid Model",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
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
                    .Generate(1),
                NumberOfRows = 1
            };

            yield return new GenerateMultipleTestContext
            {
                CaseNumber = 2,
                CaseDescription = "Generate 5 Valid Models",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
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
                    .Generate(5),
                NumberOfRows = 5
            };

            yield return new GenerateMultipleTestContext
            {
                CaseNumber = 3,
                CaseDescription = "Generate 10 Valid Models",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
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
                    .Generate(10),
                NumberOfRows = 10
            };
        }

        private static IEnumerable<GenerateMultipleTestContext> GetInvalidArgumentExceptionData()
        {
            yield return new GenerateMultipleTestContext
            {
                CaseNumber = 1,
                CaseDescription = "Invalid Number of Rows - Zero",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
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
                    .Generate(0),
                ExpectedExceptionMessage = Errors.ScenariosBuilder.InvalidNumberOfRows
            };

            yield return new GenerateMultipleTestContext
            {
                CaseNumber = 2,
                CaseDescription = "Invalid Number of Rows - Negative",
                TestedAction = () => new ScenariosBuilder<SimpleModel>()
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
                    .Generate(-1),
                ExpectedExceptionMessage = Errors.ScenariosBuilder.InvalidNumberOfRows
            };
        }
    }
}