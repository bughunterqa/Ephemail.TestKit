namespace Ephemail.TestKit.Exeptions
{
    public static class EmailErrors
    {
        public static string MissingEmailAddress =>
            "Email address must be set. Call UseEmail(...) first.";

        public static string SubjectNotFound(string expectedSubject, IEnumerable<string> availableSubjects) =>
            availableSubjects.Any()
                ? $"❌ Expected subject not found: \"{expectedSubject}\"\nAvailable subjects:\n- {string.Join("\n- ", availableSubjects)}"
                : "❌ No emails found at all.";

        public static string TemplateMismatch(string missingLine, string bodyText) => $"""
        ❌ Email body does not match the expected template.
        👉 Expected line not found: "{missingLine}"

        🔍 Actual Email Body:
        ------------------------------
        {bodyText.Trim()}
        ------------------------------
        """;

        public static string EmailNotFetched =>
            "Email has not been fetched. Call Expect(...) first.";

        public static string LinkNotFound(string anchorText) =>
            $"Link with anchor text \"{anchorText}\" not found.";
    }
}
