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

        private readonly AdviceService _adviceService;

        public HomeController(ILogger<HomeController> logger, 
            AdviceService adviceService)
        {
            _logger = logger;
            _adviceService = adviceService;
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
                var advicesModel = await _adviceService.GetAdvicesWithTranslationAsync(amount);
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
