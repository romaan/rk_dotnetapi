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
using System.Globalization;
using Microsoft.CodeAnalysis;

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

        [TestMethod]
        public void Post_Null_Record_Return_400()
        {
            var mockApiContext = new Mock<ApiContext>();
            var mockToDoItems = new Mock<DbSet<TodoItem>>();

            var controller = new TodoItemsController(mockApiContext.Object);
            BadRequestResult result = (BadRequestResult)controller.Post(null);

            Assert.AreEqual(400, result.StatusCode);
        }


        [TestMethod]
        public void Put_Null_Record_Return_400()
        {
            var mockApiContext = new Mock<ApiContext>();
            var mockToDoItems = new Mock<DbSet<TodoItem>>();

            var controller = new TodoItemsController(mockApiContext.Object);
            BadRequestResult result = (BadRequestResult)controller.Put(1, null);
            Assert.AreEqual(400, result.StatusCode);
        }
        [TestMethod]
        public void Put_NoRecordFound__Return_404()
        {
            var mockApiContext = new Mock<ApiContext>();
            var mockToDoItems = new Mock<DbSet<TodoItem>>();
            var items = new List<TodoItem>
            {
                new TodoItem()
            }.AsQueryable();

            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.Provider).Returns(items.Provider);
            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.Expression).Returns(items.Expression);
            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.ElementType).Returns(items.ElementType);
            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            mockApiContext.Setup(m => m.TodoItems).Returns(mockToDoItems.Object);

            var controller = new TodoItemsController(mockApiContext.Object);
            NotFoundResult result = (NotFoundResult)controller.Put(1, new TodoItem() { Id = 1, Description = "test", IsDone = true, Title = "test" });
            Assert.AreEqual(404, result.StatusCode);

        }

        [TestMethod]
        public void Put_TodoItem__Return_304()
        {
            var mockApiContext = new Mock<ApiContext>();
            var mockToDoItems = new Mock<DbSet<TodoItem>>();
            var items = new List<TodoItem>
            {
                new TodoItem() {Id = 1}
            }.AsQueryable();

            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.Provider).Returns(items.Provider);
            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.Expression).Returns(items.Expression);
            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.ElementType).Returns(items.ElementType);
            mockToDoItems.As<IQueryable<TodoItem>>().Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            mockApiContext.Setup(m => m.TodoItems).Returns(mockToDoItems.Object);

            var controller = new TodoItemsController(mockApiContext.Object);
            NoContentResult result = (NoContentResult)controller.Put(1, new TodoItem() { Id = 1, Description = "test", IsDone = true, Title = "test" });
            Assert.AreEqual(204,result.StatusCode);

        }






    }
}
