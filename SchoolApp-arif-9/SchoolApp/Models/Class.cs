using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        [Display(Name = "Class Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Class Numeric Value")]
        [Required]
        public string NameNumeric { get; set; }
        public int TeacherId { get; set; }
        public int  SchoolId { get; set; }
        public int SectionId { get; set; }
    }
}