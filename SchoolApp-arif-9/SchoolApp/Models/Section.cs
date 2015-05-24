using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class Section
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int SchoolId { get; set; }
    }
}