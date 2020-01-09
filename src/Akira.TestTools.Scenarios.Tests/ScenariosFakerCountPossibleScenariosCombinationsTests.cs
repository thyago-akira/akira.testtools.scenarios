using System.Collections.Generic;
using Akira.TestTools.Scenarios.Tests.Context;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerCountPossibleScenariosCombinationsTests
    {
        public static IEnumerable<object[]> GetValidData()
            => GenerateMinimumTestingScenariosTestData
                .GetTestDataByDataType(GenerateMinimumTestingScenariosTestData.TestDataType.ValidData)
                .GetTestDynamicData();

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_CountPossibleScenariosCombinations_ReturnsCount(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var faker = getTestBuildersTestContext.GetFaker();

            // Assert
            Assert.IsNotNull(faker);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountPossibleScenariosCombinations,
                faker.BuilderRepository.CountCompletedModelBuilders);
        }
    }
}