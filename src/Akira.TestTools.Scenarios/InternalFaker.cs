using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios;
using Bogus;

namespace Akira.TestTools.Scenarios
{
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "All underscores are discards.")]
    public class InternalFaker<T> : Faker<T>, IScenarioRuleSet<T>
        where T : class
    {
        public InternalFaker()
        {
            _ = this.StrictMode(true);
        }

        public new IScenarioRuleSet<T> Ignore<TProperty>(
            Expression<Func<T, TProperty>> propertyOrField)
        {
            _ = base.Ignore(propertyOrField);

            return this;
        }

        public new IScenarioRuleSet<T> RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<TProperty> getValue)
        {
            _ = base.RuleFor(
                property,
                getValue);

            return this;
        }

        public new IScenarioRuleSet<T> RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            TProperty value)
        {
            _ = base.RuleFor(
                property,
                value);

            return this;
        }

        public new IScenarioRuleSet<T> RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<Faker, T, TProperty> getValue)
        {
            _ = base.RuleFor(
                property,
                getValue);

            return this;
        }

        public new IScenarioRuleSet<T> RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<Faker, TProperty> getValue)
        {
            _ = base.RuleFor(
                property,
                getValue);

            return this;
        }
    }
}