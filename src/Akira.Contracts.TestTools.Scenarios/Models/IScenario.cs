namespace Akira.Contracts.TestTools.Scenarios.Models
{
    public interface IScenario
    {
        IContext Context { get; }

        string Name { get; }

        string Key { get; }
    }
}