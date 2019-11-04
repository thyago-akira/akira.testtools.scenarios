using System;
using System.Collections.Generic;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerAddKnownScenarioCombinationTests : BaseScenariosFakerTests
    {
        public static IEnumerable<object[]> GetValidData()
            => AddKnownScenarioCombinationTestData.GetTestData(AddKnownScenarioCombinationTestData.TestDataType.ValidData);

        public static IEnumerable<object[]> GetInvalidData()
            => AddKnownScenarioCombinationTestData.GetTestData(AddKnownScenarioCombinationTestData.TestDataType.InvalidData);

        public static IEnumerable<object[]> GetScenariosColisionInvalidData()
            => AddKnownScenarioCombinationTestData.GetTestData(AddKnownScenarioCombinationTestData.TestDataType.ScenariosCollisionInvalidData);

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_AddKnownScenarioCombination_(
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
        public void ScenariosFaker_AddKnownScenarioCombination_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenariosColisionInvalidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_AddKnownScenarioCombination_Colision_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }
    }
}