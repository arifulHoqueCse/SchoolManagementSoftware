namespace SchoolApp.Models
{
    public class Mark
    {
        public int MarkId { set; get; }
        public int StudentId { set; get; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int ExamId { get; set; }
        public double MarkObtained { get; set; }
        public double MarkTotal { get; set; }
        public string Attendence { get; set; }
        public string Comment { get; set; }
        public int SchoolId { set; get; }
    }
}