using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(email => email.ProcessedEmails)
                .WithOne(email => email.ProcessedBy)
                .HasForeignKey(email => email.ProcessedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(loanApplication => loanApplication.OpenLoanApplications)
                .WithOne(loanApplication => loanApplication.OpenedBy)
                .HasForeignKey(loanApplication => loanApplication.OpenedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(loanApplication => loanApplication.ClosedLoanApplication)
                .WithOne(loanApplication => loanApplication.ClosedBy)
                .HasForeignKey(loanApplication => loanApplication.ClosedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
