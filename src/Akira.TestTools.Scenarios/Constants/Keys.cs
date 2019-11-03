namespace Akira.TestTools.Scenarios.Constants
{
    public static class Keys
    {
        public const string ScenarioContextKeyValueFormat = "|{0}:{1}|";

        public static string GetScenarioContextKeyValue(
            int scenarioContextIndex,
            string scenarioName)
        {
            return string.Format(
                ScenarioContextKeyValueFormat,
                scenarioContextIndex,
                scenarioName);
        }
    }
}