namespace RepositoryPattern.Domain.Entities
{
    public class Author
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
