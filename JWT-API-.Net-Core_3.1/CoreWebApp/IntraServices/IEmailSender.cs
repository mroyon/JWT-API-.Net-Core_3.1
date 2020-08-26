using System;
using System.Threading.Tasks;

namespace CoreWebApp.IntraServices
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
