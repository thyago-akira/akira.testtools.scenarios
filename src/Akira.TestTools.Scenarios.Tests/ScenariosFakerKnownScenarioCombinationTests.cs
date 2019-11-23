using System;
using System.Collections.Generic;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Extensions;
using Akira.TestTools.Scenarios.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerKnownScenarioCombinationTests : BaseScenariosFakerTests
    {
        public static IEnumerable<object[]> GetValidData()
            => KnownScenarioCombinationTestData
                .GetTestDataByDataType(KnownScenarioCombinationTestData.TestDataType.ValidData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetInvalidData()
            => KnownScenarioCombinationTestData
                .GetTestDataByDataType(KnownScenarioCombinationTestData.TestDataType.InvalidData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetScenariosColisionInvalidData()
            => KnownScenarioCombinationTestData
                .GetTestDataByDataType(KnownScenarioCombinationTestData.TestDataType.ScenariosCollisionInvalidData)
                .GetTestDynamicData();

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_KnownScenarioCombination_(
            Context.TestContext scenarioContext)
        {
            // Action
            var scenariosFaker = scenarioContext.TestedAction();

            // Assert
            Assert.IsNotNull(scenariosFaker);
            Assert.IsInstanceOfType(scenariosFaker, typeof(ScenariosFaker<SimpleModel>));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_KnownScenarioCombination_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenariosColisionInvalidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_KnownScenarioCombination_Colision_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }
    }
}