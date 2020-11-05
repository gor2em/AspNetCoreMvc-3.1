using glory.BookStore.Models;
using System.Threading.Tasks;

namespace glory.BookStore.Services
{
    public interface IEmailService
    {
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForgotPassword(UserEmailOptions userEmailOptions);
    }
}