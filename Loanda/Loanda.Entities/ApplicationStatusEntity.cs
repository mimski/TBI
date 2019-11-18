using System.Collections.Generic;

namespace Loanda.Entities
{
   public class ApplicationStatusEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<LoanApplicationEntity> LoanApplications { get; set; }
    }
}
