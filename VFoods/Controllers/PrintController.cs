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
    public class PrintController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Print
        public ActionResult Index()
        {
            var tbl_SaleDetails = db.tbl_SaleDetails.Include(t => t.tbl_Products).Include(t => t.tbl_Sales);
            return View(tbl_SaleDetails.ToList());
        }

        // GET: Print/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_SaleDetails tbl_SaleDetails = db.tbl_SaleDetails.Find(id);
            if (tbl_SaleDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_SaleDetails);
        }

        // GET: Print/Create
        public ActionResult Create()
        {
            ViewBag.Product_id_fk = new SelectList(db.tbl_Products, "Id", "Product_name");
            ViewBag.sales_id_fk = new SelectList(db.tbl_Sales, "id", "Vehicle_no");
            return View();
        }

        // POST: Print/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sales_id_fk,Product_id_fk,Product_qty,Product_total_price,isDelete")] tbl_SaleDetails tbl_SaleDetails)
        {
            if (ModelState.IsValid)
            {
                db.tbl_SaleDetails.Add(tbl_SaleDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_id_fk = new SelectList(db.tbl_Products, "Id", "Product_name", tbl_SaleDetails.Product_id_fk);
            ViewBag.sales_id_fk = new SelectList(db.tbl_Sales, "id", "Vehicle_no", tbl_SaleDetails.sales_id_fk);
            return View(tbl_SaleDetails);
        }

        // GET: Print/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_SaleDetails tbl_SaleDetails = db.tbl_SaleDetails.Find(id);
            if (tbl_SaleDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_id_fk = new SelectList(db.tbl_Products, "Id", "Product_name", tbl_SaleDetails.Product_id_fk);
            ViewBag.sales_id_fk = new SelectList(db.tbl_Sales, "id", "Vehicle_no", tbl_SaleDetails.sales_id_fk);
            return View(tbl_SaleDetails);
        }

        // POST: Print/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sales_id_fk,Product_id_fk,Product_qty,Product_total_price,isDelete")] tbl_SaleDetails tbl_SaleDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_SaleDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_id_fk = new SelectList(db.tbl_Products, "Id", "Product_name", tbl_SaleDetails.Product_id_fk);
            ViewBag.sales_id_fk = new SelectList(db.tbl_Sales, "id", "Vehicle_no", tbl_SaleDetails.sales_id_fk);
            return View(tbl_SaleDetails);
        }

        // GET: Print/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_SaleDetails tbl_SaleDetails = db.tbl_SaleDetails.Find(id);
            if (tbl_SaleDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_SaleDetails);
        }

        // POST: Print/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            tbl_SaleDetails tbl_SaleDetails = db.tbl_SaleDetails.Find(id);
            db.tbl_SaleDetails.Remove(tbl_SaleDetails);
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
