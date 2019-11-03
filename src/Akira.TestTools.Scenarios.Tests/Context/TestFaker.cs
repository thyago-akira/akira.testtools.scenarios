using System;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context
{
    public class TestFakerContext
    {
        public string FakerDescription { get; set; }

        public Func<IScenariosBuilder<SimpleModel>> GetFaker { get; set; }

        public string OverrideMessage { get; set; } = null;
    }
}