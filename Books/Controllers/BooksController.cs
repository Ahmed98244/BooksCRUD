using System.Linq;
using System.Web.Mvc;
using Books.Models;
using Books.ViewModels;
using System.Data.Entity;
using System.Net;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Books
        public ActionResult Index()
        {
            var books = _context.Books.Include(m => m.category).ToList();

            return View(books);
        }
        public ActionResult Details(int? id)
        {
            if(id == null)
            
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                var book = _context.Books.Include(m => m.category).SingleOrDefault(m => m.Id == id);

                if (book == null)
                    return HttpNotFound();


                return View(book);
        }

        public ActionResult Create()
        {
            var viewModel = new BookFormNewModel
            {
                Categories = _context.Categories.Where(m => m.IsActive) .ToList()
            };
            return View("BookForm", viewModel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var book = _context.Books.Find(id);

            if (book == null)
                return HttpNotFound();

            var viewModel = new BookFormNewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                categoryId = book.categoryId,
                Description = book.Description,
                Categories = _context.Categories.Where(m => m.IsActive).ToList()
            };

            return View("BookForm", viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookFormNewModel model)
        {
             if(!ModelState.IsValid)
            {
                model.Categories = _context.Categories.Where(m => m.IsActive).ToList();
                return View("BookForm", model);
            }

             if(model.Id == 0)
            {
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    categoryId = model.categoryId,
                    Description = model.Description
                };
                _context.Books.Add(book);
            }

             else
            {
                var book = _context.Books.Find(model.Id);

                if (book == null)
                    return HttpNotFound();

                    book.Title = model.Title;
                    book.Author = model.Author;
                    book.categoryId = model.categoryId;
                    book.Description = model.Description;
            }
          
            _context.SaveChanges();

            TempData["Message"] = "Saved Successfully";
            return RedirectToAction("Index");
        }
    }
}