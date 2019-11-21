using System;

namespace Akira.TestTools.Scenarios.Tests.Context
{
    public abstract class BaseTestContext
    {
        public int CaseNumber { get; set; }

        public string CaseDescription { get; set; }

        public string ExpectedExceptionMessage { get; set; }

        public abstract Action GenerateExceptionAction { get; }

        public override string ToString() => $"\r\nCase #{this.CaseNumber} - {this.CaseDescription}";
    }
}