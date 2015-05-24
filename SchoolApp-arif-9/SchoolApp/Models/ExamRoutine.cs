using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class ExamRoutine
    {
        public int Id { get; set; }
        public string ExamTitle { get; set; }
        public int ClassId { get; set; }
        public string SubjectName { get; set; }
        public string ExamDate { get; set; }
        public string ExamDay { get; set; }
        public string ExamTime { get; set; }
        public int SchoolId { get; set; }
    }
}