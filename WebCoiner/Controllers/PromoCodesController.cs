﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCoiner;

namespace WebCoiner.Controllers
{
    [Authorize(Roles = "administrator")]
    public class PromoCodesController : Controller
    {
        private Entities db = new Entities();

        // GET: PromoCodes
        public ActionResult list()
        {
            return View(db.PromoCodes.ToList());
        }

        // GET: PromoCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoCode promoCode = db.PromoCodes.Find(id);
            if (promoCode == null)
            {
                return HttpNotFound();
            }
            return View(promoCode);
        }

        // GET: PromoCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromoCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PromoCode1")] PromoCode promoCode)
        {
            if (string.IsNullOrEmpty(promoCode.PromoCode1))
            {
                ModelState.AddModelError(string.Empty, "Promocode is empty");
            }

            if (ModelState.IsValid)
            {
                db.PromoCodes.Add(promoCode);
                db.SaveChanges();
                return RedirectToAction("list");
            }

            return View(promoCode);
        }

        // GET: PromoCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoCode promoCode = db.PromoCodes.Find(id);
            if (promoCode == null)
            {
                return HttpNotFound();
            }
            return View(promoCode);
        }

        // POST: PromoCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PromoCode1")] PromoCode promoCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promoCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list");
            }
            return View(promoCode);
        }

        // GET: PromoCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoCode promoCode = db.PromoCodes.Find(id);
            if (promoCode == null)
            {
                return HttpNotFound();
            }
            return View(promoCode);
        }

        // POST: PromoCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromoCode promoCode = db.PromoCodes.Find(id);
            db.PromoCodes.Remove(promoCode);
            db.SaveChanges();
            return RedirectToAction("list");
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
