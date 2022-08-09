namespace Green.API.Models
{
    public class Customer
    {
        public string? Username { get; set; } // might be deleted later
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; }


        public Customer() { }

        public Customer(string username, string password, string email, string name, string address, string phonenumber)
        {
            Username = username;
            Password = password;
            Email = email;
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
           

        }

    }
}