using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class LoanApplicationEntityConfiguration : IEntityTypeConfiguration<LoanApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<LoanApplicationEntity> builder)
        {
            builder.ToTable("LoanApplications");

            builder.HasKey(loanApplication => loanApplication.Id);

            builder.Property(loanApplication => loanApplication.CreatedOn)
                .HasColumnType("date")
              .IsRequired(false);

            builder.Property(loanApplication => loanApplication.ModifiedOn)
                 .HasColumnType("date")
               .IsRequired(false);

            builder.Property(loanApplication => loanApplication.DeletedOn)
                 .HasColumnType("date")
               .IsRequired(false);

            builder.Property(loanApplication => loanApplication.IsDeleted)
                .HasDefaultValue(false);

            builder.Property(loanApplication => loanApplication.IsApproved)
                .HasDefaultValue(null);
        }
    }
}
