using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookStore.Models
{
    public class BookDBInitializer : DropCreateDatabaseAlways<BookStoreContext>
    {
        protected override void Seed(BookStoreContext context)
        {
            context.Books.Add(new Book()
            {
                Name = "Lost",
                PublicationDate = new DateTime(2014, 5, 3),
                Price = 25,
                Genre = "Cartoon"

            });

            context.Books.Add(new Book()
            {
                Name = "Hello",
                PublicationDate = new DateTime(2011, 1, 5),
                Price = 99,
                Genre = "Comedy"
            });

            context.Books.Add(new Book()
            {
                Name = "Bye",
                PublicationDate = new DateTime(2000, 7, 3),
                Price = 51,
                Genre = "Drama"
            });


            base.Seed(context);
        }
    }
}