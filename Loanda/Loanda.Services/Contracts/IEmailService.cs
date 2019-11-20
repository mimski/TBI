using Loanda.Entities;
using Loanda.Services.DTOs;
using Loanda.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IEmailService
    {
        Task<long> CreateAsync(EmailDTO emailDto);

        Task<IReadOnlyCollection<ReceivedEmail>> GetAllAsync(CancellationToken cancellationToken);

        Task<ReceivedEmail> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
