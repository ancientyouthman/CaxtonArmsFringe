using CaxtonArmsFringe.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CaxtonArmsFringe.Services
{
    public class EmailService
    {
        public SendEmailAttempt SendEmail(BrochureSubmissionModel model)
        {
            var result = new SendEmailAttempt {Success = false };

            try
            {

                var emailFrom = ConfigurationManager.AppSettings["EmailFrom"];
                var emailTo = ConfigurationManager.AppSettings["EmailTo"];
                var gmailAddress = ConfigurationManager.AppSettings["GmailAddress"];
                var gmailPassword = ConfigurationManager.AppSettings["GmailPassword"];


                MailMessage mail = new MailMessage(emailFrom, emailTo);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(gmailAddress, gmailPassword);
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Host = "smtp.gmail.com";
                mail.Subject = "Submission from online brochure form";
                mail.Body = RenderEmailBody(model);
                mail.IsBodyHtml = true;

                if (model.Photo != null && model.Photo.ContentLength > 0)
                {
                    if (!ValidatePhotoUpload(model.Photo))
                    {
                        result.Message = "Please upload a photo in one of the following formats: jpg, png, gif";
                        return result;
                    }
                    string fileName = Path.GetFileName(model.Photo.FileName);
                    var attachment = new Attachment(model.Photo.InputStream, fileName);
                    mail.Attachments.Add(attachment);
                }

                smtpClient.Send(mail);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        private string RenderEmailBody(BrochureSubmissionModel model)
        {
            var result = "";

            result += "<h1>New submission from online brochure form</h1>";

            if (!string.IsNullOrEmpty(model.Name)) result += "Name: " + model.Name + "<br />";
            if (!string.IsNullOrEmpty(model.StageName)) result += "Stage name: " + model.StageName + "<br />";
            if (!string.IsNullOrEmpty(model.Email)) result += "Email: <a href=\"mailto:" + model.Email + "\">" + model.Email + "</a><br />";
            if (!string.IsNullOrEmpty(model.TwitterHandle)) result += "Twitter: " + model.TwitterHandle + "<br />";
            if (!string.IsNullOrEmpty(model.Bio)) result += "Bio: " + model.Bio + "<br />";
            if (!string.IsNullOrEmpty(model.TicketPrice)) result += "Ticket price: " + model.TicketPrice + "<br />";
            if (!string.IsNullOrEmpty(model.AdditionalInfo)) result += "Addtional info: " + model.AdditionalInfo + "<br />";

            return result;
        }

        public bool ValidatePhotoUpload(HttpPostedFileBase photo)
        {
            string[] allowedFormats = { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = System.IO.Path.GetExtension(photo.FileName);
            return allowedFormats.Contains(extension);
        }
    }
}