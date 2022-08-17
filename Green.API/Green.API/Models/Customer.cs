namespace Green.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; } // might be deleted later
        public string? Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        

        public Customer(){}
        
        public Customer(string username, string password, string email, string name, string address, string phonenumber) 
        {
            Username = username;
            Password = password;
            Email = email;
            Address = address;
            PhoneNumber = phonenumber;
            Name = name;
                       
        }

        public Customer(int id, string username, string password, string email, string name, string address, string phonenumber)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Address = address;
            PhoneNumber = phonenumber;
            Name = name;

        }



    }
}
