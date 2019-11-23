using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Context;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerGenerateMinimumTestingScenariosTests
    {
        public static IEnumerable<object[]> GetValidData()
            => GenerateMinimumTestingScenariosTestData
                .GetTestDataByDataType(GenerateMinimumTestingScenariosTestData.TestDataType.ValidData)
                .GetTestDynamicData();

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GenerateMinimumTestingScenariosForTestingNull_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var models = getTestBuildersTestContext.GetFaker().GenerateMinimumTestingScenarios();

            // Assert
            Assert.IsNotNull(models);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountAll,
                models.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GenerateMinimumTestingScenariosForTestingAll_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var models = getTestBuildersTestContext.GetFaker().GenerateMinimumTestingScenarios(ScenarioBuilderType.All);

            // Assert
            Assert.IsNotNull(models);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountAll,
                models.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GenerateMinimumTestingScenariosForTestingValidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var models = getTestBuildersTestContext.GetFaker().GenerateMinimumTestingScenarios(ScenarioBuilderType.ValidOnly);

            // Assert
            Assert.IsNotNull(models);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountValidOnly,
                models.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GenerateMinimumTestingScenariosForTestingInvalidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var models = getTestBuildersTestContext.GetFaker().GenerateMinimumTestingScenarios(ScenarioBuilderType.InvalidOnly);

            // Assert
            Assert.IsNotNull(models);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountInvalidOnly,
                models.Count());
        }
    }
}