using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string UserCode { set; get; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int AdminLavel { get; set; }

        public int SchoolId { get; set; }
    }
}