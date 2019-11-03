namespace Akira.Contracts.TestTools.Scenarios
{
    public enum ScenarioCombinationType
    {
        /// <summary>
        /// Default <see cref="ScenarioCombinationType"/>, indicates a Scenario that could Valid or Invalid
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