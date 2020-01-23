namespace Akira.Contracts.TestTools.Scenarios.Enums
{
    public enum BuilderType
    {
        /// <summary>
        /// Valid Only Scenario: The builder will create models that contain <see cref="ScenarioType.AlwaysValid"/> scenarios
        /// </summary>
        ValidOnly = 2,

        /// <summary>
        /// Invalid Only Scenario: The builder will create models that contain <see cref="ScenarioType.AlwaysInvalid"/> scenarios
        /// </summary>
        InvalidOnly = 4,

        /// <summary>
        /// All universe of scenarios
        /// </summary>
        All = 7
    }
}