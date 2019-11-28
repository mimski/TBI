using Loanda.Services.DTOs;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IEmailAttachmentService
    {
        Task<bool> AddAttachmentAsync(EmailAttachmentDTO emailAttachmentDto);
    }
}
