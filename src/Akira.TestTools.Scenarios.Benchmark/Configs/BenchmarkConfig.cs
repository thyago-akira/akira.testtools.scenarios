using System;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Benchmark.Stubs;
using BenchmarkDotNet.Attributes;

namespace Akira.TestTools.Scenarios.Benchmark.Configs
{
    public class BenchmarkConfig
    {
        private readonly Func<Customer> instanceBuilder = () => new Customer
        {
            Id = 1,
            FirstName = "John",
            MiddleName = "Tovard",
            LastName = "Austin",
            DateOfBirth = new DateTime(1980, 06, 14),
            Gender = Gender.Male,
            Email = "test@test.com",
            CreditLimit = 0,
        };

        private readonly IScenariosBuilder<Customer> simpleFaker = new ScenariosFaker<Customer>()
            .DefaultContextValidScenario(scenarioRuleset => scenarioRuleset
                .RuleFor(x => x.Id, (f) => f.Random.Int(1))
                .RuleFor(x => x.FirstName, (f) => f.Name.FirstName())
                .RuleFor(x => x.MiddleName, (f) => f.Name.FirstName())
                .RuleFor(x => x.LastName, (f) => f.Name.LastName())
                .RuleFor(x => x.DateOfBirth, (f) => f.Date.Past(18, new DateTime(1900, 1, 1)))
                .RuleFor(x => x.Gender, (f) => f.Random.Enum<Gender>())
                .RuleFor(x => x.Email, (f) => f.Internet.Email())
                .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(0, 200000)))
            .DefaultContextInvalidScenario(scenarioRuleset => scenarioRuleset
                .Ignore(x => x.Id)
                .Ignore(x => x.FirstName)
                .Ignore(x => x.MiddleName)
                .Ignore(x => x.LastName)
                .Ignore(x => x.DateOfBirth)
                .Ignore(x => x.Gender)
                .Ignore(x => x.Email)
                .Ignore(x => x.CreditLimit));

        private readonly IScenariosBuilder<Customer> customScenarioFaker = new ScenariosFaker<Customer>()
            .DefaultContextValidScenario(scenarioRuleset => scenarioRuleset
                .RuleFor(x => x.Id, (f) => f.Random.Int(1))
                .RuleFor(x => x.FirstName, (f) => f.Name.FirstName())
                .RuleFor(x => x.MiddleName, (f) => f.Name.FirstName())
                .RuleFor(x => x.LastName, (f) => f.Name.LastName())
                .RuleFor(x => x.DateOfBirth, (f) => f.Date.Past(18, new DateTime(1900, 1, 1)))
                .RuleFor(x => x.Gender, (f) => f.Random.Enum<Gender>())
                .RuleFor(x => x.Email, (f) => f.Internet.Email()))
            .DefaultContextInvalidScenario(scenarioRuleset => scenarioRuleset
                .Ignore(x => x.Id)
                .Ignore(x => x.FirstName)
                .Ignore(x => x.MiddleName)
                .Ignore(x => x.LastName)
                .Ignore(x => x.DateOfBirth)
                .Ignore(x => x.Gender)
                .Ignore(x => x.Email))
            .ScenarioContext("CreditLimit")
            .Scenario(
                "LowCreditLimit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(1, 300)))
            .Scenario(
                "MediumCreditLimit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(301, 5000)))
            .Scenario(
                "HighCreditLimit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(5001, 300000)))
            .Scenario(
                "WithoutCredit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, 0))
            .Scenario(
                "InvalidCredit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(int.MinValue, -1)));

        private readonly IScenariosBuilder<Customer> complexScenarioFaker = new ScenariosFaker<Customer>()
            .DefaultContextValidScenario(scenarioRuleset => scenarioRuleset
                .RuleFor(x => x.Id, (f) => f.Random.Int(1))
                .RuleFor(x => x.Email, (f) => f.Internet.Email()))
            .DefaultContextInvalidScenario(scenarioRuleset => scenarioRuleset
                .Ignore(x => x.Id)
                .Ignore(x => x.Email))
            .ScenarioContext("FirstName")
            .Scenario(
                "ValidName",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.FirstName, (f) => f.Name.FirstName()))
            .Scenario(
                "InvalidName",
                scenarioRuleset => scenarioRuleset
                    .Ignore(x => x.FirstName))
            .ScenarioContext("MiddleName")
            .Scenario(
                "ValidName",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.MiddleName, (f) => f.Name.FirstName()))
            .Scenario(
                "InvalidName",
                scenarioRuleset => scenarioRuleset
                    .Ignore(x => x.MiddleName))
            .ScenarioContext("LastName")
            .Scenario(
                "ValidName",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.LastName, (f) => f.Name.LastName()))
            .Scenario(
                "InvalidName",
                scenarioRuleset => scenarioRuleset
                    .Ignore(x => x.LastName))
            .ScenarioContext("DateOfBirth")
            .Scenario(
                "ValidDoB",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.DateOfBirth, (f) => f.Date.Past(18, new DateTime(1900, 1, 1))))
            .Scenario(
                "InvalidDoB",
                scenarioRuleset => scenarioRuleset
                    .Ignore(x => x.DateOfBirth))
            .ScenarioContext("Gender")
            .Scenario(
                "Male",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.Gender, () => Gender.Male))
            .Scenario(
                "Female",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.Gender, () => Gender.Female))
            .Scenario(
                "InvalidGender",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.Gender, () => (Gender)999))
            .ScenarioContext("Email")
            .Scenario(
                "ValidEmail",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.Email, (f) => f.Internet.Email()))
            .Scenario(
                "InvalidEmail",
                scenarioRuleset => scenarioRuleset
                    .Ignore(x => x.Email))
            .ScenarioContext("CreditLimit")
            .Scenario(
                "LowCreditLimit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(1, 300)))
            .Scenario(
                "MediumCreditLimit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(301, 5000)))
            .Scenario(
                "HighCreditLimit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(5001, 300000)))
            .Scenario(
                "WithoutCredit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, 0))
            .Scenario(
                "InvalidCredit",
                scenarioRuleset => scenarioRuleset
                    .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(int.MinValue, -1)));

        [Params(1, 100, 1000)]
        public int Input { get; set; }

        [Benchmark]
        public void InstanceBuilderForLoop() => this.CreateInstancesInForLoop(this.Input, this.instanceBuilder);

        [Benchmark]
        public void SimpleFakerForLoop() => this.CreateInstancesInForLoop(this.Input, () => this.simpleFaker.Generate());

        [Benchmark]
        public void CustomFakerForLoop() => this.CreateInstancesInForLoop(this.Input, () => this.customScenarioFaker.Generate());

        [Benchmark]
        public void ComplexFakerForLoop() => this.CreateInstancesInForLoop(this.Input, () => this.complexScenarioFaker.Generate());

        private void CreateInstancesInForLoop(int numberOfNewInstances, Func<Customer> getCustomer)
        {
            for (var i = 0; i < numberOfNewInstances; i++)
            {
                _ = getCustomer();
            }
        }
    }
}