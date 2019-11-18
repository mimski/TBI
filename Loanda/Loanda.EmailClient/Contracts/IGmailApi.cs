using System.Threading.Tasks;

namespace Loanda.EmailClient.Contracts
{
    public interface IGmailApi
    {
        Task GetUnreadEmailsFromGmailAsync();
    }
}
