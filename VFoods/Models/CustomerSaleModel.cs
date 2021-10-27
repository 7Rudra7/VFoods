using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFoods.Models
{
    public class CustomerSaleModel
    {
        public IQueryable<tbl_CustomerDetails> Customers { get; set; }
        public List<tbl_CustomerDetails> Customer { get; set; }
        public List<tbl_Sales> Sales { get; set; }

        public List<tbl_payments> Payments { get; set; }
        public List<tbl_SaleDetails> SaleOrderDetail { get; set; }
        public IQueryable<tbl_Products> Products { get; set; }
        public tbl_Products Product { get; set; }
    }
}


