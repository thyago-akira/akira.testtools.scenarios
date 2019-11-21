using System.Collections.Generic;

namespace Akira.TestTools.Scenarios.Tests.Extensions
{
    internal static class IEnumerableExtensions
    {
        internal static IEnumerable<object[]> GetTestDynamicData<T>(this IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                yield return new object[] { item };
            }
        }
    }
}