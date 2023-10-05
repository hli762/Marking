using UOAmarking.Models;

namespace UOAmarking.Dtos
{
    public class GoogleCourseSupervisor
    {

        public int id { get; set; }
        public string name { get; set; }

        public string email { get; set; }

        public bool isDirector { get; set; }

        public string Type { get; set; }

        public ICollection<Course> courses { get; set; }

    }
}
