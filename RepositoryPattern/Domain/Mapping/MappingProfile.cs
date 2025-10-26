using AutoMapper;
using RepositoryPattern.Domain.Contracts;
using RepositoryPattern.Domain.Entities;

namespace RepositoryPattern.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Author, GetAuthorDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
            CreateMap<CreateAuthor, Author>();
            CreateMap<UpdateAuthor, Author>();
            CreateMap<UpdateAuthor, Author>();
            CreateMap<DeleteAuthor, Author>();

            CreateMap<Book, GetBookDto>();
            CreateMap<CreateBook, Book>();
            CreateMap<UpdateBook, Book>();
            CreateMap<DeleteBook, Book>();
            CreateMap<GetBook, Book>();
        }
    }
}
