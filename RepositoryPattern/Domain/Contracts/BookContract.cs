namespace RepositoryPattern.Domain.Contracts
{
    public record CreateBook
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
    public record UpdateBook
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
    public record DeleteBook
    {
        public Guid Id { get; set; }
    }
    public record GetBook
    {
        public Guid Id { get; set; }
    }
    public class GetBookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public double Price{ get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
}
