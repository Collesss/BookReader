using AutoMapper;
using BookReader.Api.BookReaderHttpClient.Exceptions;
using BookReader.Api.BookReaderHttpClient.Interfaces;
using BookReader.Api.Dto.Author.Response;
using BookReader.Models.Author;
using Microsoft.AspNetCore.Mvc;

namespace BookReader.Controllers
{
    [Route("{controller}")]
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IBookReaderHttpClient _bookReaderHttpClient;
        private readonly IMapper _mapper;

        public AuthorController(ILogger<AuthorController> logger, IBookReaderHttpClient bookReaderHttpClient, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookReaderHttpClient = bookReaderHttpClient ?? throw new ArgumentNullException(nameof(bookReaderHttpClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("{action}/{id:int}")]
        public async Task<IActionResult> ViewAuthor([FromRoute]int id, CancellationToken cancellationToken)
        {
            try
            {
                AuthorResponseDto author = await _bookReaderHttpClient.GetAuthorById(id, cancellationToken);

                ViewAuthorViewModel viewAuthor = _mapper.Map<AuthorResponseDto, ViewAuthorViewModel>(author);

                return viewAuthor is null ? NotFound(id) : View(viewAuthor);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (BookReaderHttpClientException e)
            {
                _logger.LogError(e, $"Error get author with id: {id}");
                return BadRequest($"Error get author with id: {id}");
            }
        }
    }
}
