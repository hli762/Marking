using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UOAmarking.Models
{
    public class Course
    {
        public Course()
        {
            assignments = new List<Assignment>();
            applications = new List<Application>();
            markers = new List<User>();
            CourseSupervisors = new List<CourseSupervisor>();
            remainHours = new List<MarkingHours>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int CourseNumber { get; set; }

        [Required]
        public string CourseName { get; set; }



        public int EstimatedStudents { get; set; }

        public int EnrolledStudents { get; set; }

        public bool NeedsMarker { get; set; }

        public double TotalMarkingHour { get; set; }

        public string Overview { get; set; }


        //navigation
        public Semester Semester { get; set; }
        public ICollection<Assignment> assignments { get; set; }
        public ICollection<Application> applications { get; set; }
        public ICollection<User> markers { get; set; }

        public ICollection<CourseSupervisor>  CourseSupervisors { get; set; }

        public ICollection<MarkingHours> remainHours { get; set; }
   
    }
}