﻿namespace Green.API.Models
{
    public class Product
    {
<<<<<<< HEAD
        public string Category { get; set; }
        public string Productname { get; set; }
        public string Description { get; set; }
        public string Artistname { get; set; }
        public decimal Unitprice { get; set; }

        public Product(string category, string productname, string description, string artistname, decimal unitprice)
=======
        public string Category { get; set; } 
        public string Productname {get; set; }
        public string Description { get; set; }
        public string Artistname { get; set; }
        public decimal Unitprice { get; set;}

        public Product(string category,string productname, string description, string artistname, decimal unitprice)
>>>>>>> origin/daniel
        {
            Category = category;
            Productname = productname;
            Description = description;
            Artistname = artistname;
            Unitprice = unitprice;
        }
    }
}