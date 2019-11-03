namespace Akira.TestTools.Scenarios.Tests.Context
{
    public abstract class BaseTestContext
    {
        public int CaseNumber { get; set; }

        public string CaseDescription { get; set; }

        public override string ToString() => $"\r\nCase #{this.CaseNumber} - {this.CaseDescription}";
    }
}