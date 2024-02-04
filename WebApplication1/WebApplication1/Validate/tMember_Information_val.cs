using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Validate
{
    public class tMember_Information_val
    {
        public int MId { get; set; }
        [DisplayName("帳號")]
        public string MUserId { get; set; }
        [DisplayName("密碼")]
        public string MPassword { get; set; }
        [DisplayName("再次輸入密碼")]
        public string MPassword2 { get; set; }
        [DisplayName("信箱")]
        [Required(ErrorMessage = "信箱欄位不可空白")]
        [EmailAddress]
        public string MEmail { get; set; }
        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名欄位不可空白")]
        public string MName { get; set; }
        [DisplayName("電話號碼")]
        [Required(ErrorMessage = "電話欄位不可空白")]
        [Phone]
        public string MPhone { get; set; }
        public string MResetPassword { get; set; }
        public bool MActivate { get; set; }
        public string MOauthTypeId { get; set; }
        public string MOauthId { get; set; }
        public Nullable<int> MCartOrderId { get; set; }
    }
}