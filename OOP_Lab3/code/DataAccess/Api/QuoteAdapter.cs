using OOP_LAB3.Application.DTO;
using OOP_LAB3.Domain.Factories;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace OOP_LAB3.DataAccess.Api
{
    public class QuoteAdapter
    {
        private readonly HttpClient _httpClient;

        public QuoteAdapter()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true
            };

            _httpClient = new HttpClient(handler);
        }

        public async Task<QuoteDTO> GetRandomQuoteAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://zenquotes.io/api/random");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var data = JsonSerializer.Deserialize<QuoteApiResponse[]>(json);
                //var firstQuote = quotes[0];

                return QuoteFactory.Create(data[0].Quote, data[0].Author);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return new QuoteDTO("No quote available", "System");
            }
        }

        private class QuoteApiResponse
        {
            [JsonPropertyName("q")]  // Для System.Text.Json
            public string Quote { get; set; }

            [JsonPropertyName("a")]
            public string Author { get; set; }

            [JsonPropertyName("h")]
            public string Html { get; set; }
        }
    }
}
