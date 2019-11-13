using Loanda.Data.Context;
using Loanda.Entities;
using Loanda.Services.Contracts;
using Loanda.Services.DTOs;
using Loanda.Services.Mapper;
using Loanda.Services.Mapper.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services
{
    public class EmailService : IEmailService
    {
        private readonly LoandaContext context;
        private readonly IEmailDtoMapper dtoMapper;

        public EmailService(LoandaContext context, IEmailDtoMapper dtoMapper)
        {
            this.context = context;
            this.dtoMapper = dtoMapper;
        }

        public async Task<bool> CreateAsync(EmailDTO emailDto)
        {
            var email = dtoMapper.Map(emailDto);

            this.context.Add(email);

            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<ReceivedEmail>> GetAllAsync()
        {
            var emails = await this.context.ReceivedEmails.ToListAsync();
           
            return emails;
        }
    }
}
