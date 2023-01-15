using BookReader.Api.Repository.Entities;

namespace BookReader.Api.Repository.Interfaces
{
    public interface IBookRepository : IRepository<BookEntity, int>
    {
    }
}
