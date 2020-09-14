using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ECommerceSite.Controllers
{
    public class HomeController : Controller
    {
        private ECommerceEntities2 db = new ECommerceEntities2();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult About()
        {
            return View();
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }




        [HttpPost]
        public ActionResult Details(string itemno, int ProductId)
        {
            
            string UserId = Session["userid"].ToString();
            decimal noofunits = decimal.Parse(itemno);
           
            

                var p = db.Products.Where(pro => pro.productid.Equals(ProductId)).FirstOrDefault();

                if (noofunits > p.Availableunits)
                {
                    ViewBag.Error = "No stock available";
                    return View("Details",p);
                }
            else
                {
                    if (db.CartDetails.Where(car => car.Productid.Value.Equals(ProductId) && car.userid.Equals(UserId)).FirstOrDefault() != null)
                    {

                        CartDetail cart = db.CartDetails.Where(car => car.Productid.Value.Equals(ProductId) && car.userid.Equals(UserId)).FirstOrDefault();
                        cart.quantity = cart.quantity + noofunits;

                        db.Entry(cart).State = EntityState.Modified;
                        ViewBag.Added = "Added to Existing Cart";
                        p.Availableunits = p.Availableunits - cart.quantity>0? p.Availableunits - cart.quantity:0 ;

                    db.SaveChanges();
                    return View("Details", p);
                }
                    else
                    {
                        CartDetail cart = new CartDetail();
                        cart.userid = Session["userid"].ToString();
                        cart.Productid = p.productid;
                        
                        cart.quantity = noofunits;

                        cart.Price = (p.price - (p.price*(p.discount)/100))*noofunits;
                        db.CartDetails.Add(cart);
                    ViewBag.Added = "Added to Cart";
                    p.Availableunits = p.Availableunits - cart.quantity;

                    db.SaveChanges();
                    return View("Details", p);
                }
                }
            
            //return RedirectToAction("Index", "Home");
        }













        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productid,productName,CategoryName,Availableunits,price,discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productid,productName,CategoryName,Availableunits,price,discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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