using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookDetailsQuery
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId { get; set; }

        public GetBookDetailsQuery(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookDetailsViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId); // Linq
            if(book is null)
            {
                throw new InvalidOperationException("Kitab tapilmadi!");
            }
            BookDetailsViewModel wm = new BookDetailsViewModel();
            wm.Title = book.Title;
            wm.PageCount = book.PageCount;
            wm.PublishDate = book.PublisDate.Date.ToString("dd/MM/yyy");
            wm.Genre = ((GenreEnum)book.GenreId).ToString();
            return wm;
        }
    }

    public class BookDetailsViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
