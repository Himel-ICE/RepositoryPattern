namespace RepositoryPattern.Domain.Contracts
{
        public record CreateAuthor
        {   
            public string Name { get; set; }
            public string Bio { get; set; }
        }
        public record UpdateAuthor
        {
            public string Name { get; set; }
            public string Bio { get; set; }
        }
        public record DeleteAuthor
        {
            public Guid Id { get; set; }
        }
        public record GetAuthor
        {
            public Guid Id { get; set; }
        }
        public class GetAuthorDto
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Bio { get; set; }
            public ICollection<GetBookDto> Books { get; set; }
        }
}
