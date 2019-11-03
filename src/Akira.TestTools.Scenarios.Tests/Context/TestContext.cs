using System;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context
{
    public class TestContext : BaseTestContext
    {
        public Func<IScenariosBuilder<SimpleModel>> TestedAction { get; set; }

        public string ExpectedExceptionMessage { get; set; }
    }
}