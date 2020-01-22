namespace Akira.Contracts.TestTools.Scenarios.Enums
{
    public enum ScenarioCombinationType
    {
        /// <summary>
        /// Default <see cref="ScenarioCombinationType"/>, indicates a Scenario that depends on the other scenarios configuration.
        /// It could be Valid or Invalid
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// This <see cref="ScenarioCombinationType"/> indicates that your generated model will be always Valid
        /// </summary>
        AlwaysValid = 1,

        /// <summary>
        /// This <see cref="ScenarioCombinationType"/> indicates that your generated model will be always Invalid,
        /// </summary>
        AlwaysInvalid = 2
    }
}