using System.Text.RegularExpressions;

namespace Akira.TestTools.Scenarios.Constants
{
    public static class Keys
    {
        public const char KeySeparator = '|';

        public const char ValueSeparator = '@';

        public const string RegexBaseTemplate = @"^({0})$";

        public const string OptionalGroup = "?";

        public const string ExactGroup = "{1}";

        public const string AnyWord = @".+";

        /// <summary>
        /// The scenario context key looks like this format: |{0}:{1}|
        /// </summary>
        public static readonly string ScenarioContextKeyValueFormat =
            string.Concat(
                KeySeparator,
                "{0}",
                ValueSeparator,
                "{1}",
                KeySeparator);

        /// <summary>
        /// The regular expression template looks like this format: (\|({0}\:{1})\|){2}
        /// </summary>
        public static readonly string RegexGroupTemplate =
            string.Concat(
                @"(\",
                KeySeparator,
                @"({0}\",
                ValueSeparator,
                @"{1})\",
                KeySeparator,
                "){2}");

        public static string GetScenarioContextKeyValue(
            int scenarioContextIndex,
            string scenarioName)
        {
            return string.Format(
                ScenarioContextKeyValueFormat,
                scenarioContextIndex,
                scenarioName);
        }

        public static Regex GetRegex(
            string valuesRegex)
        {
            return new Regex(
                string.Format(
                    RegexBaseTemplate,
                    valuesRegex),
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static (string, string) GetScenarioContextKeyValueRegexValue(
            int scenarioContextIndex,
            string scenarioName)
        {
            return (string.Format(
                    RegexGroupTemplate,
                    scenarioContextIndex,
                    scenarioName,
                    OptionalGroup),
                string.Format(
                    RegexGroupTemplate,
                    scenarioContextIndex,
                    scenarioName,
                    ExactGroup));
        }

        public static string GetMissingScenarioContextKeyValueRegexValue(
            int scenarioContextIndex)
        {
            return string.Format(
                RegexGroupTemplate,
                scenarioContextIndex,
                AnyWord,
                OptionalGroup);
        }
    }
}