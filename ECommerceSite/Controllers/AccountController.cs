using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerceSite;

namespace ECommerceSite.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult AddorEdit(int id = 0)
        {
            tbluser user = new tbluser();
            return View(user);
        }
        [HttpPost]
        public ActionResult AddorEdit(tbluser user)
        {
            using (ECommerceEntities db = new ECommerceEntities())
            {
                if(db.tblusers.Any(x => x.userid == user.userid))
                {
                    ViewBag.DuplicateMessage = "User Name Already Exists.";
                    return View("AddorEdit", user);
                }
                else
                {
                    db.tblusers.Add(user);
                    db.SaveChanges();
                }
                
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";
            return View("AddorEdit",new tbluser());
        }
    }
}