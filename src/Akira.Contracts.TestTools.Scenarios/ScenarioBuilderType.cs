namespace Akira.Contracts.TestTools.Scenarios
{
    public enum ScenarioBuilderType
    {
        /// <summary>
        /// Valid Only Scenario: The builder will create models that contain <see cref="ScenarioCombinationType.AlwaysValid"/> scenarios
        /// </summary>
        ValidOnly = 2,

        /// <summary>
        /// Invalid Only Scenario: The builder will create models that contain <see cref="ScenarioCombinationType.AlwaysInvalid"/> scenarios
        /// </summary>
        InvalidOnly = 4,

        /// <summary>
        /// All universe of scenarios
        /// </summary>
        All = 7
    }
}