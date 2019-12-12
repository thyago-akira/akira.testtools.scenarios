# Akira.TestTools.Scenarios
[![Downloads](https://img.shields.io/nuget/dt/akira.testtools.scenarios.svg)](https://www.nuget.org/packages/Akira.TestTools.Scenarios/)
[![Version](https://img.shields.io/nuget/v/akira.testtools.scenarios)](https://www.nuget.org/packages/Akira.TestTools.Scenarios/)
[![Build status](https://ci.appveyor.com/api/projects/status/l9921h92qihnfytf/branch/master?svg=true)](https://ci.appveyor.com/project/thyago-akira/akira-testtools-scenarios/branch/master)
[![Tests](https://img.shields.io/appveyor/tests/thyago-akira/akira-testtools-scenarios/master)](https://ci.appveyor.com/project/thyago-akira/akira-testtools-scenarios/branch/master)

## Install:
To install **Akira.TestTools.Scenarios**, run the following command in the [Package Manager Console](https://docs.nuget.org/docs/start-here/using-the-package-manager-console)

    PM> Install-Package Akira.TestTools.Scenarios

## Medium Articles:
### Scenarios Combinations
- [English Version](https://medium.com/@thyakira/what-is-your-testing-universe-1dce22d82eb)
- [Versión Español](https://medium.com/@thyakira/cual-es-tu-universo-de-pruebas-468a13973a8a)
- [Versão Português](https://medium.com/@thyakira/qual-e-seu-universo-de-testes-63cf6b453121)

### Valid/Invalid Scenarios
In progress

## IScenariosBuilder<T>
### CountPossibleScenariosCombinations
Gets the number of possible scenarios for the current scenarios builder

**Example:**
```csharp
var allPossibleScenarios = scenariosFaker.CountPossibleScenariosCombinations;
```
### ScenarioContext
Add a new Scenario Context to the IScenariosBuilder<T>

**Example:**
```csharp
...
.ScenarioContext("CreditLimit")
...
```
### DefaultContextValidScenario
Add a new Valid Scenario to the current Default Scenario Context

**Example:**
```csharp
...
.DefaultContextValidScenario(scenarioRuleset => scenarioRuleset
    .Ignore(x => x.Id))
...
```
### DefaultContextInvalidScenario
Add a new Invalid Scenario to the current Default Scenario Context

**Example:**
```csharp
...
.DefaultContextInvalidScenario(scenarioRuleset => scenarioRuleset
    .Ignore(x => x.Id))
...
```
### Scenario
Add a new Scenario to the current Scenario Context

**Example:**
```csharp
...
.Scenario(
    "LowCreditLimit",
    scenarioRuleset => scenarioRuleset
        .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(1, 300)))
...
```
### KnownScenarioCombination
Add a Known Valid Scenario Combination to the IScenariosBuilder<T>

**Example:**
```csharp
...
.KnownScenarioCombination(
    new Dictionary<string, string>
    {
        { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
        { "CreditLimit", "HighCreditLimit" }
    },
    ScenarioCombinationType.AlwaysValid)
...
```
### Generate
Generate a new instance of model

**Example:**
```csharp
var randomModel = scenariosFaker.Generate();
```
### Generate
Generate multiples instances of model

**Example:**
```csharp
var randomList = scenariosFaker.Generate(15);
```
### GenerateMinimumTestingScenarios
Generate multiples instances of model based on kind of model you need to create

**Example:**
```csharp
var minimumListToTest = scenariosFaker.GenerateMinimumTestingScenarios();
```
### Full Example:
```csharp
var scenariosFaker = new ScenariosFaker<Customer>()
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
        .Ignore(x => x.Email),
        ScenarioCombinationType.AlwaysInvalid)
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
            .RuleFor(x => x.CreditLimit, (f) => f.Random.Int(int.MinValue, -1)))
    .KnownScenarioCombination(
        new Dictionary<string, string>
        {
            { Defaults.ScenarioContextName, Defaults.ScenarioValidName },
            { "CreditLimit", "HighCreditLimit" }
        },
        ScenarioCombinationType.AlwaysValid)
    .KnownScenarioCombination(
        new Dictionary<string, string>
        {
            { Defaults.ScenarioContextName, Defaults.ScenarioInvalidName },
            { "CreditLimit", "InvalidCredit" }
        });

var allPossibleScenarios = scenariosFaker.CountPossibleScenariosCombinations;

var randomModel = scenariosFaker.Generate();

var randomList = scenariosFaker.Generate(15);

var minimumListToTest = scenariosFaker.GenerateMinimumTestingScenarios();
```