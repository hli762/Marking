using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace UOAmarking.Models
{
    public class Semester
    {
        public Semester()
        {
            Courses = new List<Course>();
        }

        [Key]
        public int Id { get; set; }
        public int Year { get; set; }
        public string SemesterType { get; set; }

        //navigation
        public ICollection<Course> Courses { get; set; }
    }
}