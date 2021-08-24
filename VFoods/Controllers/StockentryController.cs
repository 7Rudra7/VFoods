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
    public class StockentryController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Stockentry
        public ActionResult Index()
        {
            var tbl_Stockentry1 = db.tbl_Stockentry.Include(t => t.tbl_Products);

            var tbl_Stockentry = tbl_Stockentry1.Where(t => t.isDelete != true);
            return View(tbl_Stockentry.ToList());
        }

        // GET: Stockentry/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Stockentry tbl_Stockentry = db.tbl_Stockentry.Find(id);
            if (tbl_Stockentry == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Stockentry);
        }

        // GET: Stockentry/Create
        public ActionResult Create()
        {
            ViewBag.Pro_id_fk = new SelectList(db.tbl_Products.Where(x=> x.isDelete!=true), "Id", "Product_name");
            return View();
        }

        // POST: Stockentry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Pro_id_fk,Date,qty")] tbl_Stockentry tbl_Stockentry)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Stockentry.Add(tbl_Stockentry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Pro_id_fk = new SelectList(db.tbl_Products, "Id", "Product_name", tbl_Stockentry.Pro_id_fk);
            return View(tbl_Stockentry);
        }

        // GET: Stockentry/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Stockentry tbl_Stockentry = db.tbl_Stockentry.Find(id);
            if (tbl_Stockentry == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pro_id_fk = new SelectList(db.tbl_Products, "Id", "Product_name", tbl_Stockentry.Pro_id_fk);
            return View(tbl_Stockentry);
        }

        // POST: Stockentry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Pro_id_fk,Date,qty")] tbl_Stockentry tbl_Stockentry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Stockentry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Pro_id_fk = new SelectList(db.tbl_Products, "Id", "Product_name", tbl_Stockentry.Pro_id_fk);
            return View(tbl_Stockentry);
        }

        // GET: Stockentry/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Stockentry tbl_Stockentry = db.tbl_Stockentry.Find(id);
            if (tbl_Stockentry == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Stockentry);
        }

        // POST: Stockentry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Stockentry tbl_Stockentry = db.tbl_Stockentry.Find(id);
            db.tbl_Stockentry.Remove(tbl_Stockentry);
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
