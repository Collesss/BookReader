using AutoMapper;
using BookReader.Api.Repository.Entities;
using BookReader.Models.Author;
using BookReader.Models.Book;

namespace BookReader.AutoMapperProfiles
{
    public class RepositoryViewModelProfile : Profile
    {
        public RepositoryViewModelProfile() 
        {
            CreateMap<BookEntity, ViewBookViewModel>();
            CreateMap<BookEntity, BookViewModel>();

            CreateMap<AuthorEntity, ViewAuthorViewModel>();
        }
    }
}
