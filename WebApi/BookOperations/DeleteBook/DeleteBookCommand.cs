using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        
        private readonly BookStoreDBContext _dbContext;

        public int BookId { get; set; }
        public DeleteBookCommand(BookStoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var filtered = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
            if (filtered is null)
            {
                throw new InvalidOperationException("Silinecek kitab tapilmadi!");
            }
            _dbContext.Books.Remove(filtered);
            _dbContext.SaveChanges();
        }

        public class DeleteBookModel
        {
            public string Title { get; set;}
        }
    }
}
