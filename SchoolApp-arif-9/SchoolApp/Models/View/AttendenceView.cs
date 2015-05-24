using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.View
{
    public class AttendenceView
    {
        public string StudentName { get; set; }
        public string StudentRoll { get; set; }
        public int AttendenceStatus { get; set; }
        public DateTime AttendenceDate { get; set; }
    }
}