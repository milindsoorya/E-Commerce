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
    public class TransactionBillsController : Controller
    {
        private ECommerceEntities2 db = new ECommerceEntities2();

        // GET: TransactionBills
        public ActionResult Index()
        {
            return View(db.TransactionBills.ToList());
        }

        // GET: TransactionBills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionBill transactionBill = db.TransactionBills.Find(id);
            if (transactionBill == null)
            {
                return HttpNotFound();
            }
            return View(transactionBill);
        }

        // GET: TransactionBills/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionBills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id = 0)
        {
            TransactionBill bill = new TransactionBill();

            if (!db.CartDetails.Any())
            {
                ViewBag.Error = "Cart is Empty";
               
            }
            else
            {
                string UserId = Session["userid"].ToString();
                bill.userid = UserId;
                var cart = db.CartDetails.Where(pro => pro.userid.Equals(UserId)).FirstOrDefault();
                bill.amount = db.CartDetails.Select(t => t.Price ?? 0).Sum();
                bill.TransactionDate = DateTime.Now;
                db.TransactionBills.Add(bill);
                db.CartDetails.Remove(cart);
                db.SaveChanges();
                ViewBag.Success = "Paid";
                return RedirectToAction("Index");
            }

            //ViewBag.cid = new SelectList(db.CartDetails, "id", "userid", transactionBill.cid);
            return View(bill);
        }

        // GET: TransactionBills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionBill transactionBill = db.TransactionBills.Find(id);
            if (transactionBill == null)
            {
                return HttpNotFound();
            }
            return View(transactionBill);
        }

        // POST: TransactionBills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Transactionid,userid,amount,TransactionDate")] TransactionBill transactionBill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactionBill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transactionBill);
        }

        // GET: TransactionBills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransactionBill transactionBill = db.TransactionBills.Find(id);
            if (transactionBill == null)
            {
                return HttpNotFound();
            }
            return View(transactionBill);
        }

        // POST: TransactionBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionBill transactionBill = db.TransactionBills.Find(id);
            db.TransactionBills.Remove(transactionBill);
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
