namespace Akira.Contracts.TestTools.Scenarios
{
    public enum ScenarioBuilderType
    {
        /// <summary>
        /// Valid Only Scenario: Those scenarios with only valid data
        /// </summary>
        ValidOnly = 2,

        /// <summary>
        /// Invalid Only Scenario: Those scenarios with only invalid data
        /// </summary>
        InvalidOnly = 4,

        /// <summary>
        /// All universe of scenarios
        /// </summary>
        All = 7
    }
}