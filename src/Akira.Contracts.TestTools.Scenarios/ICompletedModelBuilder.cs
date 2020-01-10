namespace Akira.Contracts.TestTools.Scenarios
{
    public interface ICompletedModelBuilder<T>
        where T : class
    {
        string Key { get; }

        T Generate();
    }
}