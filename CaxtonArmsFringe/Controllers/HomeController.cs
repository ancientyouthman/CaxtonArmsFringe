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
        private ReCaptchaService _reCaptchaService = new ReCaptchaService();

        [HttpGet]
        public ActionResult Index()
        {
            return View("Welcome");
        }

        [HttpPost]
        public ActionResult Brochure(BrochureSubmissionModel model)
        {
            if (!ModelState.IsValid) return View("Brochure", model);
            var encodedResponse = Request.Form["g-Recaptcha-Response"];
            bool captchaValid = (_reCaptchaService.Validate(encodedResponse) == "true" ? true : false);

            if (!captchaValid)
            {
                ModelState.AddModelError("", "Please confirm you're not a robot");
                return View(model);
            }

            var result = _emailService.SendEmail(model);
            if (result.Success) return Redirect("Success");
            ModelState.AddModelError("", result.Message);
            return View("Index", model);
        }

        public ActionResult Brochure()
        {
            return View();
        }


        public ActionResult Success()
        {
            return View();
        }
    }
}