using BookReader.Api.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReader.Api.Repository.RepositoryDb.ConfigurationsModels
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Name);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasIndex(e => e.Description);
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2_000);

            builder
                .HasOne<AuthorEntity>()
                .WithMany()
                .HasForeignKey(b => b.AuthorId)
                .HasPrincipalKey(a => a.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
