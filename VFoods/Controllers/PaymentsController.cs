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
    public class PaymentsController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Payments
        public ActionResult Index()
        {
            var tbl_payments = db.tbl_payments.Include(t => t.tbl_CustomerDetails);
            return View(tbl_payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_payments tbl_payments = db.tbl_payments.Find(id);
            if (tbl_payments == null)
            {
                return HttpNotFound();
            }
            return View(tbl_payments);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.Customerid_fk = new SelectList(db.tbl_CustomerDetails, "Id", "Account_name");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,date,Customerid_fk,sale_id,Amount,Comment")] tbl_payments tbl_payments)
        {
            if (ModelState.IsValid)
            {
                db.tbl_payments.Add(tbl_payments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Customerid_fk = new SelectList(db.tbl_CustomerDetails, "Id", "Account_name", tbl_payments.Customerid_fk);
            return View(tbl_payments);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_payments tbl_payments = db.tbl_payments.Find(id);
            if (tbl_payments == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customerid_fk = new SelectList(db.tbl_CustomerDetails, "Id", "Account_name", tbl_payments.Customerid_fk);
            return View(tbl_payments);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,date,Customerid_fk,sale_id,Amount,Comment")] tbl_payments tbl_payments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_payments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Customerid_fk = new SelectList(db.tbl_CustomerDetails, "Id", "Account_name", tbl_payments.Customerid_fk);
            return View(tbl_payments);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_payments tbl_payments = db.tbl_payments.Find(id);
            if (tbl_payments == null)
            {
                return HttpNotFound();
            }
            return View(tbl_payments);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_payments tbl_payments = db.tbl_payments.Find(id);
            db.tbl_payments.Remove(tbl_payments);
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
