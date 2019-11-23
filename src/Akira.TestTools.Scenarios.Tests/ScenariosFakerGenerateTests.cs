using System;
using System.Collections.Generic;
using System.Linq;
using Akira.TestTools.Scenarios.Tests.Context.Data;
using Akira.TestTools.Scenarios.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerGenerateTests : BaseScenariosFakerTests
    {
        private const string TestScenarioContextName = nameof(TestScenarioContextName);

        private const string TestScenarioName = nameof(TestScenarioName);

        private const string TestScenarioNameAlternative = nameof(TestScenarioNameAlternative);

        public static IEnumerable<object[]> GetValidData()
            => GenerateTestData
                .GetTestDataByDataType(GenerateTestData.TestDataType.ValidData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetMultipleValidData()
            => GenerateMultipleTestData
                .GetTestDataByDataType(GenerateMultipleTestData.TestDataType.ValidData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetInvalidArgumentExceptionData()
            => GenerateTestData
                .GetTestDataByDataType(GenerateTestData.TestDataType.InvalidArgumentExceptionData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetInvalidOperationExceptionData()
            => GenerateTestData
                .GetTestDataByDataType(GenerateTestData.TestDataType.InvalidOperationExceptionData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetInvalidIncompleteModelData()
            => GenerateTestData
                .GetTestDataByDataType(GenerateTestData.TestDataType.InvalidIncompleteModelData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetInvalidArgumentExceptionCollisionData()
            => GenerateTestData
                .GetTestDataByDataType(GenerateTestData.TestDataType.InvalidCollisionData)
                .GetTestDynamicData();

        public static IEnumerable<object[]> GetMultipleInvalidArgumentExceptionData()
            => GenerateMultipleTestData
                .GetTestDataByDataType(GenerateMultipleTestData.TestDataType.InvalidArgumentExceptionData)
                .GetTestDynamicData();

        [DataTestMethod]
        [DynamicData(nameof(GetValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Generate_ReturnsValidModel(
            Context.GenerateTestContext testContext)
        {
            // Action
            var model = testContext.TestedAction();

            // Asserts
            Assert.IsNotNull(model);

            testContext.AdditionalAsserts?.Invoke(model);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetMultipleValidData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Generate_ReturnsValidModel(
            Context.GenerateMultipleTestContext testContext)
        {
            // Action
            var list = testContext.TestedAction();

            // Asserts
            Assert.IsNotNull(list);
            Assert.AreEqual(testContext.NumberOfRows, list.Count());
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidArgumentExceptionData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Generate_ThrowsArgumentException(
            Context.GenerateTestContext testContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(testContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidOperationExceptionData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Generate_ThrowsInvalidOperationException(
            Context.GenerateTestContext testContext)
        {
            // Action && Assert
            this.AssertThrowsException<InvalidOperationException>(testContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidIncompleteModelData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Generate_ThrowsValidationException(
            Context.GenerateTestContext testContext)
        {
            // Action && Assert
            this.AssertThrowsException<Bogus.ValidationException>(testContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetInvalidArgumentExceptionCollisionData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Generate_Collision_ThrowsArgumentException(
            Context.GenerateTestContext testContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(testContext);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetMultipleInvalidArgumentExceptionData), DynamicDataSourceType.Method)]
        public void ScenariosFaker_Generate_Multiple_ThrowsArgumentException(
            Context.GenerateMultipleTestContext testContext)
        {
            // Action && Assert
            this.AssertThrowsException<ArgumentException>(testContext);
        }
    }
}