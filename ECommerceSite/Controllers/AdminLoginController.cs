using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceSite.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            using (ECommerceEntities2 db = new ECommerceEntities2())
            {
                var userDetails = db.Admins.Where(x => x.userid == admin.userid && x.password == admin.password).FirstOrDefault();
                if (userDetails == null)
                {
                    admin.LoginErrorMessage = "Wrong UserName or Password";
                    return View("Login", admin);

                }
                    ModelState.Clear();
                    Session["userid"] = admin.userid;
                    return RedirectToAction("Index", "Products");
                
            }
            
        }
    }
}