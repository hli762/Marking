using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UOAmarking.Models
{

	public class Application
	{
		[Key]   
        public int Id { get; set; }

        public bool haveMarkedBefore { get; set; }

        public bool isRecommanded { get; set; }

        public bool haveDoneBefore { get; set; }

        public double previousGrade { get; set; }

        public string haveDoneReleventCourse { get; set; }

        public string currentStatus { get; set; }

        //navigation
        public Course Course { get; set;}

		public User user { get; set; }
    }
}

