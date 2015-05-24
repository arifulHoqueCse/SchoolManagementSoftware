using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolApp.Models
{
    public class Library
    {
        public int Id { get; set; }
        public int SchoolId { get; set; }
        [Display(Name = "Student Name")]
        [Required]
        public string StudentName { get; set; }
        [Display(Name = "Roll Number")]
        [Required]
        public string StudentRoll { get; set; }
        [Display(Name = "Registration Number")]
        [Required]
        public string StudentReg { get; set; }
      
        [Required]
        public string Class { get; set; }
        [Display(Name = "Book Name")]
        [Required]
        public string BookName { get; set; }
        [Display(Name = "Author Name")]
        [Required]
        public string AuthorName { get; set; }
       
    }
}