using blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace blog.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly BlogContext _context;

        public BlogController(BlogContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="ADMIN")]
        [HttpGet]

        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> UsersIndex()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> UsersIndexDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }


        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task <IActionResult> Add([Bind("Id,Heading,Content,Author")] Blog blog)
        {

            if(ModelState.IsValid)
            {
                _context.Blogs.Add(blog);
               await  _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //var blogg = new Blog()
            //{
            //    Heading=blog.Heading,
            //    Content=blog.Content,
            //    Author=blog.Author,
            //};
            //_context.Blogs.Add(blog);
            //_context.SaveChangesAsync();
            return View(blog);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Heading,Content, Author")] Blog blog)
        {
            if (id != blog.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
               
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
             
                
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
        [Authorize(Roles = "ADMIN")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.ID == id);
        }
    }

    //[HttpGet]
    //    public ActionResult Get()
    //    {
    //        if (_context.Blogs == null)
    //            return NotFound();

    //       // return _context.Blog.ToList();
    //        return View();
    //    }
}

