using System;
using System.Collections.Generic;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerScenarioContextTests : BaseScenariosFakerTests
    {
        public static IEnumerable<object[]> GetScenarioValidDataForCompleteFaker()
            => ScenarioContextTestData
                .GetValidData()
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetScenarioInvalidDataForCompleteFaker()
            => ScenarioContextTestData
                .GetInvalidDataWithCompletedFakers()
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetScenarioInvalidDataForIncompleteFaker()
            => ScenarioContextTestData
                .GetInvalidDataWithIncompletedFakers()
                .GetTestDynamicData();

        [DataTestMethod]
        [DynamicData(nameof(GetScenarioValidDataForCompleteFaker), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Scenario_CompleteFaker_ReturnsScenariosFaker(
            Context.TestContext addScenarioContext)
        {
            // Action
            var scenariosFaker = addScenarioContext.TestedAction();

            // Assert
            Assert.IsNotNull(scenariosFaker);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenarioInvalidDataForIncompleteFaker), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Scenario_IncompleteFaker_ThrowsException(
            Context.TestContext addScenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<InvalidOperationException>(addScenarioContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetScenarioInvalidDataForCompleteFaker), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Scenario_CompleteFaker_ThrowsException(
            Context.TestContext addScenarioContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(addScenarioContext);
        }
    }
}