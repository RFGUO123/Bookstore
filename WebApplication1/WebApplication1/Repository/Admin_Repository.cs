using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class Admin_Repository
    {
        ShoppingdatabaseEntities1 db = new ShoppingdatabaseEntities1();
        public tAdministrator Check_Admin_Login(string AUserId, string APassword)
        {
            return db.tAdministrator.Where(m => m.AUserId == AUserId && m.APassword == APassword).FirstOrDefault();
        }
    }
}