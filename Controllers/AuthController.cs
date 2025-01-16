using LoginandReset.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginandReset.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            Session["UserName"] = "";
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(string txtUserName, string txtPassword)
        {
            Session["UserName"] = "";
            string Message = "Unauthorized";

            BaseAccount baseAccount = new BaseAccount();
            if (baseAccount.VerifyUser(txtUserName, txtPassword))
            {
                Message = "Authorized";
                Session["UserName"] = txtUserName;
                //return Redirect("www.google.com"); 
                return RedirectToAction("About", "Home");
            }
            ViewBag.Message = Message;
            //return RedirectToAction("Dashboard", "Inventory");
            return View("Login");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("UserName");
            return View("Login");
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoReset(string txtUserName, string newPassword, string confirmPassword)
        {
            Session["UserName"] = "";
            string Message = "Unauthorized";
            if (newPassword != confirmPassword)
            {
                ViewBag.Message = "Passwords do not match!";
                return View("ResetPassword");
            }
            ResetAccount resetAccount= new ResetAccount();
            if (resetAccount.VerifyUser(txtUserName, newPassword))
            {
                Message = "Authorized";
                Session["UserName"] = txtUserName;
                //return Redirect("www.google.com"); 
                return RedirectToAction("About", "Home");
            }
            ViewBag.Message = Message;
            //return RedirectToAction("Dashboard", "Inventory");
            return View("Login");
        }
    }
}