using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleAPI;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Controllers;

namespace SimpleApiTest
{
    [TestClass]
    public class TodoItemsControllerTests
    {
        [TestMethod]
        public void Post_Success_Return_201()
        {
            var mockApiContext = new Mock<ApiContext>();
            var mockToDoItems = new Mock<DbSet<TodoItem>>();
            mockApiContext.Setup(m => m.TodoItems).Returns(mockToDoItems.Object);

            var controller = new TodoItemsController(mockApiContext.Object);
            CreatedAtRouteResult result = (CreatedAtRouteResult)controller.Post(new TodoItem() { Id = 1, Description = "test", IsDone = true, Title = "test" });

            Assert.AreEqual(201, result.StatusCode);
        }

        [TestMethod]
        public void Post_Duplicate_Record_Return_400()
        {
            var mockApiContext = new Mock<ApiContext>();
            var mockToDoItems = new Mock<DbSet<TodoItem>>();

            mockApiContext.Setup(m => m.SaveChanges()).Throws(new ArgumentException());
            mockApiContext.Setup(m => m.TodoItems).Returns(mockToDoItems.Object);

            var controller = new TodoItemsController(mockApiContext.Object);
            ObjectResult result = (ObjectResult)controller.Post(new TodoItem() { Id = 1, Description = "test", IsDone = true, Title = "test" });
            Assert.AreEqual(400, result.StatusCode);
        }



    }
}
