using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VFoods;

namespace VFoods.Controllers
{
    [Authorize]
    public class AccountEntriesController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: AccountEntries
        public ActionResult Index()
        {
            var tbl_AccountEntries = db.tbl_AccountEntries.Include(t => t.tbl_accounts);
            return View(tbl_AccountEntries.ToList());
        }

        // GET: AccountEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_AccountEntries tbl_AccountEntries = db.tbl_AccountEntries.Find(id);
            if (tbl_AccountEntries == null)
            {
                return HttpNotFound();
            }
            return View(tbl_AccountEntries);
        }

        // GET: AccountEntries/Create
        public ActionResult Create()
        {
            ViewBag.Acc_id_fk = new SelectList(db.tbl_accounts, "id", "Account_name");
            return View();
        }

        // POST: AccountEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Acc_id_fk,Amount,Type,Date,Comment")] tbl_AccountEntries tbl_AccountEntries)
        {
            if (ModelState.IsValid)
            {
                db.tbl_AccountEntries.Add(tbl_AccountEntries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Acc_id_fk = new SelectList(db.tbl_accounts, "id", "Account_name", tbl_AccountEntries.Acc_id_fk);
            return View(tbl_AccountEntries);
        }

        // GET: AccountEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_AccountEntries tbl_AccountEntries = db.tbl_AccountEntries.Find(id);
            if (tbl_AccountEntries == null)
            {
                return HttpNotFound();
            }
            ViewBag.Acc_id_fk = new SelectList(db.tbl_accounts, "id", "Account_name", tbl_AccountEntries.Acc_id_fk);
            return View(tbl_AccountEntries);
        }

        // POST: AccountEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Acc_id_fk,Amount,Type,Date,Comment")] tbl_AccountEntries tbl_AccountEntries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_AccountEntries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Acc_id_fk = new SelectList(db.tbl_accounts, "id", "Account_name", tbl_AccountEntries.Acc_id_fk);
            return View(tbl_AccountEntries);
        }

        // GET: AccountEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_AccountEntries tbl_AccountEntries = db.tbl_AccountEntries.Find(id);
            if (tbl_AccountEntries == null)
            {
                return HttpNotFound();
            }
            return View(tbl_AccountEntries);
        }

        // POST: AccountEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_AccountEntries tbl_AccountEntries = db.tbl_AccountEntries.Find(id);
            db.tbl_AccountEntries.Remove(tbl_AccountEntries);
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
