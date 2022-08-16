namespace Green.API.Models
{
    public class InvoiceLine
    {

        public int InvoiceNumber { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }

        public InvoiceLine(int invoicenumber ,int productid, int quantity, decimal amount)
        {
            InvoiceNumber = invoicenumber;
            ProductId = productid;
            Quantity = quantity;
            Amount = amount;
        }
    }
}