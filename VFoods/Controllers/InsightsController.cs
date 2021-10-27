using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VFoods.Models;

namespace VFoods.Controllers
{
    public class InsightsController : Controller
    {
        // GET: Insights

        private AquaDBEntities1 db = new AquaDBEntities1();
        public ActionResult Index()
        {
            var tbl_customerdetails = db.tbl_CustomerDetails.Where(x => x.isDelete != true).ToList();

            var tbl_sales = db.tbl_Sales.Where(x => x.isDelete != true && x.Date_of_purchase == DateTime.Today.Date).ToList();

            var tbl_payments = db.tbl_payments.Where(x => x.isDelete != true && x.date == DateTime.Today.Date).ToList();

            SalesOrderViewModel svm = new SalesOrderViewModel();
            svm.Customer = tbl_customerdetails;
            svm.Sales = tbl_sales;
            svm.Payments = tbl_payments;

            return View(svm);
        }

        

        public ActionResult customerreport()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //tbl_CustomerDetails custDetails = db.tbl_CustomerDetails.Find(id);
            //if (custDetails == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }
    }
}