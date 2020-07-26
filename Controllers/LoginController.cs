using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary.Logic;
using DataLibrary.Models;
using DataLibrary.DataAccess;
using Final_Project;
namespace Final_Project.Controllers
{
    
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(Final_Project.Models.User userModel)
        {
            DataLibrary.Models.User.newUserSignUp user = new DataLibrary.Models.User.newUserSignUp
            {
                UserName = userModel.UserName,
                Password = userModel.Login_Password
            };

            user = DataLibrary.Logic.User.user.validateUser(user);
            if (user != null)
            {
                globalVariables.PatientID = user.PatientID;
                globalVariables.UserName = user.UserName;
                globalVariables.FirstName = user.FirstName;
                globalVariables.LastName = user.LastName;
                globalVariables.Password = user.Password;
                globalVariables.Height = user.Height;
                globalVariables.Weight = user.Weight;
                globalVariables.HBP = user.HBP;
                globalVariables.Diabetic = user.Diabetic;
                globalVariables.Alcohol = user.Alcohol;
                globalVariables.Age = user.Age;
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                userModel.LoginErrorMessage = "Wrong UserName or Password";
                return View("Index", userModel);
            }


        }
        public ActionResult SignUp()
        {
            ViewBag.Message = "Register User";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(UserLog model)
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
                    Alcohol = model.Alcohol
                };

                globalVariables.PatientID = DataLibrary.Logic.User.newUserSignUp.addUser(newUser);

                //check for repeating userNAme and dont assign global variables
                //globalVariables.PatientID = newUser.PatientID;
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

                return View("~/Views/Home/Index.cshtml");
            }
            return View();
        }
    }
}
