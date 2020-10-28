using glory.BookStore.Models;
using System.Threading.Tasks;

namespace glory.BookStore.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}