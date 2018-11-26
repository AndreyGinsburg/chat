using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AjaxChat.Models
{
    public class User
    {
        public bool isThere { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}