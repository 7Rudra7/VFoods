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
    public class EmployeeController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Employee
        public ActionResult Index()
        {
            var tbl_emp = db.tbl_Employee.Where(x => x.isDelete != true).ToList();
            return View(tbl_emp);
        }

        // GET: Employee/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Emp_ID,Emp_Name")] tbl_Employee tbl_Employee)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Employee.Add(tbl_Employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Employee);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Emp_ID,Emp_Name")] tbl_Employee tbl_Employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            if (tbl_Employee == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            tbl_Employee tbl_Employee = db.tbl_Employee.Find(id);
            //db.tbl_Employee.Remove(tbl_Employee);
            tbl_Employee.isDelete = true;
            db.Entry(tbl_Employee).State = EntityState.Modified;
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
