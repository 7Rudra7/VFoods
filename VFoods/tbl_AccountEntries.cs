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
    
    public partial class tbl_AccountEntries
    {
        public int id { get; set; }
        public short Acc_id_fk { get; set; }
        public decimal Amount { get; set; }
        public bool Type { get; set; }
        public System.DateTime Date { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> isDelete { get; set; }
    
        public virtual tbl_accounts tbl_accounts { get; set; }
    }
}
