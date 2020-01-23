namespace Akira.Contracts.TestTools.Scenarios.Enums
{
    public enum ScenarioType
    {
        /// <summary>
        /// Default <see cref="ScenarioType"/>, indicates a Scenario that depends on the other scenarios.
        /// It could be Valid or Invalid
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// This <see cref="ScenarioType"/> indicates that your generated model will be always Valid.
        /// </summary>
        AlwaysValid = 1,

        /// <summary>
        /// This <see cref="ScenarioType"/> indicates that your generated model will be always Invalid.
        /// </summary>
        AlwaysInvalid = 2
    }
}