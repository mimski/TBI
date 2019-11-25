using System.Threading;
using System.Threading.Tasks;

namespace Loanda.EmailClient.Contracts
{
    public interface IGmailApi
    {
        Task GetUnreadEmailsFromGmailAsync();

        Task GetEmailByGmailId(long emailId, CancellationToken cancellationToken);
    }
}
