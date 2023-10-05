using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace UOAmarking.Models
{
    public class MarkingHours
    {
        [Key]
        public int Id { get; set; }

        public Course course { get; set; }

        public User user { get; set; }

        public double remainHour { get; set; }

    }
}
