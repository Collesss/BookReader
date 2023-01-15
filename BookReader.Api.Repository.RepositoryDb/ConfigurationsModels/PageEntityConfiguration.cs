using BookReader.Api.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookReader.Api.Repository.RepositoryDb.ConfigurationsModels
{
    public class PageEntityConfiguration : IEntityTypeConfiguration<PageEntity>
    {
        public void Configure(EntityTypeBuilder<PageEntity> builder)
        {
            builder.HasKey(e => new { e.BookId, e.Number });

            builder.HasIndex(e => e.Content);
            builder.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(30_000);

            builder
                .HasOne<BookEntity>()
                .WithMany()
                .HasForeignKey(p => p.BookId)
                .HasPrincipalKey(b => b.Id)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
