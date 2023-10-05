namespace UOAmarking.Dtos
{
    public class CourseOutput
    {
        public int CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }

        public int EstimatedStudents { get; set; }

        public int EnrolledStudents { get; set; }

        public bool NeedsMarker { get; set; }

        public double TotalMarkingHour { get; set; }

    }
}
