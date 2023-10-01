using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoListElement>>> GetTodoItems()
        {
          if (_context.TodoListElements == null)
          {
              return NotFound();
          }
            return await _context.TodoListElements.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListElement>> GetTodoItem(long id)
        {
          if (_context.TodoListElements == null)
          {
              return NotFound();
          }
            var todoListElement = await _context.TodoListElements.FindAsync(id);

            if (todoListElement == null)
            {
                return NotFound();
            }

            return todoListElement;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoListElement todoListElement)
        {
            if (id != todoListElement.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoListElement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoListElement>> PostTodoItem(TodoListElement todoListElement)
        {
          if (_context.TodoListElements == null)
          {
              return Problem("Entity set 'TodoContext.TodoItems'  is null.");
          }
            _context.TodoListElements.Add(todoListElement);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoListElement.Id }, todoListElement);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (_context.TodoListElements == null)
            {
                return NotFound();
            }
            var todoItem = await _context.TodoListElements.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoListElements.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return (_context.TodoListElements?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
