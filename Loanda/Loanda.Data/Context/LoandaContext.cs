using Loanda.Data.JsonTools;
using Loanda.Entities;
using Microsoft.AspNetCore.Identity;
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

        private void LoadJsonDataInDatabase(ModelBuilder modelBuilder)
        {
            var jsonManager = new JsonManager(modelBuilder);

            //try
            //{
            jsonManager.RegisterJson<User>("users.json");
            jsonManager.RegisterJson<IdentityRole>("roles.json");
            jsonManager.RegisterJson<IdentityUserRole<string>>("userRoles.json");
            //}
            //catch (Exception)
            //{
            //    return;
            //}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            LoadJsonDataInDatabase(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
