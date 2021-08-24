using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using VFoods;
using VFoods.Models;
using System.Data.Entity.Validation;
using System.Net;
using PagedList;
using PagedList.Mvc;
namespace VFoods.Controllers
{
    [Authorize]
    public class SalesDController : Controller
    {
        AquaDBEntities1 db = new AquaDBEntities1();
        // GET: SalesD

        public ActionResult customIndex()
        {
            var tbl_Sales = db.tbl_Sales.Where(x => x.isDelete != true && x.isGST==true).ToList();

            return View(tbl_Sales);
        }

        public ActionResult Index()
        {
            var tbl_Sales = db.tbl_Sales.Where(x => x.isDelete != true && x.isGST == true).ToList();


            return View(tbl_Sales);
        }

        public JsonResult GetAllOrders()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<tbl_Sales> lstOrders = new List<tbl_Sales>();
            try
            {
                lstOrders = db.tbl_Sales.Where(a => a.isDelete != true && a.isGST==true).OrderByDescending(a=>a.id).ToList();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(lstOrders, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int orderId)
        {
            List<tbl_SaleDetails> lstOrderDetails = new List<tbl_SaleDetails>();
            try
            {
                lstOrderDetails = db.tbl_SaleDetails.Where(a => a.sales_id_fk == orderId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(lstOrderDetails);
        }



     

        [HttpGet]
        public ActionResult AddNewOrder()
        {
            List<tbl_CustomerDetails> lstCust = new List<tbl_CustomerDetails>();
            List<tbl_Products> lstProd = new List<tbl_Products>();
            List<tbl_Vehicle> lstVehi = new List<tbl_Vehicle>();
            try
            {
                lstCust = db.tbl_CustomerDetails.Where(a => a.Id >0 && a.isDelete!=true).ToList();


                ViewBag.Customers = lstCust;

                lstProd = db.tbl_Products.Where(a => a.Id > 0 && a.isDelete != true).ToList();

                ViewBag.Products = lstProd;

                lstVehi = db.tbl_Vehicle.Where(a => a.Id > 0 && a.isDelete != true).ToList();

                ViewBag.Vehicles = lstVehi;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        [HttpPost]
        public bool SubmitNewOrder(NewOrderModel data)
        {
            Boolean result = false;
            try
            {
                int orderId = 0;
                orderId = AddOrderAndDetails(data);

                if (orderId > 0)
                {
                    result = true;
                }
            }

            
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }



      

        public ActionResult Whatsapp(int orderId)
        {
    

          

            List<tbl_Sales> lstOrderDetails = new List<tbl_Sales>();

            List<tbl_SaleDetails> lstOrderDetails2 = new List<tbl_SaleDetails>();
            lstOrderDetails2 = db.tbl_SaleDetails.Where(a => a.sales_id_fk == orderId).ToList();
            try
            {
                int cust_id_fk = db.tbl_Sales.Where(a => a.id == orderId).FirstOrDefault().customer_id_fk;

                string number = db.tbl_CustomerDetails.Where(a => a.Id == cust_id_fk).FirstOrDefault().Phone_number.ToString();
                lstOrderDetails = db.tbl_Sales.Where(a => a.id == orderId).ToList();

                var totalOutstandingbalance = db.tbl_CustomerDetails.Where(a => a.Id == cust_id_fk).FirstOrDefault().Balance.ToString();
                var dateofpur = lstOrderDetails.FirstOrDefault().Date_of_purchase.ToString();
                var totalBill=  lstOrderDetails.FirstOrDefault().Total_bill.ToString();



                string message = "*VishalAqua*" + "%0a" + "*Udgir*" + "%0a" + "InvoiceNo: " + orderId + "%0a" + "Date of Purchase: " +
                    dateofpur + "%0a" + "Total Bill Amount: " + totalBill + " Rs." + "%0a" + "%0a"
                    + "Total Outstanding balance: " + totalOutstandingbalance + " Rs.";
                sendWhatsApp(number ,message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(lstOrderDetails2);
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
        public int AddOrderAndDetails(NewOrderModel data)
        {
            int orderId = 0;

            try
            {
                 orderId = (int)db.tbl_Sales.Max(a => a.id) + 1;
                var mul = 18.0 / 100;
                var gst = (decimal)mul * data.TotalAmount;
                var Balance1 = db.tbl_CustomerDetails.Where(a => a.Id == data.CustomerID).FirstOrDefault();


                var WithGST = data.TotalAmount / 0.82M;
                var TotalBalance = Balance1.Balance + WithGST;


                if (orderId > 0) {

                    tbl_Sales tt = new tbl_Sales();
                    tbl_SaleDetails td = new tbl_SaleDetails ();
                    tbl_CustomerDetails tc = new tbl_CustomerDetails();
                    

                    tc = db.tbl_CustomerDetails.SingleOrDefault(a => a.Id == data.CustomerID);
                    tc.Balance = TotalBalance;
                    db.SaveChanges();

                    tt.id = orderId;
                    tt.Date_of_purchase = DateTime.Today;
                    tt.Vehicle_no = data.VehicleNum;
                    tt.Transporter_name = data.TransporterName;
                    

                    tt.Total_bill = data.TotalAmount / 0.82M; 
                    tt.Amount_due = data.TotalAmount / 0.82M;
                    tt.Payment_status_comments = data.Comments;
                    tt.customer_id_fk = data.CustomerID;
                    tt.isDelete = false;
                    tt.isGST = true;
                    
                    db.tbl_Sales.Add(tt);
                    int count = data.lstOrderDetails.Count;

                    for (var i=0; i<count;i++)
                    {
                        td.sales_id_fk = orderId;
                        td.Product_id_fk = data.lstOrderDetails[i].Product_id_fk;
                        td.Product_qty = data.lstOrderDetails[i].Product_qty;
                        td.Product_total_price = data.lstOrderDetails[i].Product_total_price;
                        td.isDelete = false;
                        db.tbl_SaleDetails.Add(td);
                        
                        db.SaveChanges();

                    }
                    
                    //tt.tbl_SaleDetails = data.lstOrderDetails;
                    
                    db.SaveChanges();

                }
                


            }
            catch (Exception ex)
            {

                throw ex;
            }
            return orderId;
        }

        public JsonResult GetOrdersDetailsById(int orderId)
        {
            List<tbl_SaleDetails> lstOrdersDetails = new List<tbl_SaleDetails>();
            try
            {
                lstOrdersDetails = db.tbl_SaleDetails.Where(a => a.sales_id_fk == orderId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(lstOrdersDetails, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Edit(int orderId)
        {
            List<tbl_CustomerDetails> lstCust = new List<tbl_CustomerDetails>();
            List<tbl_Products> lstProd = new List<tbl_Products>();

            try
            {
                lstCust  = db.tbl_CustomerDetails.Where(a => a.Id > 0 && a.isDelete != true).ToList();

                ViewBag.Customers = lstCust;

                lstProd  = db.tbl_Products.Where(a => a.Id > 0 && a.isDelete!=true).ToList();

                ViewBag.Products = lstProd;

                TempData["orderId"] = orderId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            List<tbl_CustomerDetails> lstCust = new List<tbl_CustomerDetails>();
            try
            {
                lstCust = db.tbl_CustomerDetails.Where(a => a.Id > 0).ToList();

                ViewBag.Customers = lstCust;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(NewOrderModel data)
        {
          //  Boolean result = false;

            try
            {

                //result = AddNewOrder(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Sales ts = db.tbl_Sales.Find(orderId);
            if (ts == null)
            {
                return HttpNotFound();
            }
            return View(ts);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int orderId)
        {
            tbl_Sales ts = db.tbl_Sales.Find(orderId);
            tbl_SaleDetails tsd = db.tbl_SaleDetails.Find(orderId);


            ts.isDelete = true;
            db.SaveChanges();

            var Balance1 = db.tbl_CustomerDetails.Where(a => a.Id == ts.customer_id_fk).FirstOrDefault();
            var TotalBalance = Balance1.Balance - ts.Total_bill;
            tbl_CustomerDetails tc = new tbl_CustomerDetails();

            tc = db.tbl_CustomerDetails.SingleOrDefault(a => a.Id == ts.customer_id_fk);
            tc.Balance = TotalBalance;
            db.SaveChanges();

            tsd.isDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}