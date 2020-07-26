using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project.Models;
using DataLibrary.Logic;
using DataLibrary.Models;
using DataLibrary.DataAccess;

namespace Final_Project.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Personal Care Assistance - You can breathe easily";

            return View();
        }


        public ActionResult userAccount()
        {
            Models.UserLog userInfo = new Models.UserLog
            {
                PatientID = globalVariables.PatientID,
                UserName = globalVariables.UserName,
                FirstName = globalVariables.FirstName,
                LastName = globalVariables.LastName,
                Password = globalVariables.Password,
                Height = globalVariables.Height,
                Weight = globalVariables.Weight,
                HBP = globalVariables.HBP,
                Diabetic = globalVariables.Diabetic,
                Alcohol = globalVariables.Alcohol,
                Age = globalVariables.Age
            };
         
            return View(userInfo);
        }

        [HttpPost]
        public ActionResult UpdateUserAccount(Models.UserLog model)
        {
            if (ModelState.IsValid)
            {
                DataLibrary.Models.User.newUserSignUp newUser = new DataLibrary.Models.User.newUserSignUp
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Height = model.Height,
                    Weight = model.Weight,
                    Diabetic = model.Diabetic,
                    HBP = model.HBP,
                    Smoke = model.Smoke,
                    Alcohol = model.Alcohol,
                    PatientID = globalVariables.PatientID
                };

                DataLibrary.Logic.User.user.updateUserAccount(newUser);
                TempData["message"] = "Your Changes Have Been Successfully Saved";
                //reset the global variable to new updated values
                globalVariables.UserName = newUser.UserName;
                globalVariables.FirstName = newUser.FirstName;
                globalVariables.LastName = newUser.LastName;
                globalVariables.Password = newUser.Password;
                globalVariables.Height = newUser.Height;
                globalVariables.Weight = newUser.Weight;
                globalVariables.HBP = newUser.HBP;
                globalVariables.Diabetic = newUser.Diabetic;
                globalVariables.Alcohol = newUser.Alcohol;
                globalVariables.Age = newUser.Age;
            }
            return View("userAccount");
        }
        public ActionResult DeleteUserAccount()
        {        
                DataLibrary.Logic.User.user.DeleteUserAccount(globalVariables.PatientID);
                TempData["message"] = "Your Profile Has Been Deleted Successfully";
                //reset the global variable
                globalVariables.UserName = "";
                globalVariables.FirstName = "";
                globalVariables.LastName = "";
                globalVariables.Password = "";
                globalVariables.Height = -1;
                globalVariables.Weight = -1;
                globalVariables.Age = -1;
                globalVariables.HBP = false;
                globalVariables.Diabetic = false;
                globalVariables.Alcohol = false;


            return View("~/Views/Login/Index.cshtml");
        }
    }
}