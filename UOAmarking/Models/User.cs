using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UOAmarking.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string UPI { get; set; }

        public string Email { get; set; }

        public int AUID { get; set; }

        public bool isOverseas { get; set; }

        public bool isCitizenOrPermanent { get; set; }

        public string enrolmentDetail { get; set; }

        public string UnderOrPost { get; set; }

        public bool haveOtherContracts { get; set; }

        public string descriptionOfContracts { get; set; }

        public byte[] CV { get; set; }

        public byte[] AcademicRecord { get; set; }


        //navigation
        public ICollection<Application> applications { get; set; }

        public ICollection<MarkingHours> remainHours { get; set; }

        public ICollection<Course> courses { get; set; }
    }
}