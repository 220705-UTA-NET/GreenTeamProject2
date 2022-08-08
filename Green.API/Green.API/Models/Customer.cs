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
        


        
        public Customer(string username, string password, string email, string address, int phonenumber) 
        {
            Username = username;
            Password = password;
            Email = email;
            Address = address;
            PhoneNumber = phonenumber;
                       
        }
        
    }
}
