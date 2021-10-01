using NongSan.Utilities.Models;
using System.Threading.Tasks;

namespace NongSan.Utilities.Helpers
{
    public interface IMailHelperService
    {
        Task SendMail(MailContent mailContent);
    }
}