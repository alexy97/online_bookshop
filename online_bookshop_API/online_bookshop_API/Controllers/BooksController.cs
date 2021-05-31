using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_bookshop_API.Model;
using online_bookshop_API.Model.Requests;
using online_bookshop_API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace online_bookshop_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
      
        private readonly IMapper _mapper;
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService,IMapper mapper)
        {
            _booksService = booksService;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string? title)
        {

            if (title != null)
            {
                var books = await _booksService.GetBooks(title);
                
                return Ok(books.Value);
            }

            else
            {
                var books = await _booksService.GetBooks(null);
                return Ok(books.Value);
            }
               
        }


        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _booksService.GetBook(id);

            if (book == null)
            {
                return NotFound($"Book with the provided id of {id} does not exist in the database!");
            }

            return Ok(book.Value);
        }

  
        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookRequest bookRequest)
        {

            var bookIdCheck = _booksService.FindBook(b => b.Id == id);

            if (bookIdCheck == null)
            {
                return BadRequest($"Book with the provided Id of {id} does not exist in the database and cannot be updated!");
            }

            if (bookRequest.Title == null)
            {
                return BadRequest($"The Title attribute cannot be null!");
            }
            else if (bookRequest.Author == null)
            {
                return BadRequest($"The Author attribute cannot be null!");
            }
            else if (bookRequest.Description == null)
            {
                return BadRequest($"The Description attribute cannot be null!");
            }
            else if (bookRequest.Isbn == null)
            {
                return BadRequest($"The ISBN attribute cannot be null!");
            }
            else if (bookRequest.Summary == null)
            {
                return BadRequest($"The Summary attribute cannot be null!");
            }
            else if (bookRequest.Title.Length > 100)
                return BadRequest($"The length of the 'Title' attribute cannot exceed 100 characters!");
            else if (bookRequest.Description.Length > 2000)
                return BadRequest($"The length of the 'Description' attribute cannot exceed 2000 characters!");
            else if (bookRequest.Summary.Length > 500)
                return BadRequest($"The length of the 'Summary' attribute cannot exceed 500 characters!");
            else if (bookRequest.Isbn.Length != 13)
                return BadRequest($"The length of the 'ISBN' attribute must be exactly 13 characters!");
            else if (bookRequest.Year.Year > DateTime.Now.Year)
                return BadRequest($"The 'Year' attribute cannot be set to a future date!");
            else if (bookRequest.Title.Length > 100)
                return BadRequest($"The length of the 'Title' attribute cannot exceed 100 characters!");
            else if (bookRequest.NumPages < 0 )
                return BadRequest($"The value of the Number of Pages attribute cannot be negative!");
            else if (bookRequest.Quantity < 0)
                return BadRequest($"The value of the Quantity attribute cannot be negative!");
            else if (bookRequest.NumPurchases < 0)
                return BadRequest($"The value of the Number of Pages attribute cannot be negative!");

            var book = _mapper.Map<Book>(bookRequest);
            var updatedBook = await _booksService.PutBook(id, book);
            return Ok(updatedBook.Value);
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookRequest bookRequest)
        {
            if (bookRequest.Title == null)
            {
                return BadRequest($"The Title attribute cannot be null!");
            }
            else if (bookRequest.Author == null)
            {
                return BadRequest($"The Author attribute cannot be null!");
            }
            else if (bookRequest.Description == null)
            {
                return BadRequest($"The Description attribute cannot be null!");
            }
            else if (bookRequest.Isbn == null)
            {
                return BadRequest($"The ISBN attribute cannot be null!");
            }
            else if (bookRequest.Summary == null)
            {
                return BadRequest($"The Summary attribute cannot be null!");
            }
            else if (bookRequest.Title.Length > 100)
                return BadRequest($"The length of the 'Title' attribute cannot exceed 100 characters!");
            else if (bookRequest.Description.Length >2000)
                return BadRequest($"The length of the 'Description' attribute cannot exceed 2000 characters!");
            else if (bookRequest.Summary.Length > 500)
                return BadRequest($"The length of the 'Summary' attribute cannot exceed 500 characters!");
            else if (bookRequest.Isbn.Length != 13)
                return BadRequest($"The length of the 'ISBN' attribute must be exactly 13 characters!");
            else if (bookRequest.Year.Year > DateTime.Now.Year)
                return BadRequest($"The 'Year' attribute cannot be set to a future date!");
            else if (bookRequest.Title.Length > 100)
                return BadRequest($"The length of the 'Title' attribute cannot exceed 100 characters!");
            else if (bookRequest.NumPages < 0)
                return BadRequest($"The value of the Number of Pages attribute cannot be negative!");
            else if (bookRequest.Quantity < 0)
                return BadRequest($"The value of the Quantity attribute cannot be negative!");
            else if (bookRequest.NumPurchases < 0)
                return BadRequest($"The value of the Number of Pages attribute cannot be negative!");

            var book = _mapper.Map<Book>(bookRequest);
            var newBook = await _booksService.PostBook(book);
            if (newBook == null)
            {
                return BadRequest("A database concurrency error occurred during the post action!");
            }

            return CreatedAtAction("GetBook", new { id = newBook.Value.Id }, newBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            try
            {
                var exists = await _booksService.BookExists(id);

                if (!exists)
                {
                    return NotFound($"Book with Id = {id} not found");
                }

              await _booksService.DeleteBook(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
            return Ok("Book deleted successfully!");
        }
    }
}
