using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GetYourKnowledge.Core.Services
{
    /// <summary>
    /// LibreTranslateService is used for translating advices from English to Polish
    /// </summary>
    public class LibreTranslateService
    {
        public HttpClient Client { get; }
    }
}
