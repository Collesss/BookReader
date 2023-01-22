using AutoMapper;
using BookReader.Api.Dto.Author.Request;
using BookReader.Api.Dto.Author.Response;
using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Exceptions;
using BookReader.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(ILogger<AuthorController> logger, 
            IAuthorRepository authorRepository, IMapper mapper) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> Authors(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<AuthorEntity> authorEntities = await _authorRepository.GetAll(cancellationToken);

                IEnumerable<AuthorResponseDto> authorDtos = 
                    _mapper.Map<IEnumerable<AuthorEntity>, IEnumerable<AuthorResponseDto>>(authorEntities);

                return Ok(authorDtos);
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
        public async Task<ActionResult<AuthorResponseDto>> Author([FromRoute]int id, CancellationToken cancellationToken)
        {
            try
            {
                AuthorEntity authorEntity = await _authorRepository.GetById(id, cancellationToken);

                AuthorResponseDto authorResponseDto = _mapper.Map<AuthorEntity, AuthorResponseDto>(authorEntity);

                return authorResponseDto is null ? NotFound(id) : Ok(authorResponseDto);
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get author with id: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"InternalServerError get author with id: {id}.");
            }
        }

        public async Task<ActionResult<AuthorResponseDto>> AddAuthor(CreateAuthorRequestDto createAuthor, CancellationToken cancellationToken)
        {
            try
            {
                AuthorEntity createAuthorEntity = _mapper.Map<CreateAuthorRequestDto, AuthorEntity>(createAuthor);

                AuthorEntity createdauthorEntity = await _authorRepository.Create(createAuthorEntity, cancellationToken);

                AuthorResponseDto authorResponseDto = _mapper.Map<AuthorEntity, AuthorResponseDto>(createdauthorEntity);

                return Ok(authorResponseDto);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, "Error create author @{CreateAuthorRequestDto}.", createAuthor);
                return BadRequest(createAuthor);
            }
        }
    }
}
