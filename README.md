# EphemailClient

**Лёгкий в использовании .NET-клиент для работы с временной почтой через [Ephemail API](https://ephemail.autos).**

Получай письма, извлекай нужные данные, переходи по ссылкам — всё прямо из автотестов.

[![NuGet](https://img.shields.io/nuget/v/EphemailClient.svg)](https://www.nuget.org/packages/EphemailClient)
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

---

## 📦 Установка

```bash
dotnet add package EphemailClient
```

---

## 🚀 Быстрый старт

```csharp
Email.Epheapi
    .UseEmail("qauser-1234@ephemail.autos")
    .Expect(Templates.WelcomeAsBroker(broker, admin))
    .Extract(out var password, EmailRegex.Password)
    .GotoLink("Get Started")
    .Delete();
```

---

## 📬 Возможности

- ⏳ Ожидание письма по теме (subject) с таймаутом
- ✅ Проверка содержимого письма по шаблону
- 🔗 Извлечение ссылок по anchor-тексту
- 🔐 Извлечение текста с помощью регулярных выражений
- 🗑 Удаление отдельных писем или всех писем по адресу
- 🧱 Расширяемая структура, не привязана к Selenium

---

## 🧪 Использование в автотестах

EphemailClient интегрируется в любые .NET-проекты и широко используется в C# автотестах (UI, API, end-to-end), где необходимо обрабатывать временные email-уведомления.

---

## ✉️ Пример шаблона письма

```csharp
public static EmailTemplate WelcomeAsBroker(Broker broker, Admin admin)
{
    return new EmailTemplate(
        $"Welcome to Ephemail!",
        $"Dear {broker.FirstName}",
        "We are happy to have you on board!",
        $"Your account was created by {admin.Email}."
    );
}
```

---

## 📚 Документация

Полный список возможностей и примеров использования:  
👉 [https://ephemail.autos/docs/email-client](https://ephemail.autos/docs/email-client)

API доступно по адресу:  
🌐 `https://api.ephemail.autos`

---

## 🛠 Требования

- .NET 8.0 или выше
- Активное подключение к Ephemail API

---

## 🤝 Вклад

Pull requests приветствуются.  
Нашли баг, есть идеи — создайте issue или fork.

---

## ⚖️ Лицензия

Этот проект лицензирован под MIT License — см. [LICENSE](LICENSE).
