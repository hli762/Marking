using System;
using System.ComponentModel.DataAnnotations;
using UOAmarking.Models;

namespace UOAmarking.Dtos
{
	public class CourseInput
	{
        [Required]
        public int CourseNumber { get; set; }

        [Required]
        public string CourseName { get; set; }

        public int EstimatedStudents { get; set; }

        public int EnrolledStudents { get; set; }

        public bool NeedsMarker { get; set; }

        public double TotalMarkingHour { get; set; }

        public int SemesterID { get; set; }

        public string CourseCoordinatorEmail { get; set; }

        public string CourseDirectorEmail { get; set; }


       

      

       

        //public string CanPreAssignMarkers { get; set; }

        


    }
}

