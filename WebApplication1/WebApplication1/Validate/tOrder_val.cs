using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Validate
{
    public class tOrder_val
    {
        public int OId { get; set; }
        public int OMemberId { get; set; }
        [DisplayName("收件人姓名")]
        [Required(ErrorMessage = "收件人欄位不可空白")]
        public string OReceiver { get; set; }
        [DisplayName("收件人信箱")]
        [Required(ErrorMessage = "信箱欄位不可空白")]
        [EmailAddress(ErrorMessage = "信箱格式錯誤")]
        public string OEmail { get; set; }
        [DisplayName("收件人地址")]
        [Required(ErrorMessage = "地址欄位不可空白")]
        public string OAddress { get; set; }
        [DisplayName("收件人電話")]
        [Required(ErrorMessage = "電話欄位不可空白")]
        [Phone(ErrorMessage = "電話格式錯誤")]
        public string OPhone { get; set; }
        public Nullable<int> OPrice { get; set; }
        public Nullable<int> OShipPrice { get; set; }
        public Nullable<System.DateTime> ODate { get; set; }
        public bool OAprroved { get; set; }
    }
}