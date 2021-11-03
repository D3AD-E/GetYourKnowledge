using GetYourKnowledge.MVC.Core.Data.Exceptions;
using GetYourKnowledge.MVC.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GetYourKnowledge.MVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
        /// <summary>
        /// Should only be used in non-development env
        /// </summary>
        public IActionResult GenericError()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature?.Error is APIException exception)
            {
                Response.StatusCode = exception.StatusCode;

                return View(new GenericErrorModel { StatusCode = exception.StatusCode, Message = exception.Message });
            }

            return View(new GenericErrorModel { StatusCode = 500, Message = "Unhandled exception occured" });
        }
    }
}
