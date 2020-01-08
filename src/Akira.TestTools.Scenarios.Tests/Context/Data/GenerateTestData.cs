using System.Collections.Generic;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Extensions;
using Akira.TestTools.Scenarios.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests.Context.Data
{
    internal static class GenerateTestData
    {
        private const string ScenarioContextName = nameof(ScenarioContextName);

        private const string ScenarioContextNameAlternative = nameof(ScenarioContextNameAlternative);

        private const string ScenarioInvalidContextName = nameof(ScenarioInvalidContextName);

        private const string ScenarioTestName = nameof(ScenarioTestName);

        private const string ScenarioTestNameAlternative = nameof(ScenarioTestNameAlternative);

        private const string ScenarioTestInvalidName = nameof(ScenarioTestInvalidName);

        internal enum TestDataType
        {
            /// <summary>
            /// Valid Data Test Scenario
            /// </summary>
            ValidData,

            /// <summary>
            /// Invalid Argument Exception Test Scenario
            /// </summary>
            InvalidArgumentExceptionData,

            /// <summary>
            /// Invalid Operation Exception Test Scenario
            /// </summary>
            InvalidOperationExceptionData,

            /// <summary>
            /// Invalid Incomplete Model Test Scenario
            /// </summary>
            InvalidIncompleteModelData,

            /// <summary>
            /// Invalid Argument Exception Test Scenario - Collisions
            /// </summary>
            InvalidCollisionData
        }

        internal static IEnumerable<GenerateTestContext> GetTestDataByDataType(TestDataType testDataType)
        {
            switch (testDataType)
            {
                case TestDataType.ValidData:
                    return GetValidData();

                case TestDataType.InvalidArgumentExceptionData:
                    return GetInvalidArgumentExceptionData();

                case TestDataType.InvalidOperationExceptionData:
                    return GetInvalidOperationExceptionData();

                case TestDataType.InvalidIncompleteModelData:
                    return GetInvalidIncompleteModelData();

                case TestDataType.InvalidCollisionData:
                    return GetInvalidCollisionData();
            }

            return default;
        }

        private static IEnumerable<GenerateTestContext> GetValidData()
        {
            yield return new GenerateTestContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context - All Ignored",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
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
                    .Generate(),
                AdditionalAsserts = (model) =>
                {
                    Assert.IsNull(model.Id);
                    Assert.IsNull(model.Name);
                    Assert.IsNull(model.Total);
                }
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 2,
                CaseDescription = "Custom Scenario Context - All Ignored",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Generate(),
                AdditionalAsserts = (model) =>
                {
                    Assert.IsNull(model.Id);
                    Assert.IsNull(model.Name);
                    Assert.IsNull(model.Total);
                }
            };

            var id = 1;
            var name = "a";
            var total = 200m;

            yield return new GenerateTestContext
            {
                CaseNumber = 3,
                CaseDescription = "Default Scenario Context - Valid Only Set",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, id)
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Generate(ScenarioBuilderType.ValidOnly),
                AdditionalAsserts = (model) =>
                {
                    Assert.AreEqual(id, model.Id);
                    Assert.AreEqual(name, model.Name);
                    Assert.AreEqual(total, model.Total);
                }
            };

            id = 2;
            name = "b";
            total = 400m;

            yield return new GenerateTestContext
            {
                CaseNumber = 4,
                CaseDescription = "Default Scenario Context - Invalid Only Set",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, id)
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Generate(ScenarioBuilderType.InvalidOnly),
                AdditionalAsserts = (model) =>
                {
                    Assert.AreEqual(id, model.Id);
                    Assert.AreEqual(name, model.Name);
                    Assert.AreEqual(total, model.Total);
                }
            };

            id = 1;
            name = "a";
            total = 200m;

            yield return new GenerateTestContext
            {
                CaseNumber = 5,
                CaseDescription = "Default Scenario Context - Get Valid Only by Type",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, id)
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, 10)
                            .RuleFor(f => f.Name, "CC")
                            .RuleFor(f => f.Total, 951m),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Generate(ScenarioBuilderType.ValidOnly),
                AdditionalAsserts = (model) =>
                {
                    Assert.AreEqual(id, model.Id);
                    Assert.AreEqual(name, model.Name);
                    Assert.AreEqual(total, model.Total);
                }
            };

            id = 2;
            name = "b";
            total = 400m;

            yield return new GenerateTestContext
            {
                CaseNumber = 6,
                CaseDescription = "Default Scenario Context - Get Invalid Only by Type",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, 10)
                            .RuleFor(f => f.Name, "CC")
                            .RuleFor(f => f.Total, 951m),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, id)
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Generate(ScenarioBuilderType.InvalidOnly),
                AdditionalAsserts = (model) =>
                {
                    Assert.AreEqual(id, model.Id);
                    Assert.AreEqual(name, model.Name);
                    Assert.AreEqual(total, model.Total);
                }
            };

            id = 1;
            name = "a";
            total = 200m;

            yield return new GenerateTestContext
            {
                CaseNumber = 7,
                CaseDescription = "Default Scenario Context - Get Valid Only by Config",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, id)
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, 10)
                            .RuleFor(f => f.Name, "CC")
                            .RuleFor(f => f.Total, 951m))
                    .Generate(
                        ScenarioBuilderType.All,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName }
                        }),
                AdditionalAsserts = (model) =>
                {
                    Assert.AreEqual(id, model.Id);
                    Assert.AreEqual(name, model.Name);
                    Assert.AreEqual(total, model.Total);
                }
            };

            id = 2;
            name = "b";
            total = 400m;

            yield return new GenerateTestContext
            {
                CaseNumber = 8,
                CaseDescription = "Default Scenario Context - Get Invalid Only by Config",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, 10)
                            .RuleFor(f => f.Name, "CC")
                            .RuleFor(f => f.Total, 951m))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, id)
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total))
                    .Generate(
                        ScenarioBuilderType.All,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName }
                        }),
                AdditionalAsserts = (model) =>
                {
                    Assert.AreEqual(id, model.Id);
                    Assert.AreEqual(name, model.Name);
                    Assert.AreEqual(total, model.Total);
                }
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 9,
                CaseDescription = "Default Scenario Context - Using Faker",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, f => f.Random.Int(1))
                            .RuleFor(f => f.Name, f => f.Name.FullName())
                            .RuleFor(f => f.Total, f => f.Random.Decimal(1)),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id)
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Generate(ScenarioBuilderType.ValidOnly),
                AdditionalAsserts = (model) =>
                {
                    Assert.IsNotNull(model.Id);
                    Assert.IsTrue(model.Id > 0);
                    Assert.IsNotNull(model.Name);
                    Assert.IsNotNull(model.Total);
                    Assert.IsTrue(model.Total > 0);
                }
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 10,
                CaseDescription = "Custom Scenario Context - Using Faker",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, f => f.Random.Int(1)))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Name, f => f.Name.FullName())
                            .RuleFor(f => f.Total, f => f.Random.Decimal(1)))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Generate(
                        ScenarioBuilderType.All,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextName, ScenarioTestName }
                        }),
                AdditionalAsserts = (model) =>
                {
                    Assert.IsNotNull(model.Id);
                    Assert.IsTrue(model.Id > 0);
                    Assert.IsNotNull(model.Name);
                    Assert.IsNotNull(model.Total);
                    Assert.IsTrue(model.Total > 0);
                }
            };

            name = "a";
            total = 200m;

            yield return new GenerateTestContext
            {
                CaseNumber = 11,
                CaseDescription = "Custom Scenario Context - Replacing Rules",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, 3)
                            .RuleFor(f => f.Name, "E")
                            .RuleFor(f => f.Total, 101m))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Id, 4)
                            .RuleFor(f => f.Name, "F")
                            .RuleFor(f => f.Total, 100m))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .RuleFor(f => f.Name, name)
                            .RuleFor(f => f.Total, total))
                    .Generate(),
                AdditionalAsserts = (model) =>
                {
                    Assert.AreEqual(name, model.Name);
                    Assert.AreEqual(total, model.Total);
                }
            };
        }

        private static IEnumerable<GenerateTestContext> GetInvalidArgumentExceptionData()
        {
            yield return new GenerateTestContext
            {
                CaseNumber = 1,
                CaseDescription = "Invalid Scenario Builder Type",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .Generate((ScenarioBuilderType)999),
                ExpectedExceptionMessage = Errors.ScenarioBuilderTypeInvalid
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 2,
                CaseDescription = "Scenario Builder doesn't contain Always Valid Scenarios",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .Generate(ScenarioBuilderType.ValidOnly),
                ExpectedExceptionMessage = Errors.ScenarioBuilderDoesnotContainAlwaysValidKnownScenario
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 3,
                CaseDescription = "Scenario Builder doesn't contain Always Invalid Scenarios",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .Generate(ScenarioBuilderType.InvalidOnly),
                ExpectedExceptionMessage = Errors.ScenarioBuilderDoesnotContainAlwaysInvalidKnownScenario
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 4,
                CaseDescription = "Scenario Builder with empty Context",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .Generate(
                        ScenarioBuilderType.All,
                        new Dictionary<string, string>
                        {
                            { string.Empty, string.Empty }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigWithInvalidScenarioContext,
                    string.Empty)
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 5,
                CaseDescription = "Scenario Builder with invalid context",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
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
                    .Generate(
                        ScenarioBuilderType.All,
                        new Dictionary<string, string>
                        {
                            { ScenarioInvalidContextName, string.Empty }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.KnownScenarioCombinationConfigWithInvalidScenarioContext,
                    ScenarioInvalidContextName)
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 6,
                CaseDescription = "Scenario Builder with invalid scenario",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
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
                    .Generate(
                        ScenarioBuilderType.All,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, ScenarioTestInvalidName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigWithInvalidScenario,
                    Defaults.ScenarioContextName,
                    ScenarioTestInvalidName)
            };
        }

        private static IEnumerable<GenerateTestContext> GetInvalidOperationExceptionData()
        {
            yield return new GenerateTestContext
            {
                CaseNumber = 1,
                CaseDescription = "Empty Default Scenario Context",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .Generate(),
                ExpectedExceptionMessage = Errors.DefaultScenarioContextWithoutValidScenario
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context with Valid Scenario Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate(),
                ExpectedExceptionMessage = Errors.DefaultScenarioContextWithoutInvalidScenario
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 3,
                CaseDescription = "Default Scenario Context with Invalid Scenario Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate(),
                ExpectedExceptionMessage = Errors.DefaultScenarioContextWithoutValidScenario
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 4,
                CaseDescription = "Custom Scenario Context Empty",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Generate(),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioContextIncomplete,
                    ScenarioContextName),
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 5,
                CaseDescription = "Custom Scenario with Only One Scenario",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate(),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioContextIncomplete,
                    ScenarioContextName),
            };
        }

        private static IEnumerable<GenerateTestContext> GetInvalidIncompleteModelData()
        {
            yield return new GenerateTestContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate()
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 2,
                CaseDescription = "Custom Scenario Context",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate()
            };
        }

        private static IEnumerable<GenerateTestContext> GetInvalidCollisionData()
        {
            var exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName);

            yield return new GenerateTestContext
            {
                CaseNumber = 1,
                CaseDescription = "Default Scenario Context - Exact Default Valid as Invalid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
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
                    .Generate(
                        ScenarioBuilderType.InvalidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.InvalidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysValid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioInvalidName);

            yield return new GenerateTestContext
            {
                CaseNumber = 2,
                CaseDescription = "Default Scenario Context - Exact Default Invalid as Valid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
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
                    .Generate(
                        ScenarioBuilderType.ValidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.ValidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysInvalid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(2, ScenarioTestName);

            yield return new GenerateTestContext
            {
                CaseNumber = 3,
                CaseDescription = "Custom Scenario Context - Exact Custom Invalid as Valid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Generate(
                        ScenarioBuilderType.ValidOnly,
                        new Dictionary<string, string>
                        {
                            { ScenarioContextName, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.ValidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysInvalid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(2, ScenarioTestNameAlternative);

            yield return new GenerateTestContext
            {
                CaseNumber = 4,
                CaseDescription = "Custom Scenario Context - Exact Custom Valid as Invalid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysInvalid)
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .Generate(
                        ScenarioBuilderType.InvalidOnly,
                        new Dictionary<string, string>
                        {
                            { ScenarioContextName, ScenarioTestNameAlternative }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.InvalidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysValid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName);

            yield return new GenerateTestContext
            {
                CaseNumber = 5,
                CaseDescription = "Custom Scenario Context - Combination with existing default as Valid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysInvalid)
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Generate(
                        ScenarioBuilderType.InvalidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextName, ScenarioTestNameAlternative }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.InvalidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysValid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioInvalidName);

            yield return new GenerateTestContext
            {
                CaseNumber = 6,
                CaseDescription = "Custom Scenario Context - Combination with existing default as Invalid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysValid)
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id),
                        ScenarioCombinationType.AlwaysInvalid)
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Generate(
                        ScenarioBuilderType.ValidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextName, ScenarioTestNameAlternative }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.ValidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysInvalid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(2, ScenarioTestNameAlternative);

            yield return new GenerateTestContext
            {
                CaseNumber = 7,
                CaseDescription = "Custom Scenario Context - Combination with existing custom as Invalid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Generate(
                        ScenarioBuilderType.ValidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextName, ScenarioTestNameAlternative }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.ValidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysInvalid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(2, ScenarioTestName);

            yield return new GenerateTestContext
            {
                CaseNumber = 8,
                CaseDescription = "Custom Scenario Context - Combination with existing custom as Invalid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysValid)
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total),
                        ScenarioCombinationType.AlwaysInvalid)
                    .Generate(
                        ScenarioBuilderType.InvalidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextName, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.InvalidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysValid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioInvalidName) +
                Keys.GetScenarioContextKeyValue(2, ScenarioTestName);

            yield return new GenerateTestContext
            {
                CaseNumber = 9,
                CaseDescription = "Custom Scenario Context - Combination with existing Known Combination as Invalid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextName, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextName, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .Generate(
                        ScenarioBuilderType.ValidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextName, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.ValidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysInvalid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                Keys.GetScenarioContextKeyValue(2, ScenarioTestName);

            yield return new GenerateTestContext
            {
                CaseNumber = 10,
                CaseDescription = "Custom Scenario Context - Combination with existing Known Combination as Valid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name)
                            .Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextName, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextName, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .Generate(
                        ScenarioBuilderType.InvalidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextName, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.InvalidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysValid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioValidName) +
                Keys.GetScenarioContextKeyValue(3, ScenarioTestName);

            yield return new GenerateTestContext
            {
                CaseNumber = 11,
                CaseDescription = "Custom Scenario Context - Combination with existing Known Combination as Valid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .Generate(
                        ScenarioBuilderType.InvalidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextName, ScenarioTestName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.InvalidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysValid)
            };

            exceptionKey =
                Keys.GetScenarioContextKeyValue(1, Defaults.ScenarioInvalidName) +
                Keys.GetScenarioContextKeyValue(3, ScenarioTestName);

            yield return new GenerateTestContext
            {
                CaseNumber = 12,
                CaseDescription = "Custom Scenario Context - Combination with existing Known Combination as Valid Only",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .Generate(
                        ScenarioBuilderType.ValidOnly,
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextName, ScenarioTestName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasConflictWithKnownScenarioCombinationConfig,
                    ScenarioBuilderType.ValidOnly,
                    exceptionKey,
                    ScenarioCombinationType.AlwaysInvalid)
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 13,
                CaseDescription = "Custom Scenario Context - Combination without compatible known scenario",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .Generate(
                        ScenarioBuilderType.ValidOnly,
                        new Dictionary<string, string>
                        {
                            { ScenarioContextName, ScenarioTestName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasNoCompatibleKnownScenarioCombinationConfig,
                    ScenarioBuilderType.ValidOnly)
            };

            yield return new GenerateTestContext
            {
                CaseNumber = 14,
                CaseDescription = "Custom Scenario Context - Combination without compatible known scenario",
                TestedAction = () => new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(ScenarioContextName)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Name))
                    .ScenarioContext(ScenarioContextNameAlternative)
                    .Scenario(
                        ScenarioTestName,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .Scenario(
                        ScenarioTestNameAlternative,
                        scenarioRuleSet => scenarioRuleSet
                            .Ignore(f => f.Total))
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysInvalid)
                    .KnownScenarioCombination(
                        new Dictionary<string, string>
                        {
                            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
                            { ScenarioContextNameAlternative, ScenarioTestName }
                        },
                        ScenarioCombinationType.AlwaysValid)
                    .Generate(
                        ScenarioBuilderType.InvalidOnly,
                        new Dictionary<string, string>
                        {
                            { ScenarioContextName, ScenarioTestName }
                        }),
                ExpectedExceptionMessage = string.Format(
                    Errors.ScenarioCombinationConfigHasNoCompatibleKnownScenarioCombinationConfig,
                    ScenarioBuilderType.InvalidOnly)
            };
        }
    }
}