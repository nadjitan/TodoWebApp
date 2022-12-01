using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Dynamic;
using TodoWebApp.Data;
using TodoWebApp.Models;

namespace TodoWebApp.Controllers
{

    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly TodoWebAppContext _context;
        private readonly HomeViewModel homeViewModel = new();

        //public HomeController(ILogger<HomeController> logger)
        public HomeController(TodoWebAppContext context)
        {
            //_logger = logger;
            _context = context;
        }

        [Route("")]
        [Route("todo")]
        [Route("todo/ssr")]
        public async Task<ActionResult> Index()
        {
            homeViewModel.Todos = await _context.Todo.ToListAsync();
            return View(homeViewModel);
        }

        [Route("todo/csr")]
        public async Task<ActionResult> Csr()
        {
            homeViewModel.Todos = await _context.Todo.ToListAsync();
            return View(homeViewModel);
        }

        // POST: Home/TodosSearch
        public async Task<ActionResult> TodosSearch(String SearchPhrase)
        {
            return View("Index", await _context.Todo.Where(t => t.Title!.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Home/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Todo == null) return NotFound();

        //    var todo = await _context.Todo.FirstOrDefaultAsync(m => m.Id == id);
        //    if (todo == null) return NotFound();

        //    return View(todo);
        //}

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Title,Done,CreatedDate")] Todo todo)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(todo.Title))
            {
                homeViewModel.Title = todo.Title;
                homeViewModel.Done = todo.Done;
                homeViewModel.CreatedDate = todo.CreatedDate;
                //Debug.WriteLine($"ID: {todo.Id}\nTitle: {todo.Title}\nDone: {todo.Done}\nCreatedDate: {todo.CreatedDate}\n");

                _context.Add(todo);
                await _context.SaveChangesAsync();
                homeViewModel.Todos = await _context.Todo.ToListAsync();
                return RedirectToAction(nameof(Index), homeViewModel);
            }
            else
            {
                homeViewModel.Todos = await _context.Todo.ToListAsync();
                return View(nameof(Index), homeViewModel);
            }
        }

        // GET: Home/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null || _context.Todo == null) return NotFound();

            var todo = await _context.Todo.FindAsync(id);
            if (todo == null) return NotFound();

            return View(todo);
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Title,Done,CreatedDate")] Todo todo)
        {
            if (id != todo.Id) return NotFound();

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
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        // GET: Home/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null || _context.Todo == null) return NotFound();

            var todo = await _context.Todo.FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null) return NotFound();

            return View(todo);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (_context.Todo == null) return Problem("Entity set 'TodoWebAppContext.Todo'  is null.");

            var todo = await _context.Todo.FindAsync(id);
            if (todo != null) _context.Todo.Remove(todo);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}