using Loanda.Data.Context;
using Loanda.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Loanda.Services
{
   public class UserService : IUserService
    {
        private readonly LoandaContext dbcontext;
        public UserService(LoandaContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<bool> CheckForEmail(string email)
        {
            if (await this.dbcontext.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            }

            return false;
        }
        public async Task<bool> CheckForUserName(string username)
        {
            if (await this.dbcontext.Users.AnyAsync(x => x.UserName == username))
            {
                return true;
            }

            return false;
        }
    }
}
