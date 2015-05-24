using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class ExamGrade
    {
        public int ExamGradeId { get; set; }
        public string GradeName { get; set; }
        public double GradePoint { get; set; }
        public int MarkFrom { get; set; }
        public int MarkUpTo { get; set; }
        public string Comment { get; set; }
        public int SchoolId { get; set; }
    }
}