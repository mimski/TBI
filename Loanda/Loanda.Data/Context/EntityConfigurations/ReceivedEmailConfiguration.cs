using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class ReceivedEmailConfiguration : IEntityTypeConfiguration<ReceivedEmail>
    {
        public void Configure(EntityTypeBuilder<ReceivedEmail> builder)
        {
            builder.ToTable("ReceivedEmails");

            builder.HasKey(receivedEmail => receivedEmail.Id);

            builder.Property(receivedEmail => receivedEmail.IsReviewed)
                .HasDefaultValue(false);

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
