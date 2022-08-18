using System;

namespace Green.API.Models
{
    public class SalesInvoice
    {
        public string Name { get; set;}
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Invoicedate { get; set; }
        public string Paymenttype { get; set; }
        public decimal Totalamount {  get; set; }

        public SalesInvoice(string name, string email, string address, DateTime invoicedate, string paymenttype, decimal totalamount)
        {
            Name = name;
            Email = email;
            Address = address;
            Invoicedate = invoicedate;
            Paymenttype = paymenttype;
            Totalamount = totalamount;
            
        }
      
       


    }
}
