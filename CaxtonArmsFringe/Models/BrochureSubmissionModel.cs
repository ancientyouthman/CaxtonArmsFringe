using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CaxtonArmsFringe.Models
{
    public class BrochureSubmissionModel
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "Name to display in brochure")]
        public string StageName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = "Twitter handles")]
        public string TwitterHandle { get; set; }

        [MaxLength(600, ErrorMessage = "Please limit your bio to 600 characters")]
        public string Bio { get; set; }

        [Display(Name = "Ticket price")]
        public string TicketPrice { get; set; }

        public HttpPostedFileBase Photo { get; set; }

        [Display(Name = "Any additional information you'd like to add?")]
        public string AdditionalInfo { get; set; }

    }
}