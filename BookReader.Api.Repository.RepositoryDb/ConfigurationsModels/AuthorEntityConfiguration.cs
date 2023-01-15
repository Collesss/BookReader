using BookReader.Api.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReader.Api.Repository.RepositoryDb.ConfigurationsModels
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.FullName);
            builder.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(155);

            builder.HasIndex(e => e.AboutAuthor);
            builder.Property(e => e.AboutAuthor)
                .IsRequired()
                .HasMaxLength(10_000);
        }
    }
}
