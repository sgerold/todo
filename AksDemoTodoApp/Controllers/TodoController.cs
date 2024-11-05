using AksDemoTodoApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AksDemoTodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todoItems = new List<TodoItem>
        {
            new TodoItem { Id = 1, Name = "Test the API on AKS", IsComplete = false },
            new TodoItem { Id = 2, Name = "Deploy .NET Core App", IsComplete = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return Ok(_todoItems);
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(long id)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<TodoItem> CreateTodoItem(TodoItem newItem)
        {
            newItem.Id = _todoItems.Max(t => t.Id) + 1;
            _todoItems.Add(newItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTodoItem(long id, TodoItem updatedItem)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = updatedItem.Name;
            item.IsComplete = updatedItem.IsComplete;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(long id)
        {
            var item = _todoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _todoItems.Remove(item);
            return NoContent();
        }
    }
}
