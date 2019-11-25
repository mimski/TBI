using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class ReceivedEmailEntityConfiguration : IEntityTypeConfiguration<ReceivedEmailEntity>
    {
        public void Configure(EntityTypeBuilder<ReceivedEmailEntity> builder)
        {
            builder.ToTable("ReceivedEmails");

            builder.HasKey(receivedEmail => receivedEmail.Id);

            builder.HasOne(a => a.LoanApplication)
                .WithOne(b => b.ReceivedEmail)
                .HasForeignKey<LoanApplicationEntity>(b => b.EmailId);

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

            builder.Property(loanApplication => loanApplication.EmailStatusId)
                 .HasDefaultValue(-1);
        }
    }
}
