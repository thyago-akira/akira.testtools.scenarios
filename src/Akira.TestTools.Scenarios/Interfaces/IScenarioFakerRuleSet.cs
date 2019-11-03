using System;
using System.Linq.Expressions;

namespace Akira.TestTools.Scenarios.Interfaces
{
    public interface IScenarioFakerRuleSet<T>
        where T : class
    {
        IScenarioFakerRuleSet<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, Func<Bogus.Faker, T, TProperty> getValue);

        IScenarioFakerRuleSet<T> RuleFor<TProperty>(Expression<Func<T, TProperty>> property, Func<Bogus.Faker, TProperty> getValue);
    }
}