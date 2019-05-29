using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private BookStoreContext db = new BookStoreContext();

        // GET: Book
        public ActionResult Index(string searchString, string bookGenre)
        {
            var GenreList = new List<string>();

            var GenreQuery = from g in db.Books
                             orderby g.Genre
                             select g.Genre;

            GenreList = GenreQuery.Distinct().ToList();
            //GenreList.AddRange(GenreQuery.Distinct()); ιδιο αποτελεσμα με το απο πανω
            ViewBag.BookGenre = new SelectList(GenreList); // τα δινει στο View για να γεμισει το DropDown

            var books = from b in db.Books // φτιαχνω την λιστα μου (πηγαινω στην βαση)
                        select b;

            if (!String.IsNullOrWhiteSpace(searchString)) // με Where καλω τα αναλογα βιβλια (ηδη εχω καλεσει την βαση παραπανω αρα δεν χρειαζεται να ξαναπαω)
            {
                books = books.Where(b => b.Name.Contains(searchString));  
            }

            if (!String.IsNullOrWhiteSpace(bookGenre))
            {
                books = books.Where(b => b.Genre == bookGenre);
            }

            return View(books);
        }


        public ActionResult BargainBook()
        {
            var book = GetBargainBook();
            return View("_BargainBook", book);
        }

        private Book GetBargainBook()
        {
            return db.Books
                .OrderBy(b => b.Price)
                .First();
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,PublicationDate,Price,Genre")] Book book)
        {
            if (ModelState.IsValid) // ελεγχει αν το μοντελο ειναι οκ. Οπου δηλ. θελει string να ειναι string, οπου int -> int
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,PublicationDate,Price,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
