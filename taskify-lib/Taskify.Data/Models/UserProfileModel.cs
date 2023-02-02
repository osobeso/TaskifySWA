namespace Taskify.Data.Models
{
    public class UserProfileModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
    }
}
