using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Display(Name = "Book Name")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Book Description")]
        public string BookDescription { get; set; }
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
         [Display(Name = "Book Status")]
        public int BookStatus { get; set; }
        public string Price { get; set; }
         [Display(Name = "SSBN Number")]
        public string Ssbn { get; set; }
        public int  ClassId { get; set; }
        public int SchoolId { get; set; }
    }
}