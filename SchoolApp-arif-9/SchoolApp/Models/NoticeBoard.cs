using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class NoticeBoard
    {
        public int NoticeId { get; set; }
        [Display(Name = "Notice Title")]
        [Required]
        public string NoticeTitle { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string NoticeDescription { get; set; }
        [Display(Name = "Time")]
        public string NoticeTimeStamp { set; get; }
        public int SchoolId { get; set; }
    }
}