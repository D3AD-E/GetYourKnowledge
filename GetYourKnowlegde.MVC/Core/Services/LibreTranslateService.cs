using GetYourKnowledge.MVC.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace GetYourKnowledge.MVC.Core.Services
{
    /// <summary>
    /// LibreTranslateService is used for translating advices from English to Polish
    /// </summary>
    public class LibreTranslateService
    {
        private readonly HttpClient _client;

        private class LibreTranslateServiceRequest
        {
            public string Q { get; set; }
            public string Source { get; set; }
            public string Target { get; set; }

            public string Format { get; private set; }
            public LibreTranslateServiceRequest()
            {
                Format = "text";
            }
        }

        private class LibreTranslateServiceResponse
        {
            public string Error { get; set; }

            public string TranslatedText { get; set; }
        }


        public LibreTranslateService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://libretranslate.de/");
            _client = client;
        }

        public async Task<string> TranslateAsync(string text, LanguageType from, LanguageType to)
        {
            var request = new LibreTranslateServiceRequest
            {
                Q=text,
                Source = from.Value,
                Target = to.Value
            };
            var httpResponse = await _client.PostAsJsonAsync("/translate", request);

            var libreResponse = await httpResponse.Content.ReadFromJsonAsync<LibreTranslateServiceResponse>();

            if(!string.IsNullOrEmpty(libreResponse.Error))
            {
                throw new Exception(libreResponse.Error);
            }
            httpResponse.EnsureSuccessStatusCode();
            return libreResponse.TranslatedText;
        }
    }
}
