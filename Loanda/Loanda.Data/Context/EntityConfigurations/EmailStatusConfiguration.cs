using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class EmailStatusConfiguration : IEntityTypeConfiguration<EmailStatus>
    {
        public void Configure(EntityTypeBuilder<EmailStatus> builder)
        {
            builder.ToTable("EmailStatuses");

            builder.HasKey(emailStatuses => emailStatuses.Id);

            builder.Property(emailStatuses => emailStatuses.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
