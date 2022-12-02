using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBooks;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //private static List<Book> Books = new List<Book>
        //{
        //    new Book{Id=1,Title="Learn Html",GenreId=1,PageCount=250,PublisDate = new DateTime(2022,5,21)},
        //    new Book{Id=2,Title="Cyber security",GenreId=2,PageCount=300,PublisDate = new DateTime(2021,2,5)},
        //    new Book{Id=3,Title="Unity",GenreId=1,PageCount=120,PublisDate = new DateTime(2010,3,31)},
        //};

        private readonly BookStoreDBContext _context;

        public BooksController(BookStoreDBContext context)
        {
            _context = context;
        }

        [HttpGet("getall")]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        //[HttpGet("{id}")]
        //public Book GetById([FromQuery] string id)
        //{
        //    var book = Books.SingleOrDefault(b => b.Id == id); // Linq
        //    return book;
        //}


        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id); // Linq
            return book;       
        }

        [HttpPost("add")]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
            
        }

        [HttpPut("update")]
        public IActionResult UpdateBook(Book updatedBook)
        {
            var findBook = _context.Books.SingleOrDefault(b => b.Id == updatedBook.Id);

            if(findBook is null)
            {
                return BadRequest();
            }
                                          // if it has already been changed
            findBook.Id = updatedBook.Id != default ? updatedBook.Id : findBook.Id !; 
            findBook.Title = updatedBook.Title != default ? updatedBook.Title : findBook.Title ;
            findBook.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : findBook.GenreId;
            findBook.PublisDate = updatedBook.PublisDate != default ? updatedBook.PublisDate : findBook.PublisDate;
            findBook.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : findBook.PageCount;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("delete")]        
        public IActionResult DeleteBook(int id)
        {
            var filtered = _context.Books.SingleOrDefault(b => b.Id == id);
            if(filtered is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(filtered);
            _context.SaveChanges();
            return Ok();
        }
        

    }
}
