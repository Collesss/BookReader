using AutoMapper;
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


        public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> Authors(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<AuthorEntity> authorEntitiy = await _authorRepository.GetAll(cancellationToken);

                IEnumerable<AuthorResponseDto> authorDto = 
                    _mapper.Map<IEnumerable<AuthorEntity>, IEnumerable<AuthorResponseDto>>(authorEntitiy);

                return Ok(authorDto);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get authors.");
                return BadRequest($"Error get authors.");
            }
        }
    }
}
