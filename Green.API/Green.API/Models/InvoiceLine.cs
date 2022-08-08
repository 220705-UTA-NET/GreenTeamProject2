namespace Green.API.Models
{
    public class InvoiceLine
    {
        
        public string Productname { get; set; }
        public int Quantity { get; set; }
        public decimal Totalamount { get; set;}

        public InvoiceLine(string productname,int quantity, decimal totalamount)
        {
            Productname = productname;
            Quantity = quantity;
            Totalamount = totalamount;
        }
    }
}
