using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using glory.BookStore.Models;
using glory.BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace glory.BookStore.Controllers
{
    [Route("[controller]/[action]")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languagerRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languagerRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("~/allbooks")]
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        //book-details/id --> gibi değiştirmek istersek
        //tilda işareti ile controller index gider.
        [Route("~/book-details/{id:int:min(1)}",Name ="bookDetailsRoute")] //girilen url int ve minimum 1 olmalı
        public async Task<ViewResult> GetBook(int id, string nameOfBook)
        {
            //dynamic data = new ExpandoObject(); data.book, data.name = "görkem";
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }
        //public List<BookModel> SearchBooks(string bookName, string authorName)
        //{
        //    return _bookRepository.SearchBook(bookName, authorName);
        //}


        [Authorize] //üye girişi gerekli
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            #region
            //var model = new BookModel
            //{
            //    //Language = "English"
            //};

            //ViewBag.Language = GetLanguage().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,    
            //    Value=x.Id.ToString(),
            //}).ToList();

            //group dropdown #58
            //multi selet #59
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    //new SelectListItem(){Text="Hindi",Value="1",Disabled=true},
            //    new SelectListItem(){Text="Hindi",Value="1"},
            //    new SelectListItem(){Text="English",Value="2"},
            //    new SelectListItem(){Text="Turkish",Value="3",Selected=true},
            //    new SelectListItem(){Text="Dutch",Value="4"},
            //    new SelectListItem(){Text="Chinese",Value="5"},
            //};
            #endregion

            var model = new BookModel();
            ViewBag.Language = new SelectList(await _languagerRepository.GetLanguages(), "Id", "Name");
            ViewBag.isSuccess = isSuccess;
            ViewBag.bookId = bookId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
                };

                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var item in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel
                        {
                            Name = item.FileName,
                            URL = await UploadImage(folder, item)
                        };
                        bookModel.Gallery.Add(gallery);
                    };


                };

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                };


                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
            }
            ViewBag.Language = new SelectList(await _languagerRepository.GetLanguages(), "Id", "Name");

            ModelState.AddModelError("", "boşlukları doldurun veya eksikleri giderin.");

            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;//unique
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/" + folderPath;
        }

        //private List<LanguageModel> GetLanguage()
        //{

        //    //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");
        //    return new List<LanguageModel>
        //    {
        //        new LanguageModel(){Id=1,Text="Hindi"},
        //        new LanguageModel(){Id=2,Text="English"},
        //        new LanguageModel(){Id=3,Text="Turkish"},
        //        new LanguageModel(){Id=4,Text="Dutch"},
        //    };
        //}
    }
}
