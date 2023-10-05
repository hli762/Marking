using System.ComponentModel.DataAnnotations;

namespace UOAmarking.Models
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string assignmentType { get; set; }

        public string description { get; set; }


        //navigation
        public Course Course { get; set; }



    }
}
