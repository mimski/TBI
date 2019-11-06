using Loanda.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Loanda.Data.Context
{
    public class LoandaContext : IdentityDbContext<User>
    {
        public LoandaContext() { }

        public LoandaContext(DbContextOptions<LoandaContext> options)
          : base(options) { }

        public virtual DbSet<Applicant> Applicants { get; set; }

        public virtual DbSet<LoanApplication> LoanApplications { get; set; }

        public virtual DbSet<ReceivedEmail> ReceivedEmails { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public virtual DbSet<EmailAttachment> EmailAttachments { get; set; }
        public virtual DbSet<EmailStatus> EmailStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
