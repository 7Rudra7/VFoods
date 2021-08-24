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
    public class VehicleController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Vehicle
        public ActionResult Index()
        {
            var tbl_vehicles = db.tbl_Vehicle.Where(x => x.isDelete != true).ToList();
            return View(tbl_vehicles);
        }

        // GET: Vehicle/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Vehicle tbl_Vehicle = db.tbl_Vehicle.Find(id);
            if (tbl_Vehicle == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Vehicle);
        }

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Vehicle_number,Fitness_date,Insurance_date,MH_tax,AI_tax,Permit,PUC,Comments")] tbl_Vehicle tbl_Vehicle)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Vehicle.Add(tbl_Vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Vehicle);
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Vehicle tbl_Vehicle = db.tbl_Vehicle.Find(id);
            if (tbl_Vehicle == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Vehicle);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Vehicle_number,Fitness_date,Insurance_date,MH_tax,AI_tax,Permit,PUC,Comments")] tbl_Vehicle tbl_Vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Vehicle);
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Vehicle tbl_Vehicle = db.tbl_Vehicle.Find(id);
            if (tbl_Vehicle == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbl_Vehicle tbl_Vehicle = db.tbl_Vehicle.Find(id);
            tbl_Vehicle.isDelete = true;
            db.Entry(tbl_Vehicle).State = EntityState.Modified;
            //db.tbl_Vehicle.Remove(tbl_Vehicle);
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
