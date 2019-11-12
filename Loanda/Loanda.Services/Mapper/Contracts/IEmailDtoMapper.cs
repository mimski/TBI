using Loanda.Entities;
using Loanda.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Services.Mapper.Contracts
{
    public interface IEmailDtoMapper
    {
        EmailDTO Map(ReceivedEmail recievedEmail);
        ReceivedEmail Map(EmailDTO recievedEmail);
    }
}
