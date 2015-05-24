using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolCode { get; set; }
    }
}