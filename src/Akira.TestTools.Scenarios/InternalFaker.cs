using System;
using System.Linq.Expressions;
using Akira.Contracts.TestTools.Scenarios;
using Akira.Contracts.TestTools.Scenarios.Collections;
using Bogus;

namespace Akira.TestTools.Scenarios
{
    internal class InternalFaker<T> : IScenarioRuleSet<T>, IModelBuilder<T>
        where T : class
    {
        #region Fields

        private readonly Faker<T> faker = new Faker<T>().StrictMode(true);

        #endregion Fields

        #region Properties

        public string Key { get; private set; }

        #endregion Properties

        #region Methods

        public void IgnoreProperty<TProperty>(
            Expression<Func<T, TProperty>> property) =>
            _ = this.faker.Ignore(property);

        public void RuleForProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<T, TProperty> getValue) =>
            _ = this.faker.RuleFor(
                property,
                (f, t) => getValue(t));

        public void RuleForProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            Func<TProperty> getValue) =>
            _ = this.faker.RuleFor(
                property,
                getValue);

        public void RuleForProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            TProperty value) =>
            _ = this.faker.RuleFor(
                property,
                value);

        public void ExecuteAction(
            string scenarioRuleSetActionKey,
            Action<IScenarioRuleSet<T>> action)
        {
            this.Key += scenarioRuleSetActionKey;
            action(this);
        }

        public T Generate() => this.faker.Generate();

        #endregion Methods
    }
}