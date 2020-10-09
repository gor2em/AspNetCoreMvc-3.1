using glory.BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace glory.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}