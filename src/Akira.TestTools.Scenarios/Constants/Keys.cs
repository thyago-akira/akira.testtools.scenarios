using System.Text.RegularExpressions;

namespace Akira.TestTools.Scenarios.Constants
{
    public static class Keys
    {
        public const char KeySeparator = '|';

        public const char ValueSeparator = '@';

        public const string OptionalGroup = "?";

        public const string ExactGroup = "{1}";

        public const string AnyWord = @".+";

        /// <summary>
        /// Returns the scenario context key value. It will look like this format: |{0}:{1}|
        /// </summary>
        public static string GetScenarioContextKeyValue(
            int scenarioContextIndex,
            string scenarioName) =>
            $"{KeySeparator}{scenarioContextIndex}{ValueSeparator}{scenarioName}{KeySeparator}";

        public static Regex GetKeyRegex(
            string valuesRegex)
        {
            return new Regex(
                $"^({valuesRegex})$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// The regular expression template looks like this format: (\|({0}\:{1})\|){2}
        /// </summary>
        public static string GetScenarioContextKeyValueRegexValue(
            int scenarioContextIndex,
            string scenarioName = null,
            bool useOptionalGroup = true)
        {
            var groupValue = useOptionalGroup ? OptionalGroup : ExactGroup;

            if (string.IsNullOrWhiteSpace(scenarioName))
            {
                scenarioName = AnyWord;
                groupValue = OptionalGroup;
            }

            return $@"(\{KeySeparator}({scenarioContextIndex}\{ValueSeparator}{scenarioName})\{KeySeparator}){groupValue}";
        }
    }
}