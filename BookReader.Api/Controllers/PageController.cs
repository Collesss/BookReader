using AutoMapper;
using AutoMapper.Execution;
using BookReader.Api.Dto.Page.Response;
using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Entities.CompositeKeys;
using BookReader.Api.Repository.Exceptions;
using BookReader.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace BookReader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly ILogger<PageController> _logger;
        private readonly IMapper _mapper;
        private readonly IPageRepository _pageRepository;

        public PageController(ILogger<PageController> logger, IMapper mapper, IPageRepository pageRepository) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pageRepository = pageRepository ?? throw new ArgumentNullException(nameof(pageRepository));
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PageResponseDto>>> Pages(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<PageEntity> pageEntities = await _pageRepository.GetAll(cancellationToken);

                IEnumerable<PageResponseDto> pageDtos =
                    _mapper.Map<IEnumerable<PageEntity>, IEnumerable<PageResponseDto>>(pageEntities);

                return Ok(pageDtos);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get pages.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"InternalServerError get pages.");
            }
        }

        [HttpGet("{bookId}/{number}")]
        public async Task<ActionResult<PageResponseDto>> Page([FromRoute] int bookId, [FromRoute] int number, CancellationToken cancellationToken)
        {
            try
            {
                PageEntity pageEntity = await _pageRepository.GetById(new PageEntityKey { BookId = bookId, Number = number }, cancellationToken);

                PageResponseDto pageResponseDto = _mapper.Map<PageEntity, PageResponseDto>(pageEntity);

                return pageResponseDto is null ? NotFound(new { bookId, number }) : Ok(pageResponseDto);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get page with bookId: {bookId}, and number: {number}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"InternalServerError get page bookId: {bookId}, and number: {number}.");
            }
        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult<int>> PagesCountInBook(int bookId)
        {
            try
            {
                return (await _pageRepository.GetAllPagesBook(bookId)) is IEnumerable<PageEntity> pages ? Ok(pages.Count()) : NotFound(bookId);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get pages count with bookId: {bookId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"InternalServerError get pages count with bookId: {bookId}.");
            }
        }
        
    }
}
