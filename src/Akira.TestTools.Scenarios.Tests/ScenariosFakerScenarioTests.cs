using System;
using System.Collections.Generic;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Extensions;
using Akira.TestTools.Scenarios.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerScenarioTests : BaseScenariosFakerTests
    {
        public static IEnumerable<object[]> GetDefaultScenarioContextScenarioAfterCustomScenarioContext()
            => ScenarioTestData
                .GetTestDataByDataType(ScenarioTestData.TestDataType.DefaultScenarioContextScenarioAfterCustomScenarioContext)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetDefaultScenarioContextScenarioUsingCustomScenarioContextScenario()
            => ScenarioTestData
                .GetTestDataByDataType(ScenarioTestData.TestDataType.DefaultScenarioContextScenarioUsingCustomScenarioContextScenario)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetScenarioWithNullAction()
            => ScenarioTestData
                .GetTestDataByDataType(ScenarioTestData.TestDataType.ScenarioWithNullAction)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetScenarioWithInvalidScenarioType()
            => ScenarioTestData
                .GetTestDataByDataType(ScenarioTestData.TestDataType.ScenarioWithInvalidScenarioType)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetScenarioWithEmptyScenarioName()
            => ScenarioTestData
                .GetTestDataByDataType(ScenarioTestData.TestDataType.ScenarioWithEmptyScenarioName)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetScenarioWithExistingScenarioName()
            => ScenarioTestData
                .GetTestDataByDataType(ScenarioTestData.TestDataType.ScenarioWithExistingScenarioName)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetValidData()
            => ScenarioTestData
                .GetTestDataByDataType(ScenarioTestData.TestDataType.ValidData)
                .GetTestDynamicData();

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