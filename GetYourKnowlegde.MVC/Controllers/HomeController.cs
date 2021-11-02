using GetYourKnowledge.MVC.Core.Services;
using GetYourKnowledge.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<IActionResult> GetQuotesAsync([Bind("Amount")] InputAdviceAmountModel model)
        {
            if(ModelState.IsValid)
            {
                var advices = _adviceSlipService.GetAdvices(model.Amount);

                var 
            }
            return View("Index",model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
