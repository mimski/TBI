using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Loanda.Data.Context.Constants.DataValidationConstants.Applicant;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class ApplicantEntityConfiguration : IEntityTypeConfiguration<ApplicantEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicantEntity> builder)
        {
            builder.ToTable("Applicants");

            builder.HasKey(applicant => applicant.Id);

            builder.Property(applicant => applicant.EGN)
               .IsRequired();

            builder.Property(applicant => applicant.FirstName)
                .HasMaxLength(MaxFirstNameLenght)
                .IsRequired();

            builder.Property(applicant => applicant.MiddleName)
                .HasMaxLength(MaxMiddleNameLenght)
                .IsRequired(false);

            builder.Property(applicant => applicant.LastName)
                .HasMaxLength(MaxLastNameLenght)
                .IsRequired();

            builder.Property(applicant => applicant.DateOfBirth)
                .HasColumnType("date")
                .IsRequired(false);

            builder.Property(applicant => applicant.PhoneNumber)
               .IsRequired();

            builder.Property(applicant => applicant.Address)
              .HasMaxLength(MaxAddressLenght)
              .IsRequired();

            builder.Property(applicant => applicant.City)
               .HasMaxLength(MaxCityLenght)
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
