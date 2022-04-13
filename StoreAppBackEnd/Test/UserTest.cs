using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Models;

namespace Test;

public class UserTest
{
    [Fact]
    public void UserShouldSetValidUsernames()
    {
        //Arrange
        //I arranged for this test by creating a new user to set the lastname as
        User testUser = new User();

        //Act
        testUser.LastName = "Test LastName";

        //Assert that testUser's title is the thing that we set
        Assert.Equal("Test LastName", testUser.LastName);

    }

    [Fact]
    public void UserShouldSetProducts()
    {
        //Arrange
        Order testOrder = new Order();
        List<Product> products = new List<Product> {
            new Product {
                NameProduct = "Banana"
            },
            new Product {
                NameProduct = "peas"
            }
        };

        //Act
        testOrder.Products = products;

        //Assert
        Assert.NotNull(testOrder.Products);
        Assert.Equal(2, testOrder.Products.Count);
    }
}