using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class ApplicationStatusConfiguration : IEntityTypeConfiguration<ApplicationStatus>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatus> builder)
        {
            builder.ToTable("ApplicationStatuses");

            builder.HasKey(applicationStatus => applicationStatus.Id);

            builder.Property(applicationStatus => applicationStatus.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
