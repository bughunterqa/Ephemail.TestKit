using System;

namespace Ephemail.TestKit.Templates
{
    public class EmailTemplate
    {
        public string Subject { get; }
        public string[] ExpectedLines { get; }

        public EmailTemplate(string subject, params string[] lines)
        {
            Subject = subject;
            ExpectedLines = lines;
        }

        public bool Matches(string actualBody, out string? mismatch)
        {
            foreach (var expected in ExpectedLines)
            {
                if (!actualBody.Contains(expected, StringComparison.OrdinalIgnoreCase))
                {
                    mismatch = $"Expected line not found: \"{expected}\"";
                    return false;
                }
            }

            mismatch = null;
            return true;
        }
    }
}
