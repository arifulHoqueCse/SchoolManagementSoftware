using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class Email
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        [Display(Name = "Select Teacher")]
        [Required]
        public string EmployeeCode { get; set; }
        [Display(Name = "Student Reg. Number")]
        [Required]
        public string StudentReg { get; set; }
        public int EmailId { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeIdentity { get; set; }
        public string StudentIdentity { get; set; }
    }
}