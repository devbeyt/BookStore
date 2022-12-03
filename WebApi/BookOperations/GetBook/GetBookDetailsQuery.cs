using AutoMapper;
using System;
using System.Linq;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookDetailsQuery
    {
        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }

        public GetBookDetailsQuery(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailsViewModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId); // Linq
            if(book is null)
            {
                throw new InvalidOperationException("Kitab tapilmadi!");
            }
            BookDetailsViewModel wm = _mapper.Map<BookDetailsViewModel>(book);
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
