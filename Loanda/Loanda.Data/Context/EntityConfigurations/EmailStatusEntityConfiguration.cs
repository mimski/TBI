﻿using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Loanda.Data.Context.Constants.DataValidationConstants.EmailStatus;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class EmailStatusEntityConfiguration : IEntityTypeConfiguration<EmailStatusEntity>
    {
        public void Configure(EntityTypeBuilder<EmailStatusEntity> builder)
        {
            builder.ToTable("EmailStatuses");

            builder.HasKey(emailStatuses => emailStatuses.Id);

            builder.Property(emailStatuses => emailStatuses.Name)
                .HasMaxLength(MaxNameLenght)
                .IsRequired();

            builder.HasData(
                    new EmailStatusEntity
                    {
                        Id = -1,
                        Name = "Not Reviewed"
                    },
                    new EmailStatusEntity
                    {
                        Id = -2,
                        Name = "New"
                    },
                    new EmailStatusEntity
                    {
                        Id = -3,
                        Name = "Invalid"
                    },
                    new EmailStatusEntity
                    {
                          Id = -4,
                          Name = "Open"
                    },
                    new EmailStatusEntity
                    {
                          Id = -5,
                          Name = "Closed"
                    });
        }
    }
}
