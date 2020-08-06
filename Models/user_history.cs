using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    public class user_history
    {
        public int PatientID { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public bool Diabetic { get; set; }
        public bool HBP { get; set; }
        public bool Smoke { get; set; }
        public bool Alcohol { get; set; }
        public string Symptom { get; set; }
        public string Name { get; set; }
    }
}
