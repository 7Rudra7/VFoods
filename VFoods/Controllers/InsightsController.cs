using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VFoods.Controllers
{
    public class InsightsController : Controller
    {
        // GET: Insights

        private AquaDBEntities1 db = new AquaDBEntities1();
        public ActionResult Index()
        {
            var tbl_customerdetails = db.tbl_CustomerDetails.Where(x => x.isDelete != true).ToList();

            
            return View(tbl_customerdetails);
        }
    }
}