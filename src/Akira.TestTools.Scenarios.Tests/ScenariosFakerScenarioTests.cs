using System;
using System.Collections.Generic;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerScenarioTests : BaseScenariosFakerTests
    {
        public static IEnumerable<object[]> GetDefaultScenarioContextScenarioAfterCustomScenarioContext()
            => ScenarioTestData.GetTestData(ScenarioTestData.TestDataType.DefaultScenarioContextScenarioAfterCustomScenarioContext);

        public static IEnumerable<object[]> GetDefaultScenarioContextScenarioUsingCustomScenarioContextScenario()
            => ScenarioTestData.GetTestData(ScenarioTestData.TestDataType.DefaultScenarioContextScenarioUsingCustomScenarioContextScenario);

        public static IEnumerable<object[]> GetScenarioWithNullAction()
            => ScenarioTestData.GetTestData(ScenarioTestData.TestDataType.ScenarioWithNullAction);

        public static IEnumerable<object[]> GetScenarioWithInvalidScenarioType()
            => ScenarioTestData.GetTestData(ScenarioTestData.TestDataType.ScenarioWithInvalidScenarioType);

        public static IEnumerable<object[]> GetScenarioWithEmptyScenarioName()
            => ScenarioTestData.GetTestData(ScenarioTestData.TestDataType.ScenarioWithEmptyScenarioName);

        public static IEnumerable<object[]> GetScenarioWithExistingScenarioName()
            => ScenarioTestData.GetTestData(ScenarioTestData.TestDataType.ScenarioWithExistingScenarioName);

        public static IEnumerable<object[]> GetValidData()
            => ScenarioTestData.GetTestData(ScenarioTestData.TestDataType.ValidData);

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Scenario_ValidScenario_ReturnsScenariosFaker(
            Context.TestContext scenarioContext)
        {
            // Action
            var scenariosFaker = scenarioContext.TestedAction();

            // Assert
            Assert.IsNotNull(scenariosFaker);
            Assert.IsInstanceOfType(scenariosFaker, typeof(ScenariosFaker<SimpleModel>));
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDefaultScenarioContextScenarioAfterCustomScenarioContext), DynamicDataSourceType.Method)]
        public void ScenariosFaker_DefaultScenarioContext_SetScenarioAfterCustomScenarioContext_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<InvalidOperationException>(scenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetDefaultScenarioContextScenarioUsingCustomScenarioContextScenario), DynamicDataSourceType.Method)]
        public void ScenariosFaker_DefaultScenarioContext_SetScenarioUsingCustomScenarioContextScenario_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<InvalidOperationException>(scenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenarioWithNullAction), DynamicDataSourceType.Method)]
        public void ScenariosFaker_ScenarioWithNullAction_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenarioWithInvalidScenarioType), DynamicDataSourceType.Method)]
        public void ScenariosFaker_ScenarioWithInvalidScenarioType_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenarioWithEmptyScenarioName), DynamicDataSourceType.Method)]
        public void ScenariosFaker_ScenarioWithEmptyScenarioName_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenarioWithExistingScenarioName), DynamicDataSourceType.Method)]
        public void ScenariosFaker_ScenarioWithExistingScenarioName_ThrowsException(
            Context.TestContext scenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(scenarioContext);
        }
    }
}