using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Domain.Contracts;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public BookController(IBaseRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAll(b => b.Author);
            var bookDtos = books.Select(ToBookDto).ToList();
            return Ok(bookDtos);
        }

        [HttpGet("book/{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepository.GetById(id, b => b.Author);
            if (book == null)
                return NotFound();

            return Ok(ToBookDto(book));
        }

        [HttpPost("books")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBook createBookDto)
        {
            var book = ToBookEntity(createBookDto);
            var createdBook = await _bookRepository.Add(book);

            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, ToBookDto(createdBook));
        }

        [HttpPut("books/{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBook updateBookDto)
        {
            var existingBook = await _bookRepository.GetById(id);
            if (existingBook == null)
                return NotFound();

            existingBook.Title = updateBookDto.Title;
            existingBook.Price = updateBookDto.Price;
            existingBook.Description = updateBookDto.Description;
            existingBook.AuthorId = updateBookDto.AuthorId;

            var updatedBook = await _bookRepository.Update(existingBook);
            return Ok(ToBookDto(updatedBook));
        }

        [HttpDelete("books/{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var existingBook = await _bookRepository.GetById(id);
            if (existingBook == null)
                return NotFound();

            await _bookRepository.Delete(id);
            return NoContent();
        }

        private GetBookDto ToBookDto(Book book)
        {
            return new GetBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price,
                Description = book.Description,
                AuthorId = book.AuthorId
            };
        }

        private Book ToBookEntity(CreateBook dto)
        {
            return new Book
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description,
                AuthorId = dto.AuthorId
            };
        }
    }
}
