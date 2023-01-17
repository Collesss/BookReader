using AutoMapper;
using BookReader.Api.Dto.Book.Response;
using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Exceptions;
using BookReader.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger, IMapper mapper, IBookRepository bookRepository) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BookResponseDto>>> Authors(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<BookEntity> bookEntities = await _bookRepository.GetAll(cancellationToken);

                IEnumerable<BookResponseDto> bookDtos =
                    _mapper.Map<IEnumerable<BookEntity>, IEnumerable<BookResponseDto>>(bookEntities);

                return Ok(bookDtos);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get authors.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"InternalServerError get authors.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookResponseDto>> Author([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                BookEntity bookEntity = await _bookRepository.GetById(id, cancellationToken);

                BookResponseDto bookResponseDto = _mapper.Map<BookEntity, BookResponseDto>(bookEntity);

                return bookResponseDto is null ? NotFound(id) : Ok(bookResponseDto);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get author with id: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"InternalServerError get author with id: {id}.");
            }
        }
    }
}
