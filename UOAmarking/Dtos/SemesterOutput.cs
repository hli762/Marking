using UOAmarking.Models;

namespace UOAmarking.Dtos
{
    public class SemesterOutput
    {
        public int Year { get; set; }
        public string SemesterType { get; set; }

        public List<CourseOutput> Courses { get; set; }

    }
}
