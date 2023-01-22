using AutoMapper;
using BookReader.Models.Book;
using Microsoft.AspNetCore.Mvc;
using BookReader.Api.BookReaderHttpClient.Interfaces;
using BookReader.Api.Dto.Book.Response;
using BookReader.Api.BookReaderHttpClient.Exceptions;

namespace BookReader.Controllers
{
    [Route("{controller}")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookReaderHttpClient _bookReaderHttpClient;
        private readonly IMapper _mapper;

        public BookController(ILogger<BookController> logger, IBookReaderHttpClient bookReaderHttpClient, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookReaderHttpClient = bookReaderHttpClient ?? throw new ArgumentNullException(nameof(bookReaderHttpClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("{action}")]
        public async Task<IActionResult> ViewAllBook(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<BookResponseDto> book = await _bookReaderHttpClient.GetAllBooks(cancellationToken);

                ViewAllBookViewModel viewBook = _mapper.Map<IEnumerable<BookResponseDto>, ViewAllBookViewModel>(book);

                return View(viewBook);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (BookReaderHttpClientException e)
            {
                _logger.LogError(e, $"Error get book.");
                return BadRequest($"Error get book.");
            }
        }

        [Route("{action}/{id:int}")]
        public async Task<IActionResult> ViewBook([FromRoute]int id, CancellationToken cancellationToken)
        {
            try 
            {
                BookResponseDto book = await _bookReaderHttpClient.GetBookById(id, cancellationToken);

                ViewBookViewModel viewBook = _mapper.Map<BookResponseDto, ViewBookViewModel>(book);

                return viewBook is null ? NotFound(id) : View(viewBook);
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch(BookReaderHttpClientException e) 
            {
                _logger.LogError(e, $"Error get book with id: {id}");
                return BadRequest($"Error get book with id: {id}");
            }
        }
    }
}
