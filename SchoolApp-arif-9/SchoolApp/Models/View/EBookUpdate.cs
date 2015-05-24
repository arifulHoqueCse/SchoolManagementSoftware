using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.View
{
    public class EBookUpdate
    {
        public int EbookId { get; set; }
        public string Name { get; set; }
        public string BookDescription { get; set; }
        public string Author { get; set; }
        public string EbookFileName { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public int SchoolId { get; set; }
    }
}