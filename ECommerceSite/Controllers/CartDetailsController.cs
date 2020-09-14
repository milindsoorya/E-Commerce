﻿using System;
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
        private ECommerceEntities db = new ECommerceEntities();





        // GET: CartDetails
        public ActionResult Index()
        {
            var cartDetails = db.CartDetails.Include(c => c.Product).Include(c => c.CartTransaction);
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
            ViewBag.cid = new SelectList(db.CartTransactions, "cid", "userid");
            return View();
        }

        // POST: CartDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Productid,cid,quantity,Price,discount")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.CartDetails.Add(cartDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Productid = new SelectList(db.Products, "productid", "productName", cartDetail.Productid);
            ViewBag.cid = new SelectList(db.CartTransactions, "cid", "userid", cartDetail.cid);
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
            ViewBag.cid = new SelectList(db.CartTransactions, "cid", "userid", cartDetail.cid);
            return View(cartDetail);
        }

        // POST: CartDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Productid,cid,quantity,Price,discount")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Productid = new SelectList(db.Products, "productid", "productName", cartDetail.Productid);
            ViewBag.cid = new SelectList(db.CartTransactions, "cid", "userid", cartDetail.cid);
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
        public ActionResult DeleteConfirmed(int id)
        {
            CartDetail cartDetail = db.CartDetails.Find(id);
            db.CartDetails.Remove(cartDetail);
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
