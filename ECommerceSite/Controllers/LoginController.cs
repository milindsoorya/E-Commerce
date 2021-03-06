﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceSite;


namespace ECommerceSite.Controllers 
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(tbluser userModel)
        {
            using (ECommerceEntities2 db = new ECommerceEntities2())
            {
                var userDetails = db.tblusers.Where(x => x.userid == userModel.userid && x.password == userModel.password).FirstOrDefault();
                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong User Id or Password";
                    return View("Index", userModel);
                }
                else 
                {
                    Session["userid"] = userDetails.userid;
                    return RedirectToAction("Index", "Home");
                }
            }
              
        }

        public ActionResult Logout()
        {
            string userid = (string)Session["userid"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}