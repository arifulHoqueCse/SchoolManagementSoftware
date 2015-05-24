using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class EBook
    {
        public int EbookId { get; set; }
        
        [Display(Name = "E-Book Name")]
        [Required]
        public string Name { get; set; }
         [Display(Name = "Description")]
        public string BookDescription { get; set; }
        public string Author { get; set; }
        
        [Display(Name = "Upload E-Book")]
        [Required]
        public string EbookFileName { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public int SchoolId { get; set; }
    }
}