using Ephemail.TestKit.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Ephemail.TestKit.Core
{
    public class EphemailClient
    {
        private readonly HttpClient _http;

        public EphemailClient()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://api.ephemail.autos/")
            };
        }

        public async Task<PlainEmailDto?> GetEmailBySubjectAsync(string address, string subject, int timeoutSeconds = 20)
        {
            var encodedAddress = Uri.EscapeDataString(address);
            var encodedSubject = Uri.EscapeDataString(subject);

            var url = $"emails/details/by-subject?address={encodedAddress}&subject={encodedSubject}";

            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.Elapsed.TotalSeconds < timeoutSeconds)
            {
                var response = await _http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<PlainEmailDto>();

                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                    break;

                await Task.Delay(1000);
            }

            return null;
        }

        public async Task<List<string>> GetAllSubjectsAsync(string address)
        {
            var encoded = Uri.EscapeDataString(address);
            var url = $"emails/compact?address={encoded}";

            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<List<CompactEmailDto>>();
            return result?.Select(e => e.Subject ?? string.Empty).ToList() ?? new();
        }

        public async Task<bool> DeleteEmailByIdAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"emails/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteAllEmailsAsync(string address)
        {
            var encoded = Uri.EscapeDataString(address);
            var response = await _http.DeleteAsync($"emails?address={encoded}");
            return response.IsSuccessStatusCode;
        }
    }
}
