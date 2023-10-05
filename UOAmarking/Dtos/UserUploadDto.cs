namespace UOAmarking.Dtos
{
    public class UserUploadDto
    {
        public int userID { get; set; }
        public string name { get; set; }

        public string upi { get; set; }

        public string email { get; set; }

        public int auid { get; set; }

        public bool isOverseas { get; set; }

        public bool isCitizenOrPermanent { get; set; }

        public string enrolmentDetail { get; set; }

        public string underOrPost { get; set; }

        public bool haveOtherContracts { get; set; }

        public string descriptionOfContracts { get; set; }

        public IFormFile cv { get; set; }

        public IFormFile academicRecord { get; set; }
    }
}
