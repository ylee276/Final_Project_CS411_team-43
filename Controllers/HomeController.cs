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

        [HttpGet]
        public ActionResult GetUserHistory()
        {
            List<DataLibrary.Models.User.User_history> list = new List<DataLibrary.Models.User.User_history>();

            list = DataLibrary.Logic.User.user.GetHistory(globalVariables.PatientID);
            //ViewBag.data = symptoms.symptoms;

            return View(list);
        }


        public ActionResult LogOut()
        {

            TempData["message"] = "Successfully Log Out";

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


            return Redirect("~/login/index");
            //return View("~/Views/Login/Index.cshtml");
        }
        
        [HttpPost]
        public ActionResult GetDiseaseTable(Diseases model)
        {
            if (ModelState.IsValid)
            {
                DataLibrary.Models.User.AllDisease DiseasesX = new DataLibrary.Models.User.AllDisease
                {
                    Disease = model.Disease,
                    Description = model.Description
                };
                if (model.Disease == null)
                {
                    DiseasesX.Disease = "";
                    DiseasesX.Description = "";
                }
                DiseasesX = DataLibrary.Logic.User.Disease.GetDisease(DiseasesX);
                
                globalVariables.Disease = DiseasesX.Disease;
                globalVariables.Description = DiseasesX.Description;
                
            }
            return RedirectToAction("Disease_Table");
        }

        public ActionResult Disease_Table()
        {
            Models.Diseases DiseasesX = new Models.Diseases
            {
                Disease = globalVariables.Disease,
                Description = globalVariables.Description
            };
            ViewBag.data = DiseasesX.Description;


            return View(DiseasesX);
        }//This function is used to store/show the disease Description  

        [HttpGet]
        public ActionResult GetAllSymptoms()
        {
            List<DataLibrary.Models.User.user_symptoms> list = new List<DataLibrary.Models.User.user_symptoms>();

            list =  DataLibrary.Logic.User.symptoms.GetAllSymptoms();
            //ViewBag.data = symptoms.symptoms;

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetAllSymptoms(List<DataLibrary.Models.User.user_symptoms> list)
        {
            List<int> symptomIDs = new List<int>();
            foreach (DataLibrary.Models.User.user_symptoms symptom in list)
            {
                if (symptom.Checked == true)
                {
                    if (symptomIDs.Count >= 17)
                    {
                        //userModel.LoginErrorMessage = "You selected more than 17 symptoms please select only up to 17 symptoms";
                        //return View("Index", userModel);

                        TempData["message"] = "Please select maximum 17 symptoms";
                        return View(list);
                    }
                    else
                    {
                        symptomIDs.Add(symptom.Id);
                    }
                }
            }
            //test
            if(symptomIDs.Count <= 0)
            {
                TempData["message"] = "Please select at least 1 symptoms";
                return View(list);
            }
            int id = DataLibrary.Logic.User.symptoms.StoreUserSymptoms(symptomIDs, globalVariables.PatientID);
            return RedirectToAction("SuccessfulDiagonosis", new { id = id });



        }
        //[HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult SuccessfulDiagonosis(int id = 0)
        {
            List<DataLibrary.Models.Disease.Precaution> precautionsList = new List<DataLibrary.Models.Disease.Precaution>();
            precautionsList = DataLibrary.Logic.User.symptoms.helperPrecautions(id);
            foreach (DataLibrary.Models.Disease.Precaution precaution in precautionsList)
            {
                if (precaution.errorMessage != null)
                {
                    TempData["message"] = precaution.errorMessage;

                    return RedirectToAction("GetAllSymptoms");
                }
            }
            return View(precautionsList);
        }

    }
}