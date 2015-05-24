using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.View
{
    public class HomeWorkView
    {
        public int HwId { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string TeacherName { set; get; }
        public string ClassName { set; get; }
        public String SectionName { set; get; }
        public string SubmissionDate { get; set; }
       
        public int EmployeeName { get; set; }
    }
}