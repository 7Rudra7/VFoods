using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VFoods;

namespace VFoods.Controllers
{
    public class SalaryController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Salary
        public ActionResult Index()
        {
            var tbl_salary = db.tbl_salary.Include(t => t.tbl_Employee);

            return View(tbl_salary.ToList());
        }

        // GET: search functionality
        [HttpGet]
          
        public  ActionResult Index(string searching)
        {
            ViewData["GetSalaryDetails"] = searching;
            var tbl_salary = db.tbl_salary.Include(t => t.tbl_Employee);

            if (!string.IsNullOrEmpty(searching))
            {
                var tbl_salary1 = db.tbl_salary.Where(t => t.tbl_Employee.Emp_Name.Contains(searching));

                return View(tbl_salary1.ToList());
            }
             
            else
                return View(tbl_salary.ToList());



        }

        // GET: Salary/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_salary tbl_salary = db.tbl_salary.Find(id);
            if (tbl_salary == null)
            {
                return HttpNotFound();
            }
            return View(tbl_salary);
        }

        // GET: Salary/Create
        public ActionResult Create()
        {
            ViewBag.Emp_id_fk = new SelectList(db.tbl_Employee, "Emp_ID", "Emp_Name");
            return View();
        }

        // POST: Salary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Emp_id_fk,Amount,Date")] tbl_salary tbl_salary)
        {
            if (ModelState.IsValid)
            {
                db.tbl_salary.Add(tbl_salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Emp_id_fk = new SelectList(db.tbl_Employee, "Emp_ID", "Emp_Name", tbl_salary.Emp_id_fk);
            return View(tbl_salary);
        }

        // GET: Salary/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_salary tbl_salary = db.tbl_salary.Find(id);
            if (tbl_salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.Emp_id_fk = new SelectList(db.tbl_Employee, "Emp_ID", "Emp_Name", tbl_salary.Emp_id_fk);
            return View(tbl_salary);
        }

        // POST: Salary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Emp_id_fk,Amount,Date")] tbl_salary tbl_salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Emp_id_fk = new SelectList(db.tbl_Employee, "Emp_ID", "Emp_Name", tbl_salary.Emp_id_fk);
            return View(tbl_salary);
        }

        // GET: Salary/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_salary tbl_salary = db.tbl_salary.Find(id);
            if (tbl_salary == null)
            {
                return HttpNotFound();
            }
            return View(tbl_salary);
        }

        // POST: Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_salary tbl_salary = db.tbl_salary.Find(id);
            db.tbl_salary.Remove(tbl_salary);
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
