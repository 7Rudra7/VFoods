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
    public class PurchasesController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Purchases
        public ActionResult Index()
        {
            var tbl_Purchases = db.tbl_Purchases.Include(t => t.tbl_vendor);
            return View(tbl_Purchases.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Purchases tbl_Purchases = db.tbl_Purchases.Find(id);
            if (tbl_Purchases == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Purchases);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            ViewBag.Vendor_id_fk = new SelectList(db.tbl_vendor, "id", "VendorName");
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Vendor_id_fk,Date,Amount,Description")] tbl_Purchases tbl_Purchases)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Purchases.Add(tbl_Purchases);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Vendor_id_fk = new SelectList(db.tbl_vendor, "id", "VendorName", tbl_Purchases.Vendor_id_fk);
            return View(tbl_Purchases);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Purchases tbl_Purchases = db.tbl_Purchases.Find(id);
            if (tbl_Purchases == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vendor_id_fk = new SelectList(db.tbl_vendor, "id", "VendorName", tbl_Purchases.Vendor_id_fk);
            return View(tbl_Purchases);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Vendor_id_fk,Date,Amount,Description")] tbl_Purchases tbl_Purchases)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Purchases).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Vendor_id_fk = new SelectList(db.tbl_vendor, "id", "VendorName", tbl_Purchases.Vendor_id_fk);
            return View(tbl_Purchases);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Purchases tbl_Purchases = db.tbl_Purchases.Find(id);
            if (tbl_Purchases == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Purchases);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Purchases tbl_Purchases = db.tbl_Purchases.Find(id);
            db.tbl_Purchases.Remove(tbl_Purchases);
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
