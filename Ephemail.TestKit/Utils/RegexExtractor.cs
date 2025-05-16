using System;
using System.Text.RegularExpressions;

namespace Ephemail.TestKit.Utils
{
    public static class RegexExtractor
    {
        public static string Extract(string input, string pattern, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input is empty.", nameof(input));

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            if (!match.Success || match.Groups.Count < 2)
                throw new Exception(errorMessage);

            return match.Groups[1].Value;
        }

        public static bool TryExtract(string input, string pattern, out string result)
        {
            result = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            if (!match.Success || match.Groups.Count < 2)
                return false;

            result = match.Groups[1].Value;
            return true;
        }
    }
}
