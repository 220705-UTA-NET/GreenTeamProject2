namespace Green.API.Models
{
    public class Payment
    {
       
            public Guid Id { get; set; } // item id
            public ProductType Type { get; set; } // item type
            public Payment(Guid id, ProductType type)
            {
                Id = id;
                Type = type;
            }
    }
}
