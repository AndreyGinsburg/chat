using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxChat.Models
{
    public class ChatModel
    {
        public List<ChatMessage> Messages;
        public List<User> Users;

        public ChatModel()
        {
            Messages = new List<ChatMessage>();
            Users = new List<User>();
        }
    }
    public class ChatMessage
    {
        public User User;
        public DateTime Date = DateTime.Now;
        public string Text = "";
    }
}