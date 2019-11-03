using System;
using System.Linq.Expressions;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface IScenarioRuleSet<T>
        where T : class
    {
        IScenarioRuleSet<T> Ignore<TProperty>(Expression<Func<T, TProperty>> property);

        IScenarioRuleSet<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, Func<TProperty> getValue);

        IScenarioRuleSet<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, TProperty value);
    }
}