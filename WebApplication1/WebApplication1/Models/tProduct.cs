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
    
    public partial class tProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tProduct()
        {
            this.tMessage_Board = new HashSet<tMessage_Board>();
            this.tOrderDetail = new HashSet<tOrderDetail>();
        }
    
        public int PId { get; set; }
        public string PName { get; set; }
        public int PPrice { get; set; }
        public string PImg { get; set; }
        public int PTypeId { get; set; }
        public string POwner { get; set; }
        public int PInventory { get; set; }
        public bool PAvailable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tMessage_Board> tMessage_Board { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tOrderDetail> tOrderDetail { get; set; }
        public virtual tProductType tProductType { get; set; }
    }
}
