namespace Ephemail.TestKit.Utils
{
    public static class EmailRegex
    {
        /// <summary>
        /// Matches "Password: xyz123"
        /// Group 1 = xyz123
        /// </summary>
        public const string Password = @"Password:\s*(\S+)";

        /// <summary>
        /// Matches "Your code is: 123456"
        /// Group 1 = 123456
        /// </summary>
        public const string VerificationCode = @"code is[:\s]*([0-9]{4,8})";


        /// <summary>
        /// Matches "Temporary Login Token: abcd-1234"
        /// Group 1 = abcd-1234
        /// </summary>
        public const string LoginToken = @"Temporary Login Token[:\s]*([\w-]+)";
    }
}
