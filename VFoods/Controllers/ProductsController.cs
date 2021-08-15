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
    public class ProductsController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.tbl_Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Product_name,Rate_box,Piece_box,MRP_bottle,Product_stock")] tbl_Products tbl_Products)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Products.Add(tbl_Products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Products);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Product_name,Rate_box,Piece_box,MRP_bottle,Product_stock")] tbl_Products tbl_Products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            db.tbl_Products.Remove(tbl_Products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetProductByCode(string pCode)
        {
            db.Configuration.ProxyCreationEnabled = false;
            int x = Int32.Parse(pCode);
            tbl_Products objProduct = new tbl_Products();

            try
            {
                //objProduct = (tbl_Products)db.tbl_Products.Where(a => a.Id == x);

                objProduct =  db.tbl_Products.Find(x);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(objProduct, JsonRequestBehavior.AllowGet);
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
