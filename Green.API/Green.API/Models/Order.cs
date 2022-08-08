namespace Green.API.Models
{
    public class Order
    {
        public int Number { get; set; }
        public DateTime Date { get; set; } // go back and change certain fields to datetime 

        public Order(int number, DateTime date)
        {
            Number = number;
            Date = date;
        }
    }
}
