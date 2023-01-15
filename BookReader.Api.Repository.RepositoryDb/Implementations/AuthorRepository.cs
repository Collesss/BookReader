using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BookReader.Api.Repository.RepositoryDb.Implementations
{
    public class AuthorRepository : Repository<AuthorEntity, int, RepositoryDbContext>, IAuthorRepository
    {
        private readonly ILogger<AuthorRepository> _logger;

        public AuthorRepository(ILogger<AuthorRepository> logger,
            ILogger<Repository<AuthorEntity, int, RepositoryDbContext>> baseRepositoryLogger,
            RepositoryDbContext dbContext) : base(baseRepositoryLogger, dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
