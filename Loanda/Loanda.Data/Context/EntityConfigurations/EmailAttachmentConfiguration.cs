using Loanda.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loanda.Data.Context.EntityConfigurations
{
    internal class EmailAttachmentConfiguration : IEntityTypeConfiguration<EmailAttachmentEntity>
    {
        public void Configure(EntityTypeBuilder<EmailAttachmentEntity> builder)
        {
            builder.ToTable("EmailAttachments");

            builder.HasKey(emailAttachment => emailAttachment.Id);

            builder.Property(emailAttachment => emailAttachment.FileSizeInMb)
                .IsRequired();

            builder.Property(emailAttachment => emailAttachment.Content)
                .IsRequired();
        }
    }
}
