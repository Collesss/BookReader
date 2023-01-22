using AutoMapper;
using BookReader.Api.Dto.Author.Response;
using BookReader.Api.Dto.Book.Response;
using BookReader.Api.Dto.Page.Response;
using BookReader.Models.Author;
using BookReader.Models.Book;
using BookReader.Models.Page;

namespace BookReader.AutoMapperProfiles
{
    public class RepositoryViewModelProfile : Profile
    {
        public RepositoryViewModelProfile() 
        {
            CreateMap<AuthorResponseDto, ViewAuthorViewModel>();

            CreateMap<BookResponseDto, BookViewModel>();
            CreateMap<BookResponseDto, ViewBookViewModel>();

            CreateMap<PageResponseDto, ViewPageViewModel>();
        }
    }
}
