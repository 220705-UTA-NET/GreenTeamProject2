namespace Green.API.Models
{
    public class Sales
    {
        public int InvoiceNumber { get; set; }
        public Guid Id { get; set; } // item id
        public DateTime DateSold { get; set; }

        public List<Product> Products { get; set; } = new List<Product>(); // items ordered by customer

        public decimal Total => Products.Sum(n => n.Quantity * n.Price);

        //public decimal Tax => Total * 0.13m;

        public Sales(int invoicenumer, Guid id, DateTime datesold, List<Product> products)
        {
            InvoiceNumber = invoicenumer;
            Id = id;
            DateSold = datesold;
            Products = products;
            
        }
      
       


    }
}
