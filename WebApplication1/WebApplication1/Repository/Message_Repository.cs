using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class Message_Repository
    {
        ShoppingdatabaseEntities1 db = new ShoppingdatabaseEntities1();
        public void Create_message(tMessage_Board message_Board)
        {
            db.tMessage_Board.Add(message_Board);
            db.SaveChanges();
        }
        public List<tMessage_Board> Select_Message_By_PId(int PId)
        {
            return db.tMessage_Board.Where(m => m.MBPId == PId).OrderByDescending(m => m.MBId).ToList();
        }
        public List<tMessage_Board> Select_Message_By_PId_Page_Take(int PId, int page, int take)
        {
            return db.tMessage_Board.Where(m => m.MBPId == PId).OrderByDescending(m => m.MBId).Skip((page - 1) * take).Take(take).ToList();
        }
    }
}