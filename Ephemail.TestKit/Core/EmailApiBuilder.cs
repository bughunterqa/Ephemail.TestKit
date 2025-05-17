using Ephemail.TestKit.Exeptions;
using Ephemail.TestKit.Models;
using Ephemail.TestKit.Templates;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ephemail.TestKit.Core
{
    public class EmailApiBuilder
    {
        private string _emailAddress = string.Empty;
        private PlainEmailDto? _fetchedEmail;
        private readonly EphemailClient _client;

        public EmailApiBuilder()
        {
            _client = new EphemailClient();
        }

        public EmailApiBuilder UseEmail(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public EmailApiBuilder Expect(EmailTemplate template, int timeoutSeconds = 30)
        {
            if (string.IsNullOrWhiteSpace(_emailAddress))
                throw EphemailException.MissingEmailAddress();

            _fetchedEmail = _client.GetEmailBySubjectAsync(_emailAddress, template.Subject, timeoutSeconds)
                                   .GetAwaiter().GetResult();

            if (_fetchedEmail == null)
            {
                var subjects = _client.GetAllSubjectsAsync(_emailAddress).GetAwaiter().GetResult();
                throw EphemailException.SubjectNotFound(template.Subject, subjects);
            }

            string bodyText = _fetchedEmail.BodyText ?? string.Empty;

            if (!template.Matches(bodyText, out string? mismatchLine))
                throw EphemailException.TemplateMismatch(mismatchLine ?? "Unknown line", bodyText);

            return this;
        }


        // Будущие методы:
        public EmailApiBuilder Extract(out string result, string regexPattern)
        {
            if (_fetchedEmail == null)
                throw new InvalidOperationException("Email has not been fetched. Call Expect(...) first.");

            var text = _fetchedEmail.BodyText ?? string.Empty;

            var match = Regex.Match(text, regexPattern, RegexOptions.IgnoreCase);
            if (!match.Success || match.Groups.Count < 2)
                throw new Exception($"Failed to extract value using pattern: {regexPattern}");

            result = match.Groups[1].Value;
            return this;
        }


        public string GetLink(string anchorText)
        {
            if (_fetchedEmail == null)
                throw new InvalidOperationException("Email has not been fetched. Call Expect(...) first.");

            var link = _fetchedEmail.Links
                .FirstOrDefault(l => string.Equals(l.Text?.Trim(), anchorText.Trim(), StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(link?.Url))
                throw new Exception($"Link with anchor text \"{anchorText}\" not found.");

            return link.Url;
        }

        public EmailApiBuilder GotoLink(string anchorText, Action<string> navigator)
        {
            var url = GetLink(anchorText);

            navigator(url);

            return this;
        }

        public EmailApiBuilder Delete()
        {
            if (_fetchedEmail == null)
                throw new InvalidOperationException("Email has not been fetched. Call Expect(...) first.");

            bool deleted = _client
                .DeleteEmailByIdAsync(_fetchedEmail.Id)
                .GetAwaiter().GetResult();

            if (!deleted)
                throw new Exception("Failed to delete email.");

            return this;
        }


        public EmailApiBuilder DeleteAll()
        {
            if (string.IsNullOrWhiteSpace(_emailAddress))
                throw new InvalidOperationException("Email address must be set. Call UseEmail(...) first.");

            bool deleted = _client
                .DeleteAllEmailsAsync(_emailAddress)
                .GetAwaiter().GetResult();

            if (!deleted)
                throw new Exception("Failed to delete all emails for the given address.");

            return this;
        }

    }
}
