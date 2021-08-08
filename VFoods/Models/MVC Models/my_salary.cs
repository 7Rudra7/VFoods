using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFoods.Models.MVC_Models
{
    public class my_salary
    {
        public int id { get; set; }
        public short Emp_id_fk { get; set; }
        public short Amount { get; set; }
        public System.DateTime Date { get; set; }

        public virtual tbl_Employee tbl_Employee { get; set; }
    }
}