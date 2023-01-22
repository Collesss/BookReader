using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Entities.CompositeKeys;

namespace BookReader.Api.Repository.Interfaces
{
    public interface IPageRepository : IRepository<PageEntity, PageEntityKey>
    {
        Task<IEnumerable<PageEntity>> GetAllPagesBook(int bookId, CancellationToken cancellationToken = default);
    }
}
