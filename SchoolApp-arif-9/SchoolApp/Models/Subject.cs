namespace SchoolApp.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
        public int SchoolId { get; set; }
    }
}