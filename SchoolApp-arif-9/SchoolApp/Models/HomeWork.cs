using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class HomeWork
    {
        public int HwId { set; get; }
        [Display(Name = "Subject")]
        [Required]
        public string Title { set; get; }
        public string Description { set; get; }
        public string TeacherName { set; get; }
        [Display(Name = "Class Name")]
        public int ClassId { set; get; }
        public int SectionId { set; get; }
        [Display(Name = "Submission Date")]
        [Required]
        public string SubmissionDate { get; set; }
        public int SchoolId { get; set; }
        public int EmployeeId { get; set; }

    }
}