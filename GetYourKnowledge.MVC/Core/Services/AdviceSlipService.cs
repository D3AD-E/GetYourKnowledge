using GetYourKnowledge.MVC.Core.Data;
using GetYourKnowledge.MVC.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GetYourKnowledge.MVC.Core.Services
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

        private const int MaxAdvices = 224;
        private const int MaxTries = 30;

        public AdviceSlipService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.adviceslip.com/");

            _client = client;
        }

        public async Task<GenericAdvice> GetAdvice()
        {
            var response =  await _client.GetFromJsonAsync<AdviveSlipResponse>("/advice");
            return response?.Slip;
        }

        public async Task<GenericAdvice> GetAdvice(int id)
        {
            var response = await _client.GetFromJsonAsync<AdviveSlipResponse>($"/advice/{id}");
            return response?.Slip;
        }

        public async Task<IEnumerable<GenericAdvice>> GetAdvices(int amount)
        {
            var idList = RandomHelper.GetRandomRange(1, MaxAdvices+1, amount);
            var taskList = idList.Select(id => GetAdvice(id));

            //concurrent running of {amount} requests, not parallel!
            try
            {
                var advices = await Task.WhenAll(taskList);
                for (int i = 0; i < advices.Length; i++)
                {
                    int triesAmount = 0;
                    while(advices[i] is null)
                    {
                        var num = RandomHelper.GetRandomNumberNotInCollection(1, MaxAdvices + 1, idList);
                        advices[i] = await GetAdvice(num);
                        triesAmount++;
                        if(triesAmount>=MaxTries)
                        {
                            throw new TimeoutException("Could not get advices");
                        }
                    }
                }
                return advices;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
