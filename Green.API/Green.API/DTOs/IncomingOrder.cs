using Green.API.Models;

namespace Green.API.DTOs
{
    public class IncomingOrder
    {
        public string? CustomerName { get; set; } //get customer name 
        public IEnumerable<Sales>? Products { get; set; } // get products
    }
}
