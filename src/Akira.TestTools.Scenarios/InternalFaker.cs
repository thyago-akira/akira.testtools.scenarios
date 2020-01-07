using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios;
using Bogus;

namespace Akira.TestTools.Scenarios
{
    public class InternalFaker<T> : IScenarioRuleSet<T>, ICompletedModelBuilder<T>
        where T : class
    {
        private readonly Faker<T> faker = new Faker<T>().StrictMode(true);

        public ScenarioCombinationType ModelBuilderType { get; }

        public IDictionary<string, string> ModelBuilderScenariosConfiguration { get; }

        public IScenarioRuleSet<T> Ignore<TProperty>(
            Expression<Func<T, TProperty>> propertyOrField)
        {
            _ = this.faker.Ignore(propertyOrField);

            return this;
        }

        public IScenarioRuleSet<T> RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<T, TProperty> getValue)
        {
            _ = this.faker.RuleFor(
                property,
                (f, t) => getValue(t));

            return this;
        }

        public IScenarioRuleSet<T> RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<TProperty> getValue)
        {
            _ = this.faker.RuleFor(
                property,
                getValue);

            return this;
        }

        public IScenarioRuleSet<T> RuleFor<TProperty>(
            Expression<Func<T, TProperty>> property,
            TProperty value)
        {
            _ = this.faker.RuleFor(
                property,
                value);

            return this;
        }

        public T Generate()
        {
            return this.faker.Generate();
        }
    }
}