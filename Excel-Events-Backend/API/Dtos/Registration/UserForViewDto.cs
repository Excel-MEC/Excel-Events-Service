namespace API.Dtos.Registration
{
    public class UserForViewDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string picture { get; set; }
        public string qrCodeUrl { get; set; }
        public int? institutionId { get; set; }
        public string gender { get; set; }
        public string mobileNumber { get; set; }
        public int categoryId { get; set; }
        public string category { get; set; }
        public bool isPaid { get; set; }
    }
}