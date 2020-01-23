using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Enums;
using Akira.TestTools.Scenarios.Tests.Context;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosBuilderGenerateMinimumTestingScenariosTests
    {
        public static IEnumerable<object[]> GetValidData()
            => GenerateMinimumTestingScenariosTestData
                .GetTestDataByDataType(GenerateMinimumTestingScenariosTestData.TestDataType.ValidData)
                .GetTestDynamicData();

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosBuilder_GenerateMinimumTestingScenariosForTestingNull_ReturnsTestScenariosBuilders(
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
        public void ScenariosBuilder_GenerateMinimumTestingScenariosForTestingAll_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var models = getTestBuildersTestContext.GetFaker().GenerateMinimumTestingScenarios(BuilderType.All);

            // Assert
            Assert.IsNotNull(models);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountAll,
                models.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosBuilder_GenerateMinimumTestingScenariosForTestingValidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var models = getTestBuildersTestContext.GetFaker().GenerateMinimumTestingScenarios(BuilderType.ValidOnly);

            // Assert
            Assert.IsNotNull(models);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountValidOnly,
                models.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosBuilder_GenerateMinimumTestingScenariosForTestingInvalidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var models = getTestBuildersTestContext.GetFaker().GenerateMinimumTestingScenarios(BuilderType.InvalidOnly);

            // Assert
            Assert.IsNotNull(models);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountInvalidOnly,
                models.Count());
        }
    }
}