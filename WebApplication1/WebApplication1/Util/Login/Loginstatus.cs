using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Util.Login
{
    public class Loginstatus
    {
        private String member_uid;
        private String seller_sid;

        public Loginstatus() {
            this.member_uid = null;
            this.seller_sid = null;
        }


        public String get_uid()
        {
            return this.member_uid;
        }

        public String get_sid()
        {
            return this.seller_sid;
        }

        public void set_uid(String uid)
        {
            this.member_uid = uid;
        }

        public void set_sid(String sid)
        {
           this.seller_sid= sid;
        }

        public void clear_uid()
        {
            this.member_uid = null;
        }

        public void clear_sid()
        {
            this.seller_sid = null;
        }




    }
}