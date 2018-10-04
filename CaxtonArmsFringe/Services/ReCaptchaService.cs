using CaxtonArmsFringe.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CaxtonArmsFringe.Services
{
    public class ReCaptchaService
    {
        public string Validate(string encodedResponse)
        {
            var client = new System.Net.WebClient();

            var secret = ConfigurationManager.AppSettings["ReCaptchaSecret"];
            var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, encodedResponse));

            var captchaResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaResponse>(googleReply);

            return captchaResponse.Success.ToLower();
        }

    }
}