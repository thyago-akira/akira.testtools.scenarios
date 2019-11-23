using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Akira.TestTools.Scenarios.Tests.Stubs;

namespace Akira.TestTools.Scenarios.Tests.Context
{
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "All underscores are discards.")]
    public class GenerateMultipleTestContext : BaseTestContext
    {
        public Func<IEnumerable<SimpleModel>> TestedAction { get; set; }

        public override Action GenerateExceptionAction => () => _ = this.TestedAction();

        public int NumberOfRows { get; set; }
    }
}