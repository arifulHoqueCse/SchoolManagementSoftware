using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class ClassRoutine
    {
        public int RoutineId { get; set; }
         [Required]
        [Display(Name = "Start Time")]
        public string TimeStart { get; set; }
        [Display(Name = "End Time")]
        [Required]
        public string TimeEnd { get; set; }
        [Display(Name = "Day")]
        public string ClassDay { get; set; }
        [Display(Name = "Class")]
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public string StartAmPm { set; get; }
        public string EndAmPm { set; get; }
        public string FullStartTime { set; get; }
        public string FullEndTime { set; get; }
        public int SchoolId { get; set; }
    }
}