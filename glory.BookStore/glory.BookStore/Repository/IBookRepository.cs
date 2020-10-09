using glory.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace glory.BookStore.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBooksAync(int count);
    }
}