﻿using System;
using System.Diagnostics.CodeAnalysis;
using Akira.Contracts.TestTools.Scenarios;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context
{
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "All underscores are discards.")]
    public class TestContext : BaseTestContext
    {
        public Func<IScenariosBuilder<SimpleModel>> TestedAction { get; set; }

        public override Action GenerateExceptionAction => () => _ = this.TestedAction();
    }
}