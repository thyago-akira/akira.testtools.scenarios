﻿using System;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Bogus;

namespace Akira.TestTools.Scenarios.Extensions
{
    public static class IScenarioRuleSetFakerExtensions
    {
        private static readonly Faker Faker = new Faker();

        public static IScenarioRuleSet<T> RuleFor<T, TProperty>(
            this IScenarioRuleSet<T> scenarioRuleSet,
            Expression<Func<T, TProperty>> property,
            Func<Faker, T, TProperty> getValue)
            where T : class
        {
            if (scenarioRuleSet is Faker<T> faker)
            {
                _ = faker.RuleFor(
                    property,
                    getValue);
            }
            else
            {
                scenarioRuleSet.RuleFor(
                    property,
                    t => getValue(Faker, t));
            }

            return scenarioRuleSet;
        }

        public static IScenarioRuleSet<T> RuleFor<T, TProperty>(
            this IScenarioRuleSet<T> scenarioRuleSet,
            Expression<Func<T, TProperty>> property,
            Func<Faker, TProperty> getValue)
            where T : class
        {
            if (scenarioRuleSet is Faker<T> faker)
            {
                _ = faker.RuleFor(
                    property,
                    getValue);
            }
            else
            {
                scenarioRuleSet.RuleFor(
                    property,
                    () => getValue(Faker));
            }

            return scenarioRuleSet;
        }
    }
}