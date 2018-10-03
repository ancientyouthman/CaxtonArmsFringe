using CaxtonArmsFringe.Models;
using CaxtonArmsFringe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaxtonArmsFringe.Controllers
{
    public class HomeController : Controller
    {
        private EmailService _emailService = new EmailService();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitBrochure(BrochureSubmissionModel model)
        {
            if (!ModelState.IsValid) return View("Index", model);
            var result = _emailService.SendEmail(model);
            if (result.Success) return Redirect("Success");
            ModelState.AddModelError("", result.Message);
            return View("Index", model);
        }

        public ActionResult About()
        {

            return View();
        }


        public ActionResult Success()
        {
            return View();
        }
    }
}