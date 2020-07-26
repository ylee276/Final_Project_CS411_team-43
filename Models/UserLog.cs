using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class UserLog
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Enter an email address")]
        public string UserName { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public bool Diabetic { get; set; }
        public bool HBP { get; set; }
        public bool Smoke { get; set; }
        public bool Alcohol { get; set; }
        public int PatientID { get; set; }

    }
}