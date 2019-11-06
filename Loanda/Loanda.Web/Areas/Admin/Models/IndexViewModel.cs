using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loanda.Entities;

namespace Loanda.Web.Areas.Admin.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(IEnumerable<User> users, int pageNumber, int pageSize)
        {
            this.Users = users.Select(u => new UserViewModel(u)).ToPagedList(pageNumber, pageSize);
        }
        public string StatusMessage { get; set; }
        public IPagedList<UserViewModel> Users { get; set; } 
    }
}
