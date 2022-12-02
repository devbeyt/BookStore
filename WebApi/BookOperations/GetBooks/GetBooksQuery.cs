using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDBContext _dbContext;

        public GetBooksQuery(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var books = _dbContext.Books.OrderBy(b => b.Id).ToList<Book>(); // Linq
            List<BooksViewModel> wm = new List<BooksViewModel>();
            foreach (var book in books)
            {
                wm.Add(new BooksViewModel()
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PublishDate = book.PublisDate.Date.ToString("dd/MM/yyy"),
                    PageCount = book.PageCount
                });
            }
            return wm;
        }
    }

    public class BooksViewModel 
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }

}
