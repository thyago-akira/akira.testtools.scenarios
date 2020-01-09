namespace Akira.Contracts.TestTools.Scenarios
{
    public interface ICompletedModelBuilder<T>
        where T : class
    {
        /// TODO:
        /// ScenarioCombinationType ModelBuilderType { get; }

        /// TODO:
        /// IDictionary<string, string> ModelBuilderScenariosConfiguration { get; }

        T Generate();
    }
}