using AutoMapper;
using BookReader.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


    }
}
