using BookReader.Api.Dto.Author.Response;
using BookReader.Api.Dto.Book.Response;
using BookReader.Api.Dto.Page.Response;

namespace BookReader.Api.BookReaderHttpClient.Interfaces
{
    public interface IBookReaderHttpClient
    {
        #region Author
        Task<IEnumerable<AuthorResponseDto>> GetAllAuthors(CancellationToken cancellationToken = default);

        Task<AuthorResponseDto> GetAuthorById(int id, CancellationToken cancellationToken = default);
        #endregion

        #region Book
        Task<IEnumerable<BookResponseDto>> GetAllBooks(CancellationToken cancellationToken = default);

        Task<BookResponseDto> GetBookById(int id, CancellationToken cancellationToken = default);
        #endregion

        #region Page
        Task<IEnumerable<PageResponseDto>> GetAllPages(CancellationToken cancellationToken = default);

        Task<PageResponseDto> GetPageById(int bookId, int number, CancellationToken cancellationToken = default);

        Task<int?> GetPagesCountInBook(int bookId, CancellationToken cancellation = default);
        #endregion
    }
}
