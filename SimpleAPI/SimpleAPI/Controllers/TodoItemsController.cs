using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    public class TodoItemsController : Controller
    {

        private ApiContext _apiContext;

        public TodoItemsController(ApiContext apiContext) {
            _apiContext = apiContext;
        }

        // GET api/values
        [HttpGet]
        public TodoItem[] Get()
        {
            return _apiContext.TodoItems.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TodoItem Get(int id)
        {
            return _apiContext.TodoItems.Where(data => data.Id == id).First();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]TodoItem todoItem)
        {
            _apiContext.TodoItems.Add(todoItem);
            _apiContext.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
