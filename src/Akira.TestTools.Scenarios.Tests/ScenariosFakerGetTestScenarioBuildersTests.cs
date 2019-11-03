using System.Collections.Generic;
using System.Linq;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Context;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerGetTestScenarioBuildersTests
    {
        public static IEnumerable<object[]> GetValidData()
            => GetTestScenarioBuildersTestData.GetTestData();

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_RuleSet_GetTestScenarioBuildersNull_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumKnownScenarioCombinationsForTesting();

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountAll,
                builders.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_RuleSet_GetTestScenarioBuildersAll_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumKnownScenarioCombinationsForTesting(ScenarioBuilderType.All);

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountAll,
                builders.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_RuleSet_GetTestScenarioBuildersValidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumKnownScenarioCombinationsForTesting(ScenarioBuilderType.ValidOnly);

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountValidOnly,
                builders.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_RuleSet_GetTestScenarioBuildersInvalidOnly_ReturnsTestScenariosBuilders(
            TestBuilderContext getTestBuildersTestContext)
        {
            // Act
            var builders = getTestBuildersTestContext.GetFaker().GetMinimumKnownScenarioCombinationsForTesting(ScenarioBuilderType.InvalidOnly);

            // Assert
            Assert.IsNotNull(builders);
            Assert.AreEqual(
                getTestBuildersTestContext.ExpectedCountInvalidOnly,
                builders.Count());
        }
    }
}