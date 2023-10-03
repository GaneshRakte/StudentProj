namespace StudentProj.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string  StudName { get; set; }
        public string Address { get; set;}

        public List<Subject>Subject { get; set; }


    }
}
