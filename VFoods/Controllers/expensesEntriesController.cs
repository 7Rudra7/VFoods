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
    public class expensesEntriesController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: expensesEntries
        public ActionResult Index()
        {
            var tbl_expensesEntries = db.tbl_expensesEntries.Include(t => t.Category);
            var tbl_expemt = tbl_expensesEntries.Where(a => a.isDelete != true).ToList();
            return View(tbl_expemt);
        }

        // GET: expensesEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_expensesEntries tbl_expensesEntries = db.tbl_expensesEntries.Find(id);
            if (tbl_expensesEntries == null)
            {
                return HttpNotFound();
            }
            return View(tbl_expensesEntries);
        }

        // GET: expensesEntries/Create
        public ActionResult Create()
        {
            ViewBag.Cat_id_fk = new SelectList(db.Categories, "CategoryID", "CategortyName");
            return View();
        }

        // POST: expensesEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Cat_id_fk,Comments,Amout")] tbl_expensesEntries tbl_expensesEntries)
        {
            if (ModelState.IsValid)
            {
                db.tbl_expensesEntries.Add(tbl_expensesEntries);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cat_id_fk = new SelectList(db.Categories, "CategoryID", "CategortyName", tbl_expensesEntries.Cat_id_fk);
            return View(tbl_expensesEntries);
        }

        // GET: expensesEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_expensesEntries tbl_expensesEntries = db.tbl_expensesEntries.Find(id);
            if (tbl_expensesEntries == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat_id_fk = new SelectList(db.Categories, "CategoryID", "CategortyName", tbl_expensesEntries.Cat_id_fk);
            return View(tbl_expensesEntries);
        }

        // POST: expensesEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Cat_id_fk,Comments,Amout")] tbl_expensesEntries tbl_expensesEntries)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_expensesEntries).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cat_id_fk = new SelectList(db.Categories, "CategoryID", "CategortyName", tbl_expensesEntries.Cat_id_fk);
            return View(tbl_expensesEntries);
        }

        // GET: expensesEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_expensesEntries tbl_expensesEntries = db.tbl_expensesEntries.Find(id);
            if (tbl_expensesEntries == null)
            {
                return HttpNotFound();
            }
            return View(tbl_expensesEntries);
        }

        // POST: expensesEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_expensesEntries tbl_expensesEntries = db.tbl_expensesEntries.Find(id);

           
            tbl_expensesEntries.isDelete = true;
            db.Entry(tbl_expensesEntries).State = EntityState.Modified;
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
