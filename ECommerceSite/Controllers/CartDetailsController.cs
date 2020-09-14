using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerceSite;

namespace ECommerceSite.Controllers
{
    public class CartDetailsController : Controller
    {
        private ECommerceEntities2 db = new ECommerceEntities2();

        // GET: CartDetails
        public ActionResult Index()
        {
            var cartDetails = db.CartDetails.Include(c => c.Product).Include(c => c.tbluser);
            string UserId = Session["userid"].ToString();
            if (db.CartDetails.Where(car =>  car.userid.Equals(UserId)).FirstOrDefault() != null)
            {
                ViewBag.CartMessage = "Cart not Empty";
            }
            else
            {
                ViewBag.CartEmptyMessage = "Cart is Empty";
            }
            return View(cartDetails.ToList());
        }

        // GET: CartDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // GET: CartDetails/Create
        public ActionResult Create()
        {
            ViewBag.Productid = new SelectList(db.Products, "productid", "productName");
            ViewBag.userid = new SelectList(db.tblusers, "userid", "firstname");
            return View();
        }

        // POST: CartDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Productid,userid,quantity,Price")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Productid = new SelectList(db.Products, "productid", "productName", cartDetail.Productid);
            ViewBag.userid = new SelectList(db.tblusers, "userid", "firstname", cartDetail.userid);
            return View(cartDetail);
        }

        // GET: CartDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Productid = new SelectList(db.Products, "productid", "productName", cartDetail.Productid);
            ViewBag.userid = new SelectList(db.tblusers, "userid", "firstname", cartDetail.userid);
            return View(cartDetail);
        }

        // POST: CartDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Productid,userid,quantity,Price")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Productid = new SelectList(db.Products, "productid", "productName", cartDetail.Productid);
            ViewBag.userid = new SelectList(db.tblusers, "userid", "firstname", cartDetail.userid);
            return View(cartDetail);
        }

        // GET: CartDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartDetail cartDetail = db.CartDetails.Find(id);
            if (cartDetail == null)
            {
                return HttpNotFound();
            }
            return View(cartDetail);
        }

        // POST: CartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int productid, int quantity)
        {
            
            CartDetail cartDetail = db.CartDetails.Find(id);
            db.CartDetails.Remove(cartDetail);
            Product p1 = db.Products.Find(productid);
            p1.Availableunits = p1.Availableunits + cartDetail.quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
