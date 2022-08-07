namespace Green.API.Models
{
    public class Customer
    {
        public string? Username { get; set; } // might be deleted later
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int PhoneNumber { get; set; }
        
        public string? BirthDate { get; set; }
        public string? MemberSince { get; set; }
        
    }
}
