using Loanda.Entities.Base.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Loanda.Entities.Base
{
    public class BaseEntity : IAuditable, IDeletable
    {
        public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
