namespace Green.API.Models
{
    public class Customer
    {
        public string Username { get; set; } // might be deleted later
        public string? Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
<<<<<<< HEAD
        public string? PhoneNumber { get; set; }
        

        public Customer(){}
        
        public Customer(string username, string password, string email, string name, string address, string phonenumber) 
=======
        public string PhoneNumber { get; set; }


        public Customer() { }

        public Customer(string username, string password, string email, string name, string address, string phonenumber)
>>>>>>> origin/german
        {
            Username = username;
            Password = password;
            Email = email;
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
<<<<<<< HEAD
            Name = name;
                       
=======
           

>>>>>>> origin/german
        }

    }
}