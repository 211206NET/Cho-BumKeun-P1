using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Models;
using CustomExceptions;
using Moq;
using Cho_BumKeun_P1.Controllers;
using BL;

namespace Tests;

public class ControllerTest
{
    [Fact]
    public void ControllerTestShouldWork()
    {
        bool b = true;
        Assert.True(b);
    }

    [Fact]
    public void CustomerControllerShouldGetAllOrdersByUserId()
    {
        var mockBL = new Mock<IBL>();
        int i = 1;
        mockBL.Setup(x => x.GetAllOrders(i)).Returns(
            new List<Order>
            {
                new Order
                {
                    Id = 1,
                    StoreId = 1,
                    StoreName = "Test store one",
                    ProductId = 1,
                    ProductName = "Test product one",
                    Quantity = 1,
                    TotalPrice = 60,
                    UserId = 1
                },
                new Order
                {
                    Id = 2,
                    StoreId = 2,
                    StoreName = "Test store two",
                    ProductId = 2,
                    ProductName = "Test product two",
                    Quantity = 2,
                    TotalPrice = 20,
                    UserId = 2
                }
            }
        );
        var orderCtrllr = new OrderController(mockBL.Object);
        var result = orderCtrllr.GetUserOrder(i);

        Assert.NotNull(result);
        Assert.IsType<List<Order>>(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void StoreControllerShouldGetAllOrdersByStoreId()
    {
        var mockBL = new Mock<IBL>();
        int i = 1;
        mockBL.Setup(x => x.StoreOrders(i)).Returns(
            new List<Order>
            {
                new Order
                {
                    Id = 1,
                    StoreId = 1,
                    StoreName = "Test store one",
                    ProductId = 1,
                    ProductName = "Test product one",
                    Quantity = 1,
                    TotalPrice = 60,
                    UserId = 1
                },
                new Order
                {
                    Id = 2,
                    StoreId = 2,
                    StoreName = "Test store two",
                    ProductId = 2,
                    ProductName = "Test product two",
                    Quantity = 2,
                    TotalPrice = 20,
                    UserId = 2
                }
            }
        );
        var orderCtrllr = new OrderController(mockBL.Object);
        var result = orderCtrllr.GetStoreOrder(i);

        Assert.NotNull(result);
        Assert.IsType<List<Order>>(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void StoreControllerListShouldReturnListOfStores()
    {
        var mockBL = new Mock<IBL>();
        mockBL.Setup(x => x.GetAllStores()).Returns(
            new List<Store>
            {
                new Store
                {
                    Id = 1,
                    Name = "Test One",
                    City = "City One",
                    State = "State One",
                },
                new Store
                {
                    Id = 2,
                    Name = "Test Two",
                    City = "City Two",
                    State = "State Two",
                }
            }
        );
        var stoCntrllr = new StoreController(mockBL.Object);
        var result = stoCntrllr.Get();

        Assert.NotNull(result);
        Assert.IsType<List<Store>>(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void ProductControllerListShouldReturnListOfProducts()
    {
        var mockBL = new Mock<IBL>();
        mockBL.Setup(x => x.GetAllProducts()).Returns(
            new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Title = "Test One",
                    Price = 60,
                    Developer = "Dev One",
                    Inventory = 99
                },
                new Product
                {
                    Id = 2,
                    Title = "Test Two",
                    Price = 20,
                    Developer = "Dev Two",
                    Inventory = 100
                }
            }
        );
        var prodCntrllr = new ProductController(mockBL.Object);
        var result = prodCntrllr.Get();

        Assert.NotNull(result);
        Assert.IsType<List<Product>>(result);
        Assert.Equal(2, result.Count);
    }
}