using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Validate
{
    public class tMember_Modify_Password_val
    {
        [DisplayName("舊密碼")]
        [Required]
        public string MOld_Password { get; set; }
        [DisplayName("新密碼")]
        [Required]
        public string MPassword { get; set; }
        [DisplayName("再次輸入新密碼")]
        [Required]
        [Compare("MPassword", ErrorMessage = "輸入結果與密碼不同")]
        public string MPassword2 { get; set; }        
    }
}