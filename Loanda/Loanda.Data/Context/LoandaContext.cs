using Loanda.Data.JsonTools;
using Loanda.Entities;
using Loanda.Entities.Base.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Loanda.Data.Context
{
    public class LoandaContext : IdentityDbContext<User>
    {
        public LoandaContext() { }

        public LoandaContext(DbContextOptions<LoandaContext> options)
          : base(options) { }

        public virtual DbSet<ApplicantEntity> Applicants { get; set; }

        public virtual DbSet<LoanApplicationEntity> LoanApplications { get; set; }

        public virtual DbSet<ReceivedEmailEntity> ReceivedEmails { get; set; }

        public virtual DbSet<ApplicationStatusEntity> ApplicationStatuses { get; set; }

        public virtual DbSet<EmailAttachmentEntity> EmailAttachments { get; set; }

        public virtual DbSet<EmailStatusEntity> EmailStatuses { get; set; }

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
            modelBuilder.HasDefaultSchema("public");

            LoadJsonDataInDatabase(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            this.ApplyDeletionRules();
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync(bool applyDeletionRules = true, bool applyAuditInfoRules = true)
        {
            if (applyDeletionRules is true)
            {
                this.ApplyDeletionRules();
            }

            if (applyAuditInfoRules is true)
            {
                this.ApplyAuditInfoRules();
            }
            return await base.SaveChangesAsync();
        }

        private void ApplyDeletionRules()
        {
            var entitiesForDeletion = this.ChangeTracker.Entries()
                .Where(entity => entity.State is EntityState.Deleted && entity is IDeletable);

            foreach (var entity in entitiesForDeletion)
            {
                var entityForDeletion = (IDeletable)entity.Entity;
                entityForDeletion.DeletedOn = DateTime.UtcNow.AddHours(2);
                entityForDeletion.IsDeleted = true;
                entity.State = EntityState.Modified;
            }
        }

        private void ApplyAuditInfoRules()
        {
            var newEntities = this.ChangeTracker.Entries()
                .Where(entity => entity.Entity is IAuditable && (entity.State is EntityState.Added || entity.State is EntityState.Modified));

            foreach (var entity in newEntities)
            {
                var newEntity = (IAuditable)entity.Entity;
                if (entity.State is EntityState.Added && newEntity.CreatedOn is null)
                {
                    newEntity.CreatedOn = DateTime.UtcNow.AddHours(2);
                }
                else
                {
                    newEntity.ModifiedOn = DateTime.UtcNow.AddHours(2);
                }
            }
        }
    }
}
