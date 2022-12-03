using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; } // entity from ui

        private readonly BookStoreDBContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(b => b.Title == Model.Title);

            if (book is not null)
            {
                throw new InvalidOperationException("Bu kitab zaten mevcuddur.");
            }
            book = _mapper.Map<Book>(Model);

            //book = new Book();
            //book.Title = Model.Title;
            //book.PublisDate = Model.PublishDate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = Model.GenreId;


            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }


    public class CreateBookModel 
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }

}
