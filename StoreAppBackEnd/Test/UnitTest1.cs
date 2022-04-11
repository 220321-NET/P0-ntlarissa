using Xunit;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using Models;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //Arrange
        //I arranged for this test by creating a new user to set the lastname as
        User testUser = new User();

        //Act
        testUser.LastName = "Test LastName";

        //Assert that testUser's title is the thing that we set
        Assert.Equal("Test LastName", testUser.LastName);

    }
}