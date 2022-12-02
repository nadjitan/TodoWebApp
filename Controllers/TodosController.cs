using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoWebAppContext _context;

        public TodosController(TodoWebAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index() => Ok(_context.Todo.ToList());

        [HttpGet("{searchPhrase}")]
        public async Task<ActionResult> TodosSearch(string searchPhrase)
        {
            return Ok(await _context.Todo.Where(t => t.Title!.Contains(searchPhrase)).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> Find(int id)
        {
            var todo = await _context.Todo.FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null) return NotFound();

            return Ok(todo);
        }

        // For JS: https://learn.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-6.0#javascript-1
        [HttpPut("create-todo")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Title,Done,CreatedDate")] Todo todo)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(todo.Title))
            {
                _context.Add(todo);
                await _context.SaveChangesAsync();
                return Ok(todo);
            }
            return BadRequest("Not a valid todo.");
        }

        [HttpPatch("update-todo")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateTodo([Bind("Id,Title,Done,CreatedDate")] Todo todo)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(todo.Title))
            {
                try
                {
                    _context.Update(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id)) return NotFound();
                    else throw;
                }
                return Ok(todo);
            }
            return BadRequest("Not a valid todo.");
        }

        [HttpDelete("delete-todo/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            if (_context.Todo == null) return Problem("Entity set 'TodoWebAppContext.Todo'  is null.");

            var todo = _context.Todo.Find(id);

            if (todo != null) _context.Todo.Remove(todo);
            else return Problem($"Todo with id {id} does not exist.");

            await _context.SaveChangesAsync();
            return Ok(todo);
        }

        private bool TodoExists(int id) => _context.Todo.Any(e => e.Id == id);
    }
}
