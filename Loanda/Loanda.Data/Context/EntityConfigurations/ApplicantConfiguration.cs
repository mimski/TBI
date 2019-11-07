using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.ToTable("Applicants");

            builder.HasKey(applicant => applicant.Id);

            builder.Property(applicant => applicant.EGN)
               .IsRequired();

            builder.Property(applicant => applicant.FirstName)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(applicant => applicant.MiddleName)
                .HasMaxLength(120)
                .IsRequired(false);

            builder.Property(applicant => applicant.LastName)
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(applicant => applicant.DateOfBirth)
                .HasColumnType("date")
                .IsRequired(false);

            builder.Property(applicant => applicant.PhoneNumber)
               .IsRequired();

            builder.Property(applicant => applicant.Address)
              .HasMaxLength(1200)
              .IsRequired();

            builder.Property(applicant => applicant.City)
               .HasMaxLength(40)
               .IsRequired();

            builder.Property(applicant => applicant.EmailAddress)
                .IsRequired();

            builder.Property(applicant => applicant.CreatedOn)
                 .HasColumnType("date")
               .IsRequired(false);

            builder.Property(applicant => applicant.ModifiedOn)
                 .HasColumnType("date")
               .IsRequired(false);

            builder.Property(applicant => applicant.DeletedOn)
                 .HasColumnType("date")
               .IsRequired(false);

            builder.Property(applicant => applicant.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
