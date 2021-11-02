using GetYourKnowledge.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GetYourKnowledge.Core.Services
{
    /// <summary>
    /// AdviceSlipService is used for getting JSON of advices in English
    /// </summary>
    public class AdviceSlipService
    {
        /// <summary>
        /// AdviveSlipResponse is parsed JSON response from adviceslip.com
        /// </summary>
        private class AdviveSlipResponse
        {
            public GenericAdvice Slip { get; set; }
        }

        private readonly HttpClient _client;

        public AdviceSlipService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.adviceslip.com/");

            _client = client;
        }

        public async Task<GenericAdvice> GetAdvice()
        {
            var response =  await _client.GetFromJsonAsync<AdviveSlipResponse>("/advice");
            return response.Slip;
        }

        public async Task<IEnumerable<GenericAdvice>> GetAdvices(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
