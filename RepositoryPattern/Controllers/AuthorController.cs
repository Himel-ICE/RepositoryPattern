using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Domain.Contracts;
using RepositoryPattern.Domain.Entities;
using RepositoryPattern.Repositories;

namespace RepositoryPattern.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IBaseRepository<Author> _authorRepository;

        public AuthorController(IBaseRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/authors
        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _authorRepository.GetAll(a => a.Books);
            var authorDtos = authors.Select(ToAuthorDto).ToList();
            return Ok(authorDtos);
        }

        // GET: api/author/{id}
        [HttpGet("author/{id}")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            var author = await _authorRepository.GetById(id, a => a.Books);
            if (author == null)
                return NotFound();

            return Ok(ToAuthorDto(author));
        }

        // POST: api/authors
        [HttpPost("authors")]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthor createAuthorDto)
        {
            var author = ToAuthorEntity(createAuthorDto);
            var createdAuthor = await _authorRepository.Add(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, ToAuthorDto(createdAuthor));
        }

        // PUT: api/authors/{id}
        [HttpPut("authors/{id}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthor updateAuthorDto)
        {
            var existingAuthor = await _authorRepository.GetById(id);
            if (existingAuthor == null)
                return NotFound();

            UpdateAuthorEntity(existingAuthor, updateAuthorDto);
            var updatedAuthor = await _authorRepository.Update(existingAuthor);
            return Ok(ToAuthorDto(updatedAuthor));
        }

        // DELETE: api/authors/{id}
        [HttpDelete("authors/{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var existingAuthor = await _authorRepository.GetById(id);
            if (existingAuthor == null)
                return NotFound();

            await _authorRepository.Delete(id);
            return NoContent();
        }

        // ============================
        // 🔹 Manual Mapping Functions
        // ============================

        private GetAuthorDto ToAuthorDto(Author author)
        {
            return new GetAuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                Books = author.Books?.Select(b => new GetBookDto
                {
                    Id = b.Id,
                    Title = b.Title
                }).ToList()
            };
        }

        private Author ToAuthorEntity(CreateAuthor dto)
        {
            return new Author
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Bio = dto.Bio
            };
        }

        private void UpdateAuthorEntity(Author entity, UpdateAuthor dto)
        {
            entity.Name = dto.Name;
            entity.Bio = dto.Bio;
        }
    }

}
