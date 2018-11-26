using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AjaxChat.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("Connection") { }
        public DbSet<User> Users { get; set; }
    }

}