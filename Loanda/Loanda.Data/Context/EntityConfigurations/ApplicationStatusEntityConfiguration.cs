using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Loanda.Data.Context.Constants.DataValidationConstants.ApplicationStatus;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class ApplicationStatusEntityConfiguration : IEntityTypeConfiguration<ApplicationStatusEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatusEntity> builder)
        {
            builder.ToTable("ApplicationStatuses");

            builder.HasKey(applicationStatus => applicationStatus.Id);

            builder.Property(applicationStatus => applicationStatus.Name)
                .HasMaxLength(MaxNameLenght)
                .IsRequired();

            builder.HasData(
                new ApplicationStatusEntity
                {
                    Id = -1,
                    Name = "Processing"
                },
                new ApplicationStatusEntity
                {
                    Id = -2,
                    Name = "Approved"
                },
                 new ApplicationStatusEntity
                 {
                     Id = -3,
                     Name = "Closed" //Rejected
                 });
        }
    }
}
