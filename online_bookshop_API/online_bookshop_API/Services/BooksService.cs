using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using online_bookshop_API.Model;
using online_bookshop_API.Services.Abstractions;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace online_bookshop_API.Services
{
    public class BooksService : IBooksService
    {
        private readonly BookshopContext _context;

        public BooksService(BookshopContext context)
        {
            _context = context;
            

        }

        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id== id);

            return book;
        }

        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(string? title)
        {
            if(!string.IsNullOrEmpty(title))
            {
                var books =  await _context.Books.Where(b => b.Title.StartsWith(title)).ToListAsync();
                return books.OrderByDescending(b => b.NumPurchases).ToList();
            }
            return await _context.Books.ToListAsync();
        }


        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            var newBook = await _context.Books.AddAsync(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return newBook.Entity;
        }

        public async Task<ActionResult<Book>> PutBook(int id, Book book)
        {
            var bookEntity = _context.Books.FirstOrDefault(b => b.Id == id);
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
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            var updatedBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return updatedBook;
        }

        public async Task<Boolean> BookExists(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;
            return true;
        }

        public Book FindBook(Func<Book, Boolean> predicate)
        {
            var book = _context.Books.FirstOrDefault(predicate);
            return book;
        }

        public async Task<Boolean> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }
    }
}
