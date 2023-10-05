using System.ComponentModel.DataAnnotations;

namespace UOAmarking.Models
{
    public class CourseSupervisor
    {
        public CourseSupervisor()
        {
           courses = new List<Course>();
        }

        [Key]
        public int Id { get; set; }

        [Required] public string email { get; set; }

        public string name { get; set; }

        public bool isDirector { get; set; }



        //navigation
        public ICollection<Course> courses { get; set; }

        
    }
}
