using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class Message_Service
    {
        Repository.Message_Repository mr = new Repository.Message_Repository();
        public void Create_message(int UserId, int PId, string message)
        {
            tMessage_Board message_Board = new tMessage_Board();
            message_Board.MBMId = UserId;
            message_Board.MBPId = PId;
            message_Board.MBMessage = message;
            message_Board.MBDate = DateTime.Now;
            mr.Create_message(message_Board);
        }
    }
}