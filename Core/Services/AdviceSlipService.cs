using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetYourKnowledge.Core.Services
{
    /// <summary>
    /// AdviceSlipService is used for getting JSON of advices in English
    /// </summary>
    public class AdviceSlipService
    {
        public HttpClient Client { get; }
    }
}
