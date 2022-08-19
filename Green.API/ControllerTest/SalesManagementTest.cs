using Green.Api.Data;
using Green.API.Controllers;
using Green.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SlackAPI.WebSocketMessages;
using System.Net;

public class SalesManangementTest

{
    Mock<IRepository> MockRepo = new();

    Mock<ILogger<SalesManagementController>> MockLogger = new();


    [Fact]
    public void GetProductsPass()
    {
        //Arrange

        IEnumerable<Product> products = new List<Product>();
        MockRepo.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);


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

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);


        // Act

        var result = controller.GetAllProducts();


        // Assert
        Assert.NotNull(result);

    }

    [Fact]

    public void GetAllSalesInvoices()
    {
        //Arrange


        IEnumerable<SalesInvoice> salesinvoices = new List<SalesInvoice>();



        MockRepo.Setup(x => x.GetAllSalesInvoicesAsync()).ReturnsAsync(salesinvoices);

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);


        // Act

        var result = controller.GetAllSalesInvoices();


        // Assert
        Assert.NotNull(result);

    }


    [Fact]
    public async Task PostSalesInvoices_HappyPath()
    {
        //Arrange
        var time = DateTime.Now;
        int customerid = 35;
        string paymenttype = "bottle caps";
        decimal totalamount = 203.23M;

        MockRepo.Setup(x =>
            x.InsertSalesInvoiceAsync(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>())
        ).ReturnsAsync(new StatusCodeResult(200));

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);

        // Act
        var result = await controller.PostSalesInvoice(time, customerid, paymenttype, totalamount);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<StatusCodeResult>(result);

        var objectResult = result as StatusCodeResult;

        Assert.Equal(200, objectResult?.StatusCode);
    }

    [Fact]
    public async Task PostSalesInvoices_WhenRepoReturns500()
    {
        // Arrange
        MockRepo.Setup(x =>
            x.InsertSalesInvoiceAsync(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>())
        ).ReturnsAsync(new StatusCodeResult(500));

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);

        // Act
        var result = await controller.PostSalesInvoice(default, default, default, default);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ObjectResult>(result);

        var objectResult = result as ObjectResult;

        Assert.Equal(500, objectResult?.StatusCode);
    }

    [Fact]
    public async Task PostSalesInvoices_WhenRepoThrowException()
    {
        // Arrange
        var expectedException = new Exception("Test");

        MockRepo.Setup(x =>
            x.InsertSalesInvoiceAsync(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<decimal>())
        ).ThrowsAsync(expectedException);

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);

        // Act
        var result = await controller.PostSalesInvoice(default, default, default, default);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ObjectResult>(result);

        var objectResult = result as ObjectResult;

        Assert.Equal(500, objectResult?.StatusCode);
        Assert.Equal("SalesInvoice could not be inserted!", objectResult?.Value);

        MockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

        MockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == expectedException.Message),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
    }

    [Fact]
    public async Task PostInvoiceLine_HappyPath()
    {

        MockRepo.Setup(x =>
            x.InsertInvoiceLineAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>())
        ).ReturnsAsync(new StatusCodeResult(200));

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);



        // Act
        var result = await controller.PostInvoiceLine(new InvoiceLine(default, default, default, default));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<StatusCodeResult>(result);

        var objectResult = result as StatusCodeResult;

        Assert.Equal(200, objectResult?.StatusCode);
    }

    [Fact]
    public async Task PostInvoiceLine_WhenRepoReturns500()
    {

        // Arrange
        MockRepo.Setup(x =>
           x.InsertInvoiceLineAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>())
       ).ReturnsAsync(new StatusCodeResult(500));

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);

        // Act
        var result = await controller.PostInvoiceLine(new InvoiceLine(default, default, default, default));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ObjectResult>(result);

        var objectResult = result as ObjectResult;

        Assert.Equal(500, objectResult?.StatusCode);
    }
    [Fact]
    public async Task PostInvoiceLine_WhenException()
    {

        // Arrange
        var expectedException = new Exception("Test");

        MockRepo.Setup(x =>
            x.InsertInvoiceLineAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(),
                It.IsAny<decimal>())
        ).ThrowsAsync(expectedException);

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);

        // Act
        var result = await controller.PostInvoiceLine(new InvoiceLine(default, default, default, default));

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ObjectResult>(result);

        var objectResult = result as ObjectResult;

        Assert.Equal(500, objectResult?.StatusCode);
        Assert.Equal("InvoiceLine could not be inserted!", objectResult?.Value);

        MockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

        MockLogger.Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == expectedException.Message),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
    }




    [Fact]
    public void GetProductsOfCategory()
    {

        //Arrange



        Product products = new Product();



        string category = "nft";
        //MockRepo.Setup(x => x.GetProductsOfCategoryAsync(category)).ReturnsAsync(products);

        var controller = new SalesManagementController(MockRepo.Object, MockLogger.Object);


        // Act

        var result = controller.GetProductsOfCategory(category);


        // Assert
        Assert.NotNull(result);




    }
}


    