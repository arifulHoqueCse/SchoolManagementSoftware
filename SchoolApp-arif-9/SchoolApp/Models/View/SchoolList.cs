using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.View
{
    public class SchoolList
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress{ get; set; }
        public string Password { get; set; }
        public string UserCode { get; set; }
        public int  Userlabel { get; set; }
        public int  UserId { get; set; }
    }
}