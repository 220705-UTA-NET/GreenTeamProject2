namespace Green.API.Models
{
    public class Sales
    {
        public int InvoiceNumber { get; set; }
        public Guid Id { get; set; } // item id
        public string? DateSold { get; set; }

        public List<Product> Prodocts { get; set; } = new List<Product>(); // items ordered by customer

        public decimal Total => Prodocts.Sum(n => n.Quantity * n.Price);


    }
}
