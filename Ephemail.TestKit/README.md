# EphemailClient

📨 A lightweight client for interacting with [Ephemail](https://ephemail.autos) — a temporary email API built for test automation.

---

## ✅ Features

- Fetch plain-text email content
- Match emails against templates
- Extract passwords, codes, links
- Use in fluent test chains:
  ```csharp
  Email.Epheapi
      .UseEmail(user.Email)
      .Expect(MessageData.WelcomeTemplate(user))
      .Extract(out string password, EmailRegex.Password)
      .GotoLink("Get Started")
      .Delete();
