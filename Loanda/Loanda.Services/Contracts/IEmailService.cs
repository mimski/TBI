using Loanda.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IEmailService
    {
        Task<EmailDTO> CreateAsync(EmailDTO emailDto);

        Task<ICollection<EmailDTO>> GetAllAsync();
    }
}
