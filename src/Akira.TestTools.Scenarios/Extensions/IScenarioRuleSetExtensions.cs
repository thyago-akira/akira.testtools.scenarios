using System;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios;
using Bogus;

namespace Akira.TestTools.Scenarios.Extensions
{
    public static class IScenarioRuleSetExtensions
    {
        private static readonly Faker faker = new Faker();

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
                _ = scenarioRuleSet.RuleFor(
                    property,
                    t => getValue(IScenarioRuleSetExtensions.faker, t));
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
                _ = scenarioRuleSet.RuleFor(
                    property,
                    () => getValue(IScenarioRuleSetExtensions.faker));
            }

            return scenarioRuleSet;
        }
    }
}