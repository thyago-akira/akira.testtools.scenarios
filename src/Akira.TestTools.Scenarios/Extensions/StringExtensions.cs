namespace Akira.TestTools.Scenarios.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeName(this string name)
        {
            return name.Trim().ToLower();
        }
    }
}