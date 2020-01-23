using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    public abstract class BaseScenariosBuilderTests
    {
        protected void AssertThrowsException<T>(
            Context.BaseTestContext testContext)
            where T : Exception
        {
            // Action && Assert
            var exception = Assert.ThrowsException<T>(() =>
                testContext.GenerateExceptionAction());

            if (!string.IsNullOrWhiteSpace(testContext.ExpectedExceptionMessage))
            {
                Assert.AreEqual(
                    testContext.ExpectedExceptionMessage,
                    exception.Message);
            }
        }
    }
}