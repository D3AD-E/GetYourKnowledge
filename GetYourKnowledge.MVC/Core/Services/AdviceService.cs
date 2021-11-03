using GetYourKnowledge.MVC.Core.Data;
using GetYourKnowledge.MVC.Core.Data.Exceptions;
using GetYourKnowledge.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetYourKnowledge.MVC.Core.Services
{
    /// <summary>
    /// LibreTranslateService gets translated advices
    /// </summary>
    public class AdviceService
    {
        private readonly AdviceSlipService _adviceSlipService;
        private readonly LibreTranslateService _libreTranslateService;

        public AdviceService(AdviceSlipService adviceSlipService, LibreTranslateService libreTranslateService)
        {
            _adviceSlipService = adviceSlipService;
            _libreTranslateService = libreTranslateService;
        }

        public async Task<IEnumerable<AdviceWithTranslationModel>> GetAdvicesWithTranslationAsync(int amount)
        {
            var advices = await _adviceSlipService.GetAdvices(amount);
            if (advices is null)
            {
                throw new APIException("AdviceSlip API error");
            }

            //it is better to have 1 request with long translation rather than 20 requests with short
            var translationStringBuilder = new StringBuilder();

            foreach (var advice in advices)
            {
                translationStringBuilder.AppendLine(advice.Advice);
            }

            var translationsSpan = await _libreTranslateService.TranslateAsync(translationStringBuilder.ToString(),
                LanguageType.English, LanguageType.Polish);

            var translations = translationsSpan.Split('\n');

            //translation length must be 1 more than of advices, due to appendline
            //it cound be fixed by checking whether we have reached the last element and if so, appeding, instead of appending of newline
            //but this is more efficient
            if (translations.Length != advices.Count() + 1)
            {
                throw new APIException("Failed to get correct translation from LibreTranslate");
            }

            var advicesTranslated = advices.Select((advice, index) => new AdviceWithTranslationModel(advice)
            {
                Translation = translations[index]
            });

            return advicesTranslated;
        }
    }
}
