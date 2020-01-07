using System.Collections.Generic;

namespace Akira.Contracts.TestTools.Scenarios
{
    public interface ICompletedModelBuilder<T>
        where T : class
    {
        ScenarioCombinationType ModelBuilderType { get; }

        IDictionary<string, string> ModelBuilderScenariosConfiguration { get; }

        T Generate();
    }
}