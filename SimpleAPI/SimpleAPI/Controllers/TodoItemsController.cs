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

        // GET api/todoitems
        [HttpGet]
        public List<TodoItem> Get()
        {
            return _apiContext.TodoItems.ToList();
        }

        // GET api/todoitems/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
			var todo = _apiContext.TodoItems.FirstOrDefault(t => t.Id == id);
			if (todo == null)
			{
				return NotFound();
			}
            return Ok(todo);
        }

        // POST api/todoitems
        [HttpPost]
        public IActionResult Post([FromBody]TodoItem todoItem)
        {
            if (todoItem == null )
            {
                return BadRequest();
            }

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

        // PUT api/todoitems/5
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
        // PATCH api/todoitems/:id
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody]TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return BadRequest();
            }

            var todo = _apiContext.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Id = todoItem.Id ?? todo.Id;
            todo.IsDone = todoItem.IsDone ?? todo.IsDone;
            todo.Title = todoItem.Title ?? todo.Title;
            todo.Description = todoItem.Description ?? todo.Description;

            _apiContext.TodoItems.Update(todo);
            _apiContext.SaveChanges();

            return new NoContentResult(); 
        }

        // DELETE api/todoitems/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _apiContext.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _apiContext.TodoItems.Remove(todo);
            _apiContext.SaveChanges();
            return new NoContentResult();
        }
    }
}
