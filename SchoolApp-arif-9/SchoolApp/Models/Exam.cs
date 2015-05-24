using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        [Display(Name = "Exam Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Exam Date")]
        [Required]
        public string ExamDate { get; set; }
        public string Comment { get; set; }
        public int SchoolId { get; set; }

    }
}