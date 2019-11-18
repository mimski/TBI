using Loanda.Entities.Base.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace Loanda.Entities
{
    public class User : IdentityUser, IAuditable, IDeletable
    {
        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
