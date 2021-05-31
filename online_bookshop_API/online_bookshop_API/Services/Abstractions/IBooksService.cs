using Microsoft.AspNetCore.Mvc;
using online_bookshop_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_bookshop_API.Services.Abstractions
{
    public interface IBooksService
    {
        public Task<ActionResult<IEnumerable<Book>>> GetBooks(string? title);

        public Task<ActionResult<Book>> GetBook(int id);

        public Task<ActionResult<Book>> PutBook(int id, Book book);

        public Task<ActionResult<Book>> PostBook(Book book);

        public Task<Boolean> DeleteBook(int id);
        public Task<Boolean> BookExists(int id);
        public Book FindBook(Func<Book, Boolean> predicate);
    }
}
