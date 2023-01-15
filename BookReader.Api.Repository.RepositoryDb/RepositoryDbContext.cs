using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.RepositoryDb.ConfigurationsModels;
using Microsoft.EntityFrameworkCore;

namespace BookReader.Api.Repository.RepositoryDb
{
    public class RepositoryDbContext : DbContext
    {
        public DbSet<AuthorEntity> Authors { get; set; }

        public DbSet<BookEntity> Books { get; set; }

        public DbSet<PageEntity> Pages { get; set; }

        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PageEntityConfiguration());
        }
    }
}
