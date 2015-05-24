using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [Display(Name = "Student name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string StudentName {get; set;}
         [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Mother Name")]
        public string MotherName { get; set; }
        [Display(Name = "Registration Number")]
        public string RegNo { get; set; }
        [Display(Name = "Roll Number")]
        public string RollNumber { get; set; }
        public string Birthday { get; set; }
       
        [Display(Name = "Gender")]
        public string Sex { get; set; }
        [Required]
        public string Religion { get; set; }
         [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
        [Display(Name = "Contact Address")]
        [Required]
        public string ContactAddress { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
       
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Display(Name = "Upload Image")]
        [StringLength(100, ErrorMessage = "Please upload image")]
        [Required]
        public string ImagePath { get; set; }
        [Display(Name = "Class")]
        public int ClassId { get; set; }

        public int SectionId { get; set; }
        public int SchoolId { get; set; }
    }
}