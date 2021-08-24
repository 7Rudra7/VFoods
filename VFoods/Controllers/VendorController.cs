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
    public class VendorController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Vendor
        public ActionResult Index()
        {
            var tbl_vendor = db.tbl_vendor.Where(x => x.isDelete != true).ToList();
            return View(tbl_vendor);
        }

        // GET: Vendor/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_vendor tbl_vendor = db.tbl_vendor.Find(id);
            if (tbl_vendor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor);
        }

        // GET: Vendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,VendorName,VendorNumber,VendorAddress")] tbl_vendor tbl_vendor)
        {
            if (ModelState.IsValid)
            {
                db.tbl_vendor.Add(tbl_vendor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_vendor);
        }

        // GET: Vendor/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_vendor tbl_vendor = db.tbl_vendor.Find(id);
            if (tbl_vendor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor);
        }

        // POST: Vendor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,VendorName,VendorNumber,VendorAddress")] tbl_vendor tbl_vendor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_vendor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_vendor);
        }

        // GET: Vendor/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_vendor tbl_vendor = db.tbl_vendor.Find(id);
            if (tbl_vendor == null)
            {
                return HttpNotFound();
            }
            return View(tbl_vendor);
        }

        // POST: Vendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbl_vendor tbl_vendor = db.tbl_vendor.Find(id);
            // db.tbl_vendor.Remove(tbl_vendor);

            tbl_vendor.isDelete = true;
            db.Entry(tbl_vendor).State = EntityState.Modified;
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
