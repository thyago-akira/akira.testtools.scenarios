using System;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios;
using Bogus;

namespace Akira.TestTools.Scenarios
{
    internal class InternalFaker<T> : IScenarioRuleSet<T>, ICompletedModelBuilder<T>
        where T : class
    {
        #region Fields

        private readonly Faker<T> faker = new Faker<T>().StrictMode(true);

        #endregion Fields

        #region Constructor

        internal InternalFaker(string key)
        {
            this.Key = key;
        }

        #endregion Constructor

        #region Properties

        public string Key { get; private set; }

        #endregion Properties

        #region Methods

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

        public T Generate() => this.faker.Generate();

        #endregion Methods
    }
}