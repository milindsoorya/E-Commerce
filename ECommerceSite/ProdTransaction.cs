//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECommerceSite
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProdTransaction
    {
        public int Transactionid { get; set; }
        public Nullable<int> cid { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
    
        public virtual CartTransaction CartTransaction { get; set; }
    }
}