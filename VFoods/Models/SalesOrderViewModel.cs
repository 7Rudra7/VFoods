using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFoods.Models
{
    public class SalesOrderViewModel
    {
        public IQueryable<tbl_CustomerDetails> Customers { get; set; }
        public tbl_CustomerDetails Customer { get; set; }
        public tbl_Sales SaleOrder { get; set; }
        public List<tbl_SaleDetails> SaleOrderDetail { get; set; }
        public IQueryable<tbl_Products> Products { get; set; }
        public tbl_Products Product { get; set; }
    }
}