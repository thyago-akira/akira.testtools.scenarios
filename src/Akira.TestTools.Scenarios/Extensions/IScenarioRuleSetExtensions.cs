using System;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios.Collections;

namespace Akira.TestTools.Scenarios.Extensions
{
    public static class IScenarioRuleSetExtensions
    {
        public static IScenarioRuleSet<T> Ignore<T, TProperty>(
            this IScenarioRuleSet<T> scenarioRuleSet,
            Expression<Func<T, TProperty>> property)
            where T : class
        {
            scenarioRuleSet.IgnoreProperty(property);

            return scenarioRuleSet;
        }

        public static IScenarioRuleSet<T> RuleFor<T, TProperty>(
            this IScenarioRuleSet<T> scenarioRuleSet,
            Expression<Func<T, TProperty>> property,
            Func<T, TProperty> getValue)
            where T : class
        {
            scenarioRuleSet.RuleForProperty(
                property,
                getValue);

            return scenarioRuleSet;
        }

        public static IScenarioRuleSet<T> RuleFor<T, TProperty>(
            this IScenarioRuleSet<T> scenarioRuleSet,
            Expression<Func<T, TProperty>> property,
            Func<TProperty> getValue)
            where T : class
        {
            scenarioRuleSet.RuleForProperty(
                property,
                getValue);

            return scenarioRuleSet;
        }

        public static IScenarioRuleSet<T> RuleFor<T, TProperty>(
            this IScenarioRuleSet<T> scenarioRuleSet,
            Expression<Func<T, TProperty>> property,
            TProperty value)
            where T : class
        {
            scenarioRuleSet.RuleForProperty(
                property,
                value);

            return scenarioRuleSet;
        }
    }
}