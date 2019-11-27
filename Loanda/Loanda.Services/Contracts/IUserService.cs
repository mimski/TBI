using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> CheckForEmail(string email);
        Task<bool> CheckForUserName(string username);
    }
}
