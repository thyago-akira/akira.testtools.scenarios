using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Context;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerGetMinimumTestingScenarioCombinationsTests
    {
        public static IEnumerable<object[]> GetValidData()
            => GetMinimumTestingScenarioCombinationsTestData.GetTestData();

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GetMinimumKnownScenarioCombinationsForTestingNull_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumTestingScenarioCombinations();

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountAll,
                builders.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GetMinimumKnownScenarioCombinationsForTestingAll_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumTestingScenarioCombinations(ScenarioBuilderType.All);

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountAll,
                builders.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GetMinimumKnownScenarioCombinationsForTestingValidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumTestingScenarioCombinations(ScenarioBuilderType.ValidOnly);

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountValidOnly,
                builders.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_GetMinimumKnownScenarioCombinationsForTestingInvalidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumTestingScenarioCombinations(ScenarioBuilderType.InvalidOnly);

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountInvalidOnly,
                builders.Count());
        }
    }
}