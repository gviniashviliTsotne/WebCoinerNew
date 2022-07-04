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
    public class DashboardController : Controller
    {
        private Entities db = new Entities();

        // GET: GlobalDashboardLists
        public ActionResult list()
        {
            return View(db.GlobalDashboardLists.OrderByDescending(x => x.Id).ToList());
        }

        // GET: GlobalDashboardLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlobalDashboardList globalDashboardList = db.GlobalDashboardLists.Find(id);
            if (globalDashboardList == null)
            {
                return HttpNotFound();
            }
            return View(globalDashboardList);
        }

        // GET: GlobalDashboardLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GlobalDashboardLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BalanceToken,Profit,BalanceBTC,Raised,BTCWallet")] GlobalDashboardList globalDashboardList)
        {
            if (ModelState.IsValid)
            {
                globalDashboardList.DateCreated = DateTime.Now;
                db.GlobalDashboardLists.Add(globalDashboardList);
                db.SaveChanges();
                return RedirectToAction("list");
            }

            return View(globalDashboardList);
        }

        // GET: GlobalDashboardLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlobalDashboardList globalDashboardList = db.GlobalDashboardLists.Find(id);
            if (globalDashboardList == null)
            {
                return HttpNotFound();
            }
            return View(globalDashboardList);
        }

        // POST: GlobalDashboardLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BalanceToken,Profit,BalanceBTC,Raised,DateCreated,BTCWallet")] GlobalDashboardList globalDashboardList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(globalDashboardList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("list");
            }
            return View(globalDashboardList);
        }

        // GET: GlobalDashboardLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GlobalDashboardList globalDashboardList = db.GlobalDashboardLists.Find(id);
            if (globalDashboardList == null)
            {
                return HttpNotFound();
            }
            return View(globalDashboardList);
        }

        // POST: GlobalDashboardLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GlobalDashboardList globalDashboardList = db.GlobalDashboardLists.Find(id);
            db.GlobalDashboardLists.Remove(globalDashboardList);
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
