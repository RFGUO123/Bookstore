//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tOrder()
        {
            this.tMember = new HashSet<tMember>();
            this.tOrderDetail = new HashSet<tOrderDetail>();
        }
    
        public int OId { get; set; }
        public int OMemberId { get; set; }
        public string OReceiver { get; set; }
        public string OEmail { get; set; }
        public string OAddress { get; set; }
        public string OPhone { get; set; }
        public Nullable<int> OPrice { get; set; }
        public Nullable<int> OShipPrice { get; set; }
        public Nullable<System.DateTime> ODate { get; set; }
        public bool OAprroved { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tMember> tMember { get; set; }
        public virtual tMember tMember1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tOrderDetail> tOrderDetail { get; set; }
    }
}