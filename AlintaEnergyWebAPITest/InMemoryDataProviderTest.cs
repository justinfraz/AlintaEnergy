using System;
using System.Linq;
using AlintaEnergyWebAPI.Controllers;
using AlintaEnergyWebAPI.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlintaEnergyWebAPITest
{
    public class InMemoryDataProviderTest
    {
        [Fact]
        public void Task_Add_Without_Relation()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext
            var context = factory.CreateContextForInMemory();

            //Act
            var one = new Customer { FirstName = "John", LastName = "Beasley", DOB = DateTime.Now };
            // var two = new Customer { FirstName = "Mark", LastName = "Adams", DOB = DateTime.Now };
            context.Customers.Add(one);
            context.SaveChanges();

            //Assert  
            //Get the customer count
            var customerCount = context.Customers.Count();
            if (customerCount != 0)
            {
                Assert.Equal(1, customerCount);
            }

            //Get single customer detail
            var customerPost = context.Customers.FirstOrDefault();
            if (customerPost != null)
            {
                Assert.Equal("John", customerPost.FirstName);
            }
        }

        #region Get Customer By Id
        //[Fact]
        //public async void Task_GetPostById_Return_OkResult()
        //{
        //    //Arrange
        //    var factory = new ConnectionFactory();
        //    var context = factory.CreateContextForInMemory();

        //    //Act
        //    var controller = new CustomersController(context);
        //    var Id = 1;
        //    var data = await controller.GetCustomer(Id);

        //    //Assert
        //    Assert.IsType<OkObjectResult>(data);
        //}

        //[Fact]
        //public async void Task_GetPostById_Return_NotFoundResult()
        //{
        //    //Arrange
        //    var factory = new ConnectionFactory();
        //    var context = factory.CreateContextForInMemory();

        //    //Act
        //    var controller = new CustomersController(context);
        //    var Id = 3;
        //    var data = await controller.GetCustomer(Id);

        //    //Assert
        //    Assert.IsType<NotFoundResult>(data);
        //}

        //[Fact]
        //public async void Task_GetPostById_Return_BadRequestResult()
        //{
        //    //Arrange
        //    var factory = new ConnectionFactory();
        //    var context = factory.CreateContextForInMemory();

        //    //Act
        //    var controller = new CustomersController(context);
        //    int? postId = null;

        //    var data = await controller.GetCustomer(postId);

        //    //Assert
        //    Assert.IsType<BadRequestResult>(data);
        //}

        //[Fact]
        //public async void Task_GetPostById_MatchResult()
        //{
        //    //Arrange
        //    var controller = new PostController(repository);
        //    int? postId = 1;

        //    //Act
        //    var data = await controller.GetPost(postId);

        //    //Assert
        //    Assert.IsType<OkObjectResult>(data);

        //    var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
        //    var post = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;

        //    Assert.Equal("Test Title 1", post.Title);
        //    Assert.Equal("Test Description 1", post.Description);
        //}
        #endregion
    }
}
