//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VFoods
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_payments
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public int Customerid_fk { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> isDelete { get; set; }
    
        public virtual tbl_CustomerDetails tbl_CustomerDetails { get; set; }
    }
}
