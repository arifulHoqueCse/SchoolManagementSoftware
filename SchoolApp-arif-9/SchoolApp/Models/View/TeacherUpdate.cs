using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolApp.Models.View
{
    public class TeacherUpdate
    {
        public int TeacherId { set; get; }
        [Required]
        [Display(Name = "Teacher name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string TeacherName { set; get; }
        [Required]
        [Display(Name = "Birthday")]
        public string BirthDay { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string Religion { get; set; }
        [Required]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Required]
        [Display(Name = "Contact Address")]
        public string ContactAddress { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        public string ImagePath { get; set; }
    }
}