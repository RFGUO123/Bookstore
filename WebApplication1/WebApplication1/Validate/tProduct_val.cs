using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Validate
{
    public class tProduct_val
    {
        public int PId { get; set; }
        [DisplayName("商品名稱")]
        [Required]
        public string PName { get; set; }
        [DisplayName("單價")]
        [Required]
        public int PPrice { get; set; }
        public string PImg { get; set; }
        [DisplayName("商品類別")]
        [Required]
        public string PTypeName { get; set; }
        [DisplayName("剩餘數量")]
        [Required]
        public int PInventory { get; set; }
        public HttpPostedFileBase photo { get; set; }
    }
}