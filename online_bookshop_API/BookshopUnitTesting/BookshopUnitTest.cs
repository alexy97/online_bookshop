
using AutoMapper;
using BookshopUnitTest.Mocks;
using Microsoft.AspNetCore.Mvc;
using online_bookshop_API;
using online_bookshop_API.Controllers;
using online_bookshop_API.Model;
using online_bookshop_API.Model.Requests;
using online_bookshop_API.Services.Abstractions;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookshopUnitTest

{
    public class BookshopUnitTest
    {
        BooksController _controller;
        IBooksService _service;
        public BookshopUnitTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            _service = new MockBooksService();
            _controller = new BooksController(_service, mapper);
        }
        [Fact]
        public async System.Threading.Tasks.Task GetBooks_WhenCalled_ReturnsOkResultAsync()
        {
            
            var okResult = await _controller.GetBooks("");
            Console.WriteLine(okResult);
         
           var OkObjectResult = new OkObjectResult(1);
           Assert.IsType<OkObjectResult>(okResult.Result) ;
        }
    [Fact]
    public async void GetBooks_WhenCalled_ReturnsAllItemsAsync()
    {

         var booksResult = await _controller.GetBooks("");
         
         var books =booksResult.Result;
         var okResult = Assert.IsType<OkObjectResult>(books) ;
         var items = Assert.IsType<List<Book>>(okResult.Value);
         Assert.Equal(2 , items.Count);
    }

        [Fact]
        public async void GetBooks_WhenCalledWithParameter_ReturnsItemsSorted()
        {

            var booksResult = await _controller.GetBooks("Test");
            var books = booksResult.Result;
            var okResult = Assert.IsType<OkObjectResult>(books);
            var items = Assert.IsType<List<Book>>(okResult.Value);
           
            Assert.Equal(2, items.Count);
            Assert.True(items[0].Title.Equals("Test title 2"));
            Assert.True(items[1].Title.Equals("Test title 1"));
        }


        [Fact]
        public async void GetBooksByID_ReturnsBook()
        {

            var booksResult = await _controller.GetBook(2);
            var books = booksResult.Result;
            var okResult = Assert.IsType<OkObjectResult>(books);
            var item = Assert.IsType<Book>(okResult.Value);
            Assert.True(item.Title.Equals("Test title 2"));  
        }


        [Fact]
        public async void PostBookCorrect_AddsTheBook()
        {
            var book = new BookRequest()
            { 
                Title = "Don Quixote",
                Author = "Miguel de Cervantez",
                Description = "Don Quixote description",
                Isbn = "4124567891134",
                NumPages = 623,
                NumPurchases = 29,
                Price = (decimal)19.99
                ,
                Quantity = 100,
                Summary = "Don Quixote summary",
                Year = DateTime.Now,
                Image = null
            };
            var booksResult = await _controller.PostBook(book);
            var books = booksResult.Result;
            var okResult = Assert.IsType<CreatedAtActionResult>(books);

            var updatedBooks = await _controller.GetBooks("");
           
            var okResultBooks = Assert.IsType<OkObjectResult>(updatedBooks.Result);
            var items = Assert.IsType<List<Book>>(okResultBooks.Value);
            var addedBook = items.FindLast(b => b.Title == "Don Quixote");
            Assert.True(addedBook != null);
        }


        [Fact]
        public async void PostBookIsbnIncorrect_BadRequest()
        {
            var book = new BookRequest()
            {
                Title = "Don Quixote",
                Author = "Miguel de Cervantez",
                Description = "Don Quixote description",
                Isbn = "4124567891134",
                NumPages = 623,
                NumPurchases = 29,
                Price = (decimal)19.99
                ,
                Quantity = -1,
                Summary = "Don Quixote summary",
                Year = DateTime.Now,
                Image = null
            };
            var booksResult = await _controller.PostBook(book);
            var books = booksResult.Result;
            var okResult = Assert.IsType<BadRequestObjectResult>(books);

            var updatedBooks = await _controller.GetBooks("");

            var okResultBooks = Assert.IsType<OkObjectResult>(updatedBooks.Result);
            var items = Assert.IsType<List<Book>>(okResultBooks.Value);
            var addedBook = items.FindLast(b => b.Title == "Don Quixote");
            Assert.True(addedBook == null);
        }

        [Fact]
        public async void PostBookQuantityNegative_BadRequest()
        {
            var book = new BookRequest()
            {
                Title = "Don Quixote",
                Author = "Miguel de Cervantez",
                Description = "Don Quixote description",
                Isbn = "123",
                NumPages = 623,
                NumPurchases = 29,
                Price = (decimal)19.99
                ,
                Quantity = 100,
                Summary = "Don Quixote summary",
                Year = DateTime.Now,
                Image = null
            };
            var booksResult = await _controller.PostBook(book);
            var books = booksResult.Result;
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(books);

            var updatedBooks = await _controller.GetBooks("");

            var okResultBooks = Assert.IsType<OkObjectResult>(updatedBooks.Result);
            var items = Assert.IsType<List<Book>>(okResultBooks.Value);
            var addedBook = items.FindLast(b => b.Title == "Don Quixote");
            Assert.True(addedBook == null);
        }


        [Fact]
        public async void PutBookCorrect_BookModified()
        {
            var book = new BookRequest()
            {
                Title = "New Title",
                Author = "Test Author 1",
                Description = "Test description 1",
                Isbn = "1234567891234",
                NumPages = 100,
                NumPurchases = 5,
                Price = (decimal)20,
                Quantity = 15,
                Summary = "Test Summary 1",
                Year = DateTime.Now,
                Image = null
            };
            var booksResult = await _controller.PutBook(1,book);
            var okResult = Assert.IsType<OkObjectResult>(booksResult);

            var updatedBook = await _controller.GetBook(1);

            var okResultBooks = Assert.IsType<OkObjectResult>(updatedBook.Result);
            var item = Assert.IsType<Book>(okResultBooks.Value);
          
            Assert.True(item.Title.Equals("New Title"));
            Assert.True(item.Price.Equals(20));
        }



        [Fact]
        public async void PutBookIsbnInCorrect_BookNotModified()
        {
            var book = new BookRequest()
            {
                Title = "New Title",
                Author = "Test Author 1",
                Description = "Test description 1",
                Isbn = "123",
                NumPages = 100,
                NumPurchases = 5,
                Price = (decimal)20,
                Quantity = 15,
                Summary = "Test Summary 1",
                Year = DateTime.Now,
                Image = null
            };
            var booksResult = await _controller.PutBook(1, book);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(booksResult);

            var updatedBook = await _controller.GetBook(1);

            var okResultBooks = Assert.IsType<OkObjectResult>(updatedBook.Result);
            var item = Assert.IsType<Book>(okResultBooks.Value);

            Assert.True(!item.Title.Equals("New Title"));
            Assert.True(!item.Isbn.Equals(123));
        }

    }

}
