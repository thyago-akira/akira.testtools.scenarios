using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Akira.TestTools.Scenarios.Tests
{
    public abstract class BaseScenariosFakerTests
    {
        protected void AssertThrowsException<T>(
            Context.TestContext addScenarioContext,
            Action<T> additionalExceptionAsserts = null)
            where T : Exception
        {
            // Action && Assert
            var exception = Assert.ThrowsException<T>(() =>
                _ = addScenarioContext.TestedAction());

            Assert.AreEqual(
                addScenarioContext.ExpectedExceptionMessage,
                exception.Message);

            additionalExceptionAsserts?.Invoke(exception);
        }
    }
}