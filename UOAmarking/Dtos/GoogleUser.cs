using UOAmarking.Models;

namespace UOAmarking.Dtos
{
    public class GoogleUser
    {
        public int Id { get; set; }
        public string Type { get; set; }

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

        public ICollection<Application> applications { get; set; }

        public ICollection<MarkingHours> remainHours { get; set; }

        public ICollection<Course> courses { get; set; }

    }
}
