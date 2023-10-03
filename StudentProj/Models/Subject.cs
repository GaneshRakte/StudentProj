namespace StudentProj.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public int Subcode { get; set; }
        public string SubjectName { get; set; }
        public int StudentId { get; set; }

        public Student Student { get; set; }
        
 
    }
}
