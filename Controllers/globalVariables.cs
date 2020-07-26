using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project.Controllers
{
    public class BaseController : System.Web.Mvc.Controller
    {
        public class globalVariables
        {
            public static string UserName { get; set; }
            public static string Password { get; set; }
            public static string FirstName { get; set; }
            public static string LastName { get; set; }
            public static int Age { get; set; }
            public static int Height { get; set; }
            public static int Weight { get; set; }
            public static bool Diabetic { get; set; }
            public static bool HBP { get; set; }
            public static bool Smoke { get; set; }
            public static bool Alcohol { get; set; }
            public static int PatientID { get; set; }
        }
    }
}