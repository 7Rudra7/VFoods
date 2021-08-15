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
    public class CustomerController : Controller
    {
        private AquaDBEntities1 db = new AquaDBEntities1();

        // GET: Customer
        //public ActionResult Index()
        //{
        //    return View(db.tbl_CustomerDetails.ToList());
        //}

        //searching customer
        public ActionResult Index(string searching)
        {
            ViewData["GetCustomerDetails"] = searching;
            var tbl_CustomerDetails = db.tbl_CustomerDetails.ToList();

            if (!string.IsNullOrEmpty(searching))
            {
                var tbl_CustomerDetails1 = db.tbl_CustomerDetails.Where(x=> x.Customer_name.Contains(searching) || x.Account_name.Contains(searching) || x.City_Town.Contains(searching));

                
                if (tbl_CustomerDetails1.Count() == 0)
                {
                    ViewBag.search = "No records found";
                    return View(tbl_CustomerDetails.ToList());
                }
                else
                {
                    ViewBag.search = "Search results";
                    return View(tbl_CustomerDetails1.ToList()); }
                
            }

            else
                return View(tbl_CustomerDetails.ToList());



        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_CustomerDetails tbl_CustomerDetails = db.tbl_CustomerDetails.Find(id);
            if (tbl_CustomerDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_CustomerDetails);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account_name,Customer_name,Phone_number,Address,State,City_Town,Balance")] tbl_CustomerDetails tbl_CustomerDetails)
        {
            if (ModelState.IsValid)
            {
                db.tbl_CustomerDetails.Add(tbl_CustomerDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_CustomerDetails);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_CustomerDetails tbl_CustomerDetails = db.tbl_CustomerDetails.Find(id);
            if (tbl_CustomerDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_CustomerDetails);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account_name,Customer_name,Phone_number,Address,State,City_Town,Balance")] tbl_CustomerDetails tbl_CustomerDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_CustomerDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_CustomerDetails);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_CustomerDetails tbl_CustomerDetails = db.tbl_CustomerDetails.Find(id);
            if (tbl_CustomerDetails == null)
            {
                return HttpNotFound();
            }
            return View(tbl_CustomerDetails);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_CustomerDetails tbl_CustomerDetails = db.tbl_CustomerDetails.Find(id);
            db.tbl_CustomerDetails.Remove(tbl_CustomerDetails);
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

        public ActionResult WhatsappReminder(int? id)
        {


            tbl_CustomerDetails custDet = db.tbl_CustomerDetails.Find(id);

            

            //List<tbl_Sales> lstOrderDetails = new List<tbl_Sales>();

            //List<tbl_SaleDetails> lstOrderDetails2 = new List<tbl_SaleDetails>();
            //lstOrderDetails2 = db.tbl_SaleDetails.Where(a => a.sales_id_fk == orderId).ToList();
            try
            {
                

                string number = db.tbl_CustomerDetails.Where(a => a.Id == id).FirstOrDefault().Phone_number.ToString();
                

                var totalOutstandingbalance = db.tbl_CustomerDetails.Where(a => a.Id == id).FirstOrDefault().Balance.ToString();
                



                string message = "*VishalAqua*" + "%0a" + "*Udgir*" + "%0a" + "%0a" + "Hi Customer , Our records show that we haven’t yet received payment which is overdue. " +
                    "I would appreciate if you could check" +
                    " this out on your end. " + "%0a" + "%0a" +  "*Total Outstanding balance:* " + totalOutstandingbalance + " Rs.";
                sendWhatsApp(number, message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(custDet);
        }

        private void sendWhatsApp(string number, string message)

        {
            try

            {

                if (number == "")

                {

                    // MessageBox.Show("No number added");

                }

                if (number.Length <= 10)

                {

                    //MessageBox.Show("Inidan Code added automatically");

                    number = "+91" + number;

                }

                number = number.Replace(" ", "");



                System.Diagnostics.Process.Start("http://api.whatsapp.com/send?phone=" + number + "&text=" + message);


            }

            catch (Exception ex)

            {
                throw ex;
            }

        }
    }
}
