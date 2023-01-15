using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookReader.Api.Repository.RepositoryDb.Implementations
{
    public class BookRepository : Repository<BookEntity, int, RepositoryDbContext>, IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(ILogger<BookRepository> logger,
            ILogger<Repository<BookEntity, int, RepositoryDbContext>> baseRepositoryLogger,
            RepositoryDbContext dbContext) : base(baseRepositoryLogger, dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
