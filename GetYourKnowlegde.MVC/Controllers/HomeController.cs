using GetYourKnowledge.MVC.Core.Data;
using GetYourKnowledge.MVC.Core.Data.Exceptions;
using GetYourKnowledge.MVC.Core.Services;
using GetYourKnowledge.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetYourKnowledge.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AdviceSlipService _adviceSlipService;
        private readonly LibreTranslateService _libreTranslateService;

        public HomeController(ILogger<HomeController> logger, 
            AdviceSlipService adviceSlipService, LibreTranslateService libreTranslateService)
        {
            _logger = logger;
            _adviceSlipService = adviceSlipService;
            _libreTranslateService = libreTranslateService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetAdvicesAsync([Bind("Amount")] InputAdviceAmountModel model)
        {
            if(ModelState.IsValid)
            {
                int amount = model.Amount.GetValueOrDefault();
                var advices = await _adviceSlipService.GetAdvices(amount);

                if(advices is null)
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
                if (translations.Length != advices.Count()+1)
                {
                    throw new APIException("Failed to get correct translation from LibreTranslate");
                }

                var advicesModel = new List<AdviceWithTranslationModel>();

                int i = 0;
                foreach (var advice in advices)
                {
                    var adviceModel = new AdviceWithTranslationModel(advice)
                    {
                        Translation = translations[i++]
                    };
                    advicesModel.Add(adviceModel);
                }

                return View("Advices", advicesModel.OrderBy(x => x.Id));
            }
            return View("Index", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
