namespace Ephemail.TestKit.Exeptions
{
    public class EphemailException : Exception
    {
        public EphemailException(string message) : base(message) { }

        public static EphemailException MissingEmailAddress() =>
            new(EmailErrors.MissingEmailAddress);

        public static EphemailException SubjectNotFound(string subject, IEnumerable<string> availableSubjects) =>
            new(EmailErrors.SubjectNotFound(subject, availableSubjects));

        public static EphemailException TemplateMismatch(string line, string body) =>
            new(EmailErrors.TemplateMismatch(line, body));


        public static EphemailException EmailNotFetched() =>
            new(EmailErrors.EmailNotFetched);

        public static EphemailException LinkNotFound(string anchorText) =>
            new(EmailErrors.LinkNotFound(anchorText));
    }
}
