using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VFoods.Models
{
    public class NewOrderModel
    {

        public int OrderID { get; set; }

        public List<tbl_SaleDetails> lstOrderDetails { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [Required]
        public short VehicleNum { get; set; }
        [Required]
        public int TotalQty { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public string TransporterName { get; set; }

      
        [Required]
        public string Comments { get; set; }
    }
}


