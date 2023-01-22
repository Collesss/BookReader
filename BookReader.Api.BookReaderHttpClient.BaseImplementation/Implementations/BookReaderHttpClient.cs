using BookReader.Api.BookReaderHttpClient.Exceptions;
using BookReader.Api.BookReaderHttpClient.Interfaces;
using BookReader.Api.Dto.Author.Response;
using BookReader.Api.Dto.Book.Response;
using BookReader.Api.Dto.Page.Response;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;

namespace BookReader.Api.BookReaderHttpClient.BaseImplementation.Implementations
{
    public class BookReaderHttpClient : IBookReaderHttpClient
    {
        private readonly ILogger<BookReaderHttpClient> _logger;
        private readonly HttpClient _httpClient;

        public BookReaderHttpClient(ILogger<BookReaderHttpClient> logger, HttpClient httpClient) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #region Author
        public async Task<IEnumerable<AuthorResponseDto>> GetAllAuthors(CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/Author", cancellationToken);
                httpResponseMessage.EnsureSuccessStatusCode();

                return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<AuthorResponseDto>>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch(OperationCanceledException) 
            {
                throw;
            }
            catch(HttpRequestException ex) 
            {
                _logger.LogWarning(ex, "Request error get authors.");
                throw new BookReaderHttpClientException("Request error get authors.", ex);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Unknown error get authors.");
                throw new BookReaderHttpClientException("Unknown error get authors.", ex);
            }
        }

        public async Task<AuthorResponseDto> GetAuthorById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"api/Author/{id}", cancellationToken);

                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return null;

                httpResponseMessage.EnsureSuccessStatusCode();

                return await httpResponseMessage.Content.ReadFromJsonAsync<AuthorResponseDto>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, $"Request error get author with id: {id}.");
                throw new BookReaderHttpClientException($"Request error get author with id: {id}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unknown error get author with id: {id}.");
                throw new BookReaderHttpClientException($"Unknown error get author with id: {id}.", ex);
            }
        }
        #endregion

        #region Book
        public async Task<IEnumerable<BookResponseDto>> GetAllBooks(CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/Book", cancellationToken);
                httpResponseMessage.EnsureSuccessStatusCode();
                
                return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<BookResponseDto>>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Request error get books.");
                throw new BookReaderHttpClientException("Request error get books.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown error get authors.");
                throw new BookReaderHttpClientException("Unknown error get books.", ex);
            }
        }

        public async Task<BookResponseDto> GetBookById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"api/Book/{id}", cancellationToken);

                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return null;

                httpResponseMessage.EnsureSuccessStatusCode();

                string test = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);

                return await httpResponseMessage.Content.ReadFromJsonAsync<BookResponseDto>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, $"Request error get book with id: {id}.");
                throw new BookReaderHttpClientException($"Request error get book with id: {id}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unknown error get book with id: {id}.");
                throw new BookReaderHttpClientException($"Unknown error get book with id: {id}.", ex);
            }
        }
        #endregion

        #region Page
        public async Task<IEnumerable<PageResponseDto>> GetAllPages(CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/Page", cancellationToken);
                httpResponseMessage.EnsureSuccessStatusCode();

                return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PageResponseDto>>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "Request error get pages.");
                throw new BookReaderHttpClientException("Request error get pages.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown error get pages.");
                throw new BookReaderHttpClientException("Unknown error get pages.", ex);
            }
        }

        public async Task<PageResponseDto> GetPageById(int bookId, int number, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"api/Page/{bookId}/{number}", cancellationToken);

                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return null;

                httpResponseMessage.EnsureSuccessStatusCode();

                return await httpResponseMessage.Content.ReadFromJsonAsync<PageResponseDto>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, $"Request error get book with bookId: {bookId}, and number: {number}.");
                throw new BookReaderHttpClientException($"Request error get bookId: {bookId}, and number: {number}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unknown error get book with bookId: {bookId}, and number: {number}.");
                throw new BookReaderHttpClientException($"Unknown error get book with bookId: {bookId}, and number: {number}.", ex);
            }
        }

        public async Task<int?> GetPagesCountInBook(int bookId, CancellationToken cancellationToken = default)
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"api/Page/{bookId}", cancellationToken);

                if (httpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    return null;

                httpResponseMessage.EnsureSuccessStatusCode();

                return await httpResponseMessage.Content.ReadFromJsonAsync<int?>
                    (new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, $"Request error get count page with bookId: {bookId}.");
                throw new BookReaderHttpClientException($"Request error get count page with bookId: {bookId}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unknown error get count page with bookId: {bookId}.");
                throw new BookReaderHttpClientException($"Unknown error get count page with bookId: {bookId}.", ex);
            }
        }
        #endregion
    }
}
