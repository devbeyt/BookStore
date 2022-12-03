using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDBContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model;
        public UpdateBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

            if (book is null)
            {
                throw new InvalidOperationException("Guncellenecek kitab tapilmadi!");
            }
            // if it has already been changed
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            _dbContext.SaveChanges();
           
        }
    }


    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }

}
