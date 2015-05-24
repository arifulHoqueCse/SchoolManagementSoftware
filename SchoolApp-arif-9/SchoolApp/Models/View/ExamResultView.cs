using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.View
{
    public class ExamResultView
    {
        public double MarkObtain { get; set; }
        public double MarkTotal { get; set; }
        public string Attendence { get; set; }
        public string SubjectName { get; set; }
    }
}