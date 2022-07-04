
using Microsoft.AspNet.Identity;
using System;
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
    public class UsersController : Controller
    {
        private Entities db = new Entities();

        // GET: AspNetUsers
        public ActionResult list()
        {
            return View(db.AspNetUsers.ToList());
        }

        // GET: AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        //// GET: AspNetUsers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: AspNetUsers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,FirstName,LastName,PasswordHash,PromoCode,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Mobile,Wallet,AddressToken,NotificationResumptionSales,NotificationLatesNews,NotificationUnusualActivity")] AspNetUser aspNetUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.AspNetUsers.Add(aspNetUser);
        //        db.SaveChanges();
        //        return RedirectToAction("list");
        //    }

        //    return View(aspNetUser);
        //}

        // GET: AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,LastName,PromoCode,PhoneNumber,Wallet,InvestmentAmount,CurrentBalance")] AspNetUser aspNetUser)
        {
            if (ModelState.IsValid)
            {
                var _user = db.AspNetUsers.FirstOrDefault(x => x.Id == aspNetUser.Id);
                _user.Email = aspNetUser.Email;
                _user.FirstName = aspNetUser.FirstName;
                _user.LastName = aspNetUser.LastName;
                _user.PromoCode = aspNetUser.PromoCode;
                _user.PhoneNumber = aspNetUser.PhoneNumber;
                _user.Wallet = aspNetUser.Wallet;
                _user.InvestmentAmount = aspNetUser.InvestmentAmount;
                _user.CurrentBalance = aspNetUser.CurrentBalance;

                db.AspNetUsers.Attach(_user);
                var entry = db.Entry(_user);

                entry.Property(x => x.Email).IsModified = true;
                entry.Property(x => x.FirstName).IsModified = true;
                entry.Property(x => x.LastName).IsModified = true;
                entry.Property(x => x.PromoCode).IsModified = true;
                entry.Property(x => x.PhoneNumber).IsModified = true;
                entry.Property(x => x.Wallet).IsModified = true;
                entry.Property(x => x.InvestmentAmount).IsModified = true;
                entry.Property(x => x.CurrentBalance).IsModified = true;
                
                db.SaveChanges();
                return RedirectToAction("list");
            }
            return View(aspNetUser);
        }

        // GET: AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
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
