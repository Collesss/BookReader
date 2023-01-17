using AutoMapper;
using BookReader.Api.Dto.Author.Response;
using BookReader.Api.Dto.Book.Response;
using BookReader.Api.Repository.Entities;

namespace BookReader.Api.AutoMapperProfiles
{
    public class RepositoryDtoProfile : Profile
    {
        public RepositoryDtoProfile() 
        {
            CreateMap<AuthorEntity, AuthorResponseDto>();

            CreateMap<BookEntity, BookResponseDto>();
        }
    }
}
