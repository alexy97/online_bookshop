using Microsoft.AspNetCore.Mvc;
using online_bookshop_API;
using online_bookshop_API.Model;
using online_bookshop_API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopUnitTest.Mocks
{
    public class MockBooksService : IBooksService
    {
        private readonly List<Book> _books;
    public MockBooksService()
    {
            _books = new List<Book>()
            {
                new Book() { Id = 1,Title = "Test title 1", Author = "Test Author 1", Description = "Test description 1", Isbn = "1234567891234", NumPages= 100,NumPurchases = 5, Price = (decimal) 39.9
                , Quantity = 15, Summary ="Test Summary 1", Year = DateTime.Now, Image = null},
               new Book() { Id = 2,Title = "Test title 2", Author = "Test Author 2", Description = "Test description 2", Isbn = "4124567891234", NumPages= 500,NumPurchases = 20, Price = (decimal) 19.99
                , Quantity = 100, Summary ="Test Summary 2", Year = DateTime.Now, Image = null},
            };

    }

    public async Task<ActionResult<Book>> GetBook(int id)
    {
            await Task.Delay(10);
            return _books.Where(b => b.Id == id).FirstOrDefault();
    }


    public async Task<ActionResult<Book>> PostBook(Book book)
    {
            await Task.Delay(10);
            book.Id = _books.Count()+1;
            _books.Add(book);
            return book;
    }

    public async Task<ActionResult<Book>> PutBook(int id, Book book)
    {
          await Task.Delay(10);
            
          Book bookEntity =  _books.Where(b => b.Id == id).FirstOrDefault();
            bookEntity.Title = book.Title;
            if (book.Image != null)
            {
                bookEntity.Image = book.Image;
            }
            if (book.Image != null)
                bookEntity.Image = book.Image;
            bookEntity.Author = book.Author;
            bookEntity.Isbn = book.Isbn;
            bookEntity.NumPages = book.NumPages;
            bookEntity.NumPurchases = book.NumPurchases;
            bookEntity.Description = book.Description;
            bookEntity.Price = book.Price;
            bookEntity.Quantity = book.Quantity;
            bookEntity.Summary = book.Summary;
            bookEntity.Year = book.Year.ToLocalTime();
            return bookEntity;
    }

    public async Task<Boolean> BookExists(int id)
    {
        await Task.Delay(10);
        var book =  _books.Where(b => b.Id == id).FirstOrDefault();
        if (book == null)
            return false;
        return true;
    }

    public Book FindBook(Func<Book, Boolean> predicate)
    {
        var book = _books.FirstOrDefault(predicate);
        return book;
    }

    public async Task<Boolean> DeleteBook(int id)
    {
        var book =  _books.Where(b => b.Id == id).FirstOrDefault();
        if (book == null)
        {
            return false;
        }

        return await Task.FromResult(_books.Remove(book));
    }

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string title)
        {
      
            if (!string.IsNullOrEmpty(title))
            {
                var books =  _books.Where(b => b.Title.StartsWith(title));
                return await Task.FromResult<ActionResult<IEnumerable<Book>>>( books.OrderByDescending(b => b.NumPurchases).ToList());
            }
            return await Task.FromResult< ActionResult<IEnumerable<Book>>>(_books);
        }
    }
}
