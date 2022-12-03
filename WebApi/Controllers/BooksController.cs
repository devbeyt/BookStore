using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
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
        private readonly IMapper _mapper;

        public BooksController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
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
        public IActionResult GetById(int id)
        {
            BookDetailsViewModel result;
            try
            {
                GetBookDetailsQuery query = new GetBookDetailsQuery(_context,_mapper);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
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
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
            
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();
        }


        [HttpDelete("delete")]        
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        

    }
}
