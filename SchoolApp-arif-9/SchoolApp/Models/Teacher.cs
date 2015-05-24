using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolApp.Models
{
    public class Teacher
    {
        public int TeacherId { set; get; }
        [Required]
        [Display(Name = "Teacher name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string TeacherName { set; get; }
        [Required]
        [Display(Name = "Birthday")]
        public string BirthDay { get; set; }
          [Display(Name = "Gender")]
        [Required]
        public string Sex { get; set; }
        [Required]
        public string Religion { get; set; }
     
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
        [Remote("FindDuplicateEmail", "Teacher", HttpMethod = "POST", ErrorMessage = "Email already exists. Please enter a different email.")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Employee Id")]
        public string EmployeeCode { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [StringLength(100, ErrorMessage = "Please upload image")]
         [Required]
        public string ImagePath { get; set; }

        public int SchoolId { get; set; }
    }
}