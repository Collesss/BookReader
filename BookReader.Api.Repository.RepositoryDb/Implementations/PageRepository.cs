using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Entities.CompositeKeys;
using BookReader.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReader.Api.Repository.RepositoryDb.Implementations
{
    public class PageRepository : Repository<PageEntity, PageEntityKey, RepositoryDbContext>, IPageRepository
    {
        private readonly ILogger<PageRepository> _logger;

        public PageRepository(ILogger<PageRepository> logger,
            ILogger<Repository<PageEntity, PageEntityKey, RepositoryDbContext>> baseRepositoryLogger,
            RepositoryDbContext dbContext) : base(baseRepositoryLogger, dbContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<PageEntity> Find(PageEntityKey id, CancellationToken cancellationToken = default) =>
            await _dbContext.FindAsync<PageEntity>(id.BookId, id.Number, cancellationToken);

        public async Task<IEnumerable<PageEntity>> GetAllPagesBook(int bookId, CancellationToken cancellationToken = default) =>
            (await _dbContext.Books.FindAsync(bookId, cancellationToken)) is null ? 
                null : 
                await _dbContext.Pages.Where(page => page.BookId == bookId).OrderBy(page => page.Number).ToListAsync();
    }
}
