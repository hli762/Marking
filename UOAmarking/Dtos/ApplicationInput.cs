using System;
namespace UOAmarking.Dtos
{
	public class ApplicationInput
	{
        public int StudentID { get; set; }

        public int CourseID { get; set; }

        public string GradeObtained { get; set; }

        public string QualificationsExplanation { get; set; }

        public string PreviousExperience { get; set; }

        public string currentStatus { get; set; }
    }
}

