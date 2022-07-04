using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebCoiner;
using System.Threading.Tasks;
using WebCoiner.Models;

namespace WebCoiner.Controllers
{
    [Authorize]
    public class UserDashboardController : Controller
    {
        private Entities db = new Entities();
        private ApplicationUserManager _userManager;


        [ChildActionOnly]
        public  ActionResult getBalanceAsync()
        {
            var user =  UserManager.FindById(User.Identity.GetUserId());
            var list = db.Transactions.Where(x => x.AspNetUser.Id == user.Id && x.Approved == 2).ToList();
            var last = db.GlobalDashboardLists.OrderByDescending(p => p.Id).FirstOrDefault();
            var userDetails = db.AspNetUsers.FirstOrDefault(x => x.Id == user.Id);

            var model = new Dashboard
            {
                BTC = list.Count() != 0 ? list.Sum(x => x.BTC) : 0,
                Tokens = list.Count() != 0 ? list.Sum(x => x.Tokens) : 0,
                Raised = last.Raised,
                InvestmentAmount = userDetails.InvestmentAmount ?? 0,
                CurrentBalance = userDetails.CurrentBalance ?? 0

            };

            return PartialView(model);
        }

        public async Task<ActionResult> Dashboard()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var list = db.Transactions.Where(x => x.AspNetUser.Id == user.Id && x.Approved == 2).ToList();
            var last = db.GlobalDashboardLists.OrderByDescending(p => p.Id).FirstOrDefault();
            var userDetails = db.AspNetUsers.FirstOrDefault(x => x.Id == user.Id);

            var model = new Dashboard
            {
                BTC = list.Count() != 0 ? list.Sum(x => x.BTC) : 0,
                Tokens = list.Count() != 0 ? list.Sum(x => x.Tokens) : 0,
                Raised = last.Raised,
                InvestmentAmount = userDetails.InvestmentAmount ?? 0,
                CurrentBalance = userDetails.CurrentBalance ?? 0

            };

            return View(model);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: UserDashboard
        public async Task<ActionResult> list()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var transactions = db.Transactions.Where(x => x.AspNetUser.Id == user.Id).Include(t => t.AspNetUser).OrderByDescending(x => x.Id).ToList();
            return View(transactions);
        }

        // GET: UserDashboard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: UserDashboard/Create
        public async Task<ActionResult> Create(decimal btc = 0)
        {

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var list = db.Transactions.Where(x => x.AspNetUser.Id == user.Id && x.Approved == 2).ToList();
            var last = db.GlobalDashboardLists.OrderByDescending(p => p.Id).FirstOrDefault();
            var userDetails = db.AspNetUsers.FirstOrDefault(x => x.Id == user.Id);

            var model = new Dashboard
            {
                BTC = list.Count() != 0 ? list.Sum(x => x.BTC) : 0,
                Tokens = list.Count() != 0 ? list.Sum(x => x.Tokens) : 0,
                Raised = last.Raised,
                InvestmentAmount = userDetails.InvestmentAmount ?? 0,
                CurrentBalance = userDetails.CurrentBalance ?? 0
            };
            ViewBag.BTCWallet = db.GlobalDashboardLists.OrderByDescending(p => p.Id).FirstOrDefault().BTCWallet;

            return View(model);

            //ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            //return View(new Transaction { Tokens = (int)(btc * 535000), BTC = btc });
        }

        // POST: UserDashboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BTC")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.UserId = User.Identity.GetUserId();
                transaction.Approved = 0;
                transaction.DateCreated = DateTime.Today;
                transaction.Tokens = (Int32)(transaction.BTC * 535000);

                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("list");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", transaction.UserId);
            return View(transaction);
        }

        //// GET: UserDashboard/Edit/5
        //public ActionResult Edit(int? id)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Transaction transaction = db.Transactions.Find(id);
        //    if (transaction == null && transaction.Approved != 0)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", transaction.UserId);
        //    return View(transaction);
        //}

        //// POST: UserDashboard/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,UserId,DateCreated,Tokens,BTC,Approved")] Transaction transaction)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Transaction transactionedit = db.Transactions.Find(transaction.Id);
        //        if (transaction == null && transactionedit.Approved != 0)
        //        {
        //            return HttpNotFound();
        //        }

        //        db.Entry(transaction).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("list");
        //    }
        //    ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", transaction.UserId);
        //    return View(transaction);
        //}

        // GET: UserDashboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null && transaction.Approved != 0 && transaction.AspNetUser.Id != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: UserDashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);

            if (transaction == null && transaction.Approved != 0 && transaction.AspNetUser.Id != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            db.Transactions.Remove(transaction);
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
