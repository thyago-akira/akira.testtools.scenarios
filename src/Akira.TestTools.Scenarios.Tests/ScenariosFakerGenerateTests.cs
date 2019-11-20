﻿using System;
using Akira.TestTools.Scenarios.Constants;
using Akira.TestTools.Scenarios.Tests.Stubs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    [TestClass]
    public class ScenariosFakerGenerateTests
    {
        private const string TestScenarioContextName = nameof(TestScenarioContextName);

        private const string TestScenarioName = nameof(TestScenarioName);

        private const string TestScenarioNameAlternative = nameof(TestScenarioNameAlternative);

        [TestMethod]
        public void ScenariosFaker_GenerateDefault_WithoutScenarios_ThrowsException()
        {
            // Action && Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
                new ScenariosFaker<SimpleModel>()
                    .Generate());

            Assert.AreEqual(
                Errors.DefaultScenarioContextWithoutValidScenario,
                exception.Message);
        }

        [TestMethod]
        public void ScenariosFaker_GenerateDefault_WithOnlyValid_ThrowsException()
        {
            // Action && Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
                new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate());

            Assert.AreEqual(
                Errors.DefaultScenarioContextWithoutInvalidScenario,
                exception.Message);
        }

        [TestMethod]
        public void ScenariosFaker_GenerateDefault_WithOnlyInvalid_ThrowsException()
        {
            // Action && Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
                new ScenariosFaker<SimpleModel>()
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate());

            Assert.AreEqual(
                Errors.DefaultScenarioContextWithoutValidScenario,
                exception.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(Bogus.ValidationException))]
        public void ScenariosFaker_GenerateDefault_IncompleteModel_ThrowsException()
        {
            // Action
            new ScenariosFaker<SimpleModel>()
                .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .Generate();
        }

        [TestMethod]
        public void ScenariosFaker_GenerateCustom_WithoutScenarios_ThrowsException()
        {
            // Action && Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
                new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(TestScenarioContextName)
                    .Generate());

            Assert.AreEqual(
                string.Format(
                    Errors.ScenarioContextIncomplete,
                    TestScenarioContextName),
                exception.Message);
        }

        [TestMethod]
        public void ScenariosFaker_GenerateCustom_WithOnlyOneScenario_ThrowsException()
        {
            // Action && Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(() =>
                new ScenariosFaker<SimpleModel>()
                    .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .ScenarioContext(TestScenarioContextName)
                    .Scenario(
                        TestScenarioName,
                        scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                    .Generate());

            Assert.AreEqual(
                string.Format(
                    Errors.ScenarioContextIncomplete,
                    TestScenarioContextName),
                exception.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(Bogus.ValidationException))]
        public void ScenariosFaker_GenerateCustom_IncompleteModel_ThrowsException()
        {
            // Action
            new ScenariosFaker<SimpleModel>()
                .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .ScenarioContext(TestScenarioContextName)
                .Scenario(
                    TestScenarioName,
                    scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .Scenario(
                    TestScenarioNameAlternative,
                    scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .Generate();
        }

        [TestMethod]
        public void ScenariosFaker_GenerateDefault_AllIgnored_ModelWithNulls()
        {
            // Action
            var model = new ScenariosFaker<SimpleModel>()
                .DefaultContextValidScenario(
                    scenarioRuleSet => scenarioRuleSet
                        .Ignore(f => f.Id)
                        .Ignore(f => f.Name)
                        .Ignore(f => f.Total))
                .DefaultContextInvalidScenario(
                    scenarioRuleSet => scenarioRuleSet
                        .Ignore(f => f.Id)
                        .Ignore(f => f.Name)
                        .Ignore(f => f.Total))
                .Generate();

            // Asserts
            Assert.IsNotNull(model);
            Assert.IsNull(model.Id);
            Assert.IsNull(model.Name);
            Assert.IsNull(model.Total);
        }

        [TestMethod]
        public void ScenariosFaker_GenerateCustomScenario_AllIgnored_ModelWithNulls()
        {
            // Action
            var model = new ScenariosFaker<SimpleModel>()
                .DefaultContextValidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .DefaultContextInvalidScenario(scenarioRuleSet => scenarioRuleSet.Ignore(f => f.Id))
                .ScenarioContext(TestScenarioContextName)
                .Scenario(
                    TestScenarioName,
                    scenarioRuleSet => scenarioRuleSet
                        .Ignore(f => f.Name)
                        .Ignore(f => f.Total))
                .Scenario(
                    TestScenarioNameAlternative,
                    scenarioRuleSet => scenarioRuleSet
                        .Ignore(f => f.Name)
                        .Ignore(f => f.Total))
                .Generate();

            // Asserts
            Assert.IsNotNull(model);
            Assert.IsNull(model.Id);
            Assert.IsNull(model.Name);
            Assert.IsNull(model.Total);
        }

        [TestMethod]
        public void ScenariosFaker_GenerateCustomScenario_ReplacingRules_NoExceptionAndModelWithNulls()
        {
            // Arrange
            var id = 100;
            var replacedName = "newName";
            var replacedTotal = 202;

            // Action
            var model = new ScenariosFaker<SimpleModel>()
                .DefaultContextValidScenario(
                    scenarioRuleSet => scenarioRuleSet
                        .RuleFor(f => f.Id, id)
                        .RuleFor(f => f.Name, "a")
                        .RuleFor(f => f.Total, 200))
                .DefaultContextInvalidScenario(
                    scenarioRuleSet => scenarioRuleSet
                        .RuleFor(f => f.Id, id)
                        .RuleFor(f => f.Name, "b")
                        .RuleFor(f => f.Total, 201))
                .ScenarioContext(TestScenarioContextName)
                .Scenario(
                    TestScenarioName,
                    scenarioRuleSet => scenarioRuleSet
                        .RuleFor(f => f.Name, replacedName)
                        .RuleFor(f => f.Total, replacedTotal))
                .Scenario(
                    TestScenarioNameAlternative,
                    scenarioRuleSet => scenarioRuleSet
                        .RuleFor(f => f.Name, replacedName)
                        .RuleFor(f => f.Total, replacedTotal))
                .Generate();

            // Asserts
            Assert.IsNotNull(model);
            Assert.AreEqual(id, model.Id);
            Assert.AreEqual(replacedName, model.Name);
            Assert.AreEqual(replacedTotal, model.Total);
        }
    }
}