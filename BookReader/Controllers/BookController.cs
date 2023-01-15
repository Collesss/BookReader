using AutoMapper;
using BookReader.Models.Book;
using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Exceptions;
using BookReader.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReader.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(ILogger<BookController> logger, IBookRepository bookRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("{controller}/{action}")]
        public async Task<IActionResult> ViewAllBook(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<BookEntity> book = await _bookRepository.GetAll(cancellationToken);

                ViewAllBookViewModel viewBook = _mapper.Map<IEnumerable<BookEntity>, ViewAllBookViewModel>(book);

                return View(viewBook);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get book.");
                return BadRequest($"Error get book.");
            }
        }

        [Route("{controller}/{action}/{id:int}")]
        public async Task<IActionResult> ViewBook([FromRoute]int id, CancellationToken cancellationToken)
        {
            try 
            {
                BookEntity book = await _bookRepository.GetById(id, cancellationToken);

                ViewBookViewModel viewBook = _mapper.Map<BookEntity, ViewBookViewModel>(book);

                return viewBook is null ? NotFound(id) : View(viewBook);
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch(RepositoryException e) 
            {
                _logger.LogError(e, $"Error get book with id: {id}");
                return BadRequest($"Error get book with id: {id}");
            }
        }
    }
}
