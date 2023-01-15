using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Entities.CompositeKeys;

namespace BookReader.Api.Repository.Interfaces
{
    public interface IPageRepository : IRepository<PageEntity, PageEntityKey>
    {
    }
}
