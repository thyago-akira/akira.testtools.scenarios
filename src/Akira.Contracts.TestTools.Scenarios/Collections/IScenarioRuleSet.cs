using System;
using System.Linq.Expressions;

namespace Akira.Contracts.TestTools.Scenarios.Collections
{
    public interface IScenarioRuleSet<T>
        where T : class
    {
        void IgnoreProperty<TProperty>(
            Expression<Func<T, TProperty>> property);

        void RuleForProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<T, TProperty> getValue);

        void RuleForProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<TProperty> getValue);

        void RuleForProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            TProperty value);
    }
}