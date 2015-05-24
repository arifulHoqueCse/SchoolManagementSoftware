using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolApp.Models.View
{
    public class EditRoutine
    {
        public int RoutineId { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { set; get; }
        public string ClassDay { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}