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
        public IActionResult Post([FromBody]TodoItem todoItem)
        {
            _apiContext.TodoItems.Add(todoItem);
            try
            {
                _apiContext.SaveChanges();
            } catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            return new CreatedAtRouteResult(new { Id = todoItem.Id }, todoItem);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]TodoItem todoItem)
        {
            if (todoItem == null || todoItem.Id != id)
            {
                return BadRequest();
            }

            var todo = _apiContext.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsDone = todoItem.IsDone;
            todo.Title = todoItem.Title;
            todo.Description = todoItem.Description;

            _apiContext.TodoItems.Update(todo);
            _apiContext.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
