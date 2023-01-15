using AutoMapper;
using BookReader.Api.Repository.Entities;
using BookReader.Api.Repository.Exceptions;
using BookReader.Api.Repository.Interfaces;
using BookReader.Models.Author;
using Microsoft.AspNetCore.Mvc;

namespace BookReader.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(ILogger<AuthorController> logger, IAuthorRepository authorRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("{controller}/{action}/{id:int}")]
        public async Task<IActionResult> ViewAuthor([FromRoute]int id, CancellationToken cancellationToken)
        {
            try
            {
                AuthorEntity book = await _authorRepository.GetById(id, cancellationToken);

                ViewAuthorViewModel viewBook = _mapper.Map<AuthorEntity, ViewAuthorViewModel>(book);

                return viewBook is null ? NotFound(id) : View(viewBook);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (RepositoryException e)
            {
                _logger.LogError(e, $"Error get author with id: {id}");
                return BadRequest($"Error get author with id: {id}");
            }
        }
    }
}
