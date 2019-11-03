using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Interfaces;
using Bogus;

namespace Akira.TestTools.Scenarios
{
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "All underscores are discards.")]
    public class InternalFaker<T> : Faker<T>, IScenarioRuleSet<T>, IScenarioFakerRuleSet<T>
        where T : class
    {
        public InternalFaker()
        {
            _ = this.StrictMode(true);
        }

        IScenarioRuleSet<T> IScenarioRuleSet<T>.Ignore<TProperty>(
            Expression<Func<T, TProperty>> propertyOrField)
        {
            _ = this.Ignore(propertyOrField);

            return this;
        }

        IScenarioRuleSet<T> IScenarioRuleSet<T>.RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<TProperty> getValue)
        {
            _ = this.RuleFor(
                property,
                getValue);

            return this;
        }

        IScenarioRuleSet<T> IScenarioRuleSet<T>.RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            TProperty value)
        {
            _ = this.RuleFor(
                property,
                value);

            return this;
        }

        IScenarioFakerRuleSet<T> IScenarioFakerRuleSet<T>.RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<Faker, T, TProperty> getValue)
        {
            _ = this.RuleFor(
                property,
                getValue);

            return this;
        }

        IScenarioFakerRuleSet<T> IScenarioFakerRuleSet<T>.RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<Faker, TProperty> getValue)
        {
            _ = this.RuleFor(
                property,
                getValue);

            return this;
        }
    }
}