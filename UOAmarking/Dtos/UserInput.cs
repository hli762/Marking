using System;
using System.ComponentModel.DataAnnotations;


namespace UOAmarking.Dtos
{
	public class UserInput
	{
        [Required]
        public int Id;

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsOverseas { get; set; }

        public bool IsCitizenOrPR { get; set; }

        public string MarkerEnrolmentDetails { get; set; }

        public string IsPostgraduate { get; set; }

        public float MaxHoursPerWeek { get; set; }
    }
}

