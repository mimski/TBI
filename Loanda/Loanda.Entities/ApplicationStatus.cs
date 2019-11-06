using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Entities
{
   public class ApplicationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LoanApplication> LoanApplications { get; set; }
    }
}
