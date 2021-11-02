using GetYourKnowledge.Core.Services;
using GetYourKnowledge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GetYourKnowledge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AdviceSlipService _adviceSlipService;

        public HomeController(ILogger<HomeController> logger, AdviceSlipService adviceSlipService)
        {
            _logger = logger;
            _adviceSlipService = adviceSlipService;
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
