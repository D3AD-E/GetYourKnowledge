using GetYourKnowledge.MVC.Core.Data;
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
            var taskList = new List<Task<GenericAdvice>>();

            for (int i = 0; i < amount; i++)
            {
                taskList.Add(GetAdvice());
            }

            //concurrent running of {amount} requests, not parallel!
            try
            {
                var advices =  (await Task.WhenAll(taskList)).AsEnumerable();
                var disctinctAdvices = advices.Distinct();
                var difference = advices.Count() - disctinctAdvices.Count(); //if we have duplicates, difference would > 0
                while (difference != 0)
                {
                    advices = disctinctAdvices;
                    var additionalAdvices = await GetAdvices(difference); //get remaining advices to fill up to the amount, we know we will get the distinct ones
                    foreach (var advice in additionalAdvices)
                    {
                        advices = advices.Append(advice);
                    }
                    disctinctAdvices = advices.Distinct();
                    difference = advices.Count() - disctinctAdvices.Count();//check if there are duplicates amoung new and old advices
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
