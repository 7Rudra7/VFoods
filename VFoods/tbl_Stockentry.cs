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
    
    public partial class tbl_Stockentry
    {
        public int id { get; set; }
        public int Pro_id_fk { get; set; }
        public System.DateTime Date { get; set; }
        public string qty { get; set; }
        public Nullable<bool> isDelete { get; set; }
    
        public virtual tbl_Products tbl_Products { get; set; }
    }
}
