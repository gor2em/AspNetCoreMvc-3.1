using glory.BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace glory.BookStore.Components
{
    public class TopBooksViewComponent:ViewComponent
    {
        private readonly IBookRepository _bookrepository;
        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            _bookrepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count) //parametre gönderme?
        {
            var books = await _bookrepository.GetTopBooksAync(count);
            return View(books);
        }
    }
}
