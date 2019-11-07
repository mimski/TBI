using System.Collections.Generic;

namespace Loanda.Entities
{
   public class ApplicationStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<LoanApplication> LoanApplications { get; set; }
    }
}
