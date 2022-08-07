namespace Green.API.Models
{
    public class Product
    {
        public Guid Id { get; set; } // item id

        public string? Name { get; set; } // item name
        public decimal Price { get; set; } // item price
        public string? Description { get; set; } // item description
        public string? Artist { get; set; } //artist name 
        public int Quantity { get; set; } //item quantity

        public Product(Guid id, string? name, decimal price, string? description, string? artist, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
            Artist = artist;
            Quantity = quantity;
        }
    }
}
