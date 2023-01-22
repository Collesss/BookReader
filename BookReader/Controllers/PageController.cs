using AutoMapper;
using BookReader.Api.BookReaderHttpClient.Exceptions;
using BookReader.Api.BookReaderHttpClient.Interfaces;
using BookReader.Api.Dto.Page.Response;
using BookReader.Models.Page;
using Microsoft.AspNetCore.Mvc;

namespace BookReader.Controllers
{

    [Route("{controller}")]
    public class PageController : Controller
    {
        private readonly ILogger<PageController> _logger;
        private readonly IBookReaderHttpClient _bookReaderHttpClient;
        private readonly IMapper _mapper;

        public PageController(ILogger<PageController> logger, IBookReaderHttpClient bookReaderHttpClient, IMapper mapper) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookReaderHttpClient = bookReaderHttpClient ?? throw new ArgumentNullException(nameof(bookReaderHttpClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("{action}/{bookId:int}/{number:int}")]
        public async Task<IActionResult> ViewPage(int bookId, int number, CancellationToken cancellationToken)
        {
            try
            {
                PageResponseDto page = await _bookReaderHttpClient.GetPageById(bookId, number, cancellationToken);

                if (page is null)
                    return NotFound(new { bookId, number });

                ViewPageViewModel viewPage = _mapper.Map<PageResponseDto, ViewPageViewModel>(page);

                viewPage.PageCount = await _bookReaderHttpClient.GetPagesCountInBook(bookId, cancellationToken) ?? 0;

                return View(viewPage);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (BookReaderHttpClientException e)
            {
                _logger.LogError(e, $"Error get page with bookId: {bookId}, and number: {number}.");
                return BadRequest($"Error get page with bookId: {bookId}, and number: {number}.");
            }
        }
    }
}
