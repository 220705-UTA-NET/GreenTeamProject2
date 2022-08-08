namespace Green.API.Models
{
    public class Category
    {
        public Guid Id { get; set; } // item id
        public string? Name { get; set; } // category name

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}   
