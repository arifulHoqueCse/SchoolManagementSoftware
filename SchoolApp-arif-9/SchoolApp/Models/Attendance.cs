using System;

namespace SchoolApp.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int AttendanceStatus { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string ShiftTime { set; get; }
        public int StudentId { get; set; }
        public int  SchoolId { get; set; }
        public int TeacherId { get; set; }
        public int ClassId { get; set; }
        public int  SectionId { get; set; }
    }
}