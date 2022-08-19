using Green.Api.Data;
using Green.API.Controllers;
using Green.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SlackAPI.WebSocketMessages;
using System.Net;

namespace Controller.Test
{
    public class SalesManangementTest

    {
        Mock<IRepository> MockRepo = new();

        Mock<ILogger<SalesManagementController>> _logger = new();


        [Fact]
        public void GetProductsPass()
        {
            //Arrange

            IEnumerable<Product> products = new List<Product>();


            MockRepo.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            var controller = new SalesManagementController(MockRepo.Object, _logger.Object);


            // Act

            var result = controller.GetAllProducts();


            // Assert
            Assert.NotNull(result);

        }
        [Fact]
        public void GetProduct()
        {
            //Arrange



            Product products = new Product();



            int productid = 1;
            MockRepo.Setup(x => x.GetAProductAsync(productid)).ReturnsAsync(products);

            var controller = new SalesManagementController(MockRepo.Object, _logger.Object);


            // Act

            var result = controller.GetAllProducts(1);


            // Assert
            Assert.NotNull(result);

        }

        [Fact]

        public void GetAllSalesInvoices()
        {
            //Arrange


            IEnumerable<SalesInvoice> salesinvoices = new List<SalesInvoice>();



            MockRepo.Setup(x => x.GetAllSalesInvoicesAsync()).ReturnsAsync(salesinvoices);

            var controller = new SalesManagementController(MockRepo.Object, _logger.Object);


            // Act

            var result = controller.GetAllSalesInvoices();


            // Assert
            Assert.NotNull(result);

        }


        [Fact]

        public void PostSalesInvoices()
        {
            //Arrange

            var time = DateTime.Now;
            int customerid = 35;
            string paymenttype = "bottle caps";
            decimal totalamount = 203.23M;

            //StatusCodeResult pablo = HttpStatusCode.OK;


            //MockRepo.Setup(x => x.InsertSalesInvoiceAsync(time, customerid, paymenttype, totalamount)).ReturnsAsync();

            var controller = new SalesManagementController(MockRepo.Object, _logger.Object);


            // Act

            var result = controller.PostSalesInvoice(time, customerid, paymenttype, totalamount);


            // Assert
            Assert.NotNull(result);

        }

        [Fact]

        public void PostInvoiceLine()
        {
            //Arrange
            var time = DateTime.Now;

            InvoiceLine invoice = null;


            //StatusCodeResult pablo = HttpStatusCode.OK;


            //MockRepo.Setup(x => x.InsertSalesInvoiceAsync(time, customerid, paymenttype, totalamount)).ReturnsAsync();

            var controller = new SalesManagementController(MockRepo.Object, _logger.Object);


            // Act

            var result = controller.PostInvoiceLine(time, invoice);


            // Assert
            Assert.NotNull(result);

        }

        [Fact]


        public void GetProductsOfCategory()
        {

            //Arrange



            Product products = new Product();



            string category = "nft";
            //MockRepo.Setup(x => x.GetProductsOfCategoryAsync(category)).ReturnsAsync(products);

            var controller = new SalesManagementController(MockRepo.Object, _logger.Object);


            // Act

            var result = controller.GetProductsOfCategory(category);


            // Assert
            Assert.NotNull(result);




        }


    }
}