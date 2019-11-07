using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
    {
        public void Configure(EntityTypeBuilder<LoanApplication> builder)
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
        }
    }
}
