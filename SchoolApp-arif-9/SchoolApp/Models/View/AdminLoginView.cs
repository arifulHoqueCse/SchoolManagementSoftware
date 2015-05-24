using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.View
{
    public class AdminLoginView
    {
        public int SchoolId { get; set; }
        public string SchoolName { set; get; }
        public string SchoolAddress { set; get; }
        public string SchoolCode { set; get; }
        public string UserName { set; get; }
        public int UserId { get; set; }
        public int  UserLevel { get; set; }
    }
}