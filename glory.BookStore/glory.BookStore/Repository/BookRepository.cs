using glory.BookStore.Data;
using glory.BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        private readonly IConfiguration _configuration;

        public BookRepository(BookStoreContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books
            {
                Author = model.Author,
                Title = model.Title,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdateOn = DateTime.UtcNow,
                LanguageId = model.LanguageId,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl

            };
            newBook.BookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl
            }).ToListAsync();

            #region

            //var books = new List<BookModel>();
            //var allBooks = await _context.Books.ToListAsync();
            //if (allBooks?.Any() == true)
            //{
            //    foreach (var book in allBooks)
            //    {
            //        books.Add(new BookModel
            //        {

            //            //Language = book.Language.Name
            //        });
            //    }
            //}
            //return books;
            #endregion
        }

        public async Task<List<BookModel>> GetTopBooksAync(int count)
        {
            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Category = book.Category,
                Description = book.Description,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Title = book.Title,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl
            }).Take(count).ToListAsync();
        }



        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    Language = book.Language.Name,
                    CoverImageUrl = book.CoverImageUrl,
                    Gallery = book.BookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();
        }

        public string GetAppName()
        {
            return _configuration["AppName"];
        }
        //public List<BookModel> SearchBook(string title, string authorName)
        //{
        //    //return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        //}
        //private List<BookModel> DataSource()
        //{
        //    return new List<BookModel>()
        //    {
        //        new BookModel(){
        //            Id=1,
        //            Title="mvc",
        //            Author="glory",
        //            Description="Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
        //            Category="Programming",
        //            Language="English",
        //            TotalPages=384

        //        },
        //                        new BookModel(){
        //            Id=2,
        //            Title="mvc",
        //            Author="glory",
        //            Description="Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",


        //            Category="Programming",
        //            Language="English",
        //            TotalPages=412

        //        },
        //                                        new BookModel(){
        //            Id=3,
        //            Title="java",
        //            Author="walter",
        //            Description="The first line of Lorem Ipsum,Lorem ipsum dolor sit amet.., comes from a line in section 1.10.32.",

        //            Category="Programming",
        //            Language="Türkçe",
        //            TotalPages=289
        //        },
        //                                                        new BookModel(){
        //            Id=4,
        //            Title="php",
        //            Author="jack",
        //            Description="There are many variations of passages of Lorem Ipsum available.",

        //            Category="?",
        //            Language="German",
        //            TotalPages=941
        //        },

        //    };
        ////}
    }
}
