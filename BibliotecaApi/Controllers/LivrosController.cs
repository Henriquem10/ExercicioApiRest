using BibliotecaApi.DTOs;
using BibliotecaApi.Models;
using BibliotecaApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroRepository _repository;

        public LivrosController(ILivroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "ListarLivros")]
        public async Task<ActionResult<List<Livro>>> GetAll(string? autor, string? titulo, string? ISBN, int? ano)
        {
            var livros = await _repository.GetAllAsync(autor,titulo, ISBN, ano);
            return livros;
        }

        [HttpGet("{id}", Name = "ObterLivroPorId")]
        public async Task<ActionResult<Livro>> GetById(int id)
        {
            var livro = await _repository.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpPost("AdicionaLivro",Name = "AdicionarLivro")]
        public async Task<ActionResult> AddLivro(CreateLivroDto dto)
        {
            var livro = new Livro
            {
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                ISBN = dto.ISBN,
                AnoPublicacao = dto.AnoPublicacao,
                Disponivel = true
            };
            await _repository.AddAsync(livro);
            return CreatedAtRoute("ObterLivroPorId", new { id = livro.Id }, livro);
        }

        [HttpPut("{id}", Name = "AtualizarLivro")]
        public async Task<ActionResult> UpdateLivro(int id, CreateLivroDto dto)
        {
            var livro = await _repository.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            livro.Titulo = dto.Titulo;
            livro.Autor = dto.Autor;
            livro.ISBN = dto.ISBN;
            livro.AnoPublicacao = dto.AnoPublicacao;
            await _repository.UpdateAsync(livro);
            return NoContent();
        }

        [HttpPatch("{id}/emprestar")]
        public async Task<ActionResult> EmprestarLivro(int id)
        {
            var livro = await _repository.GetByIdAsync(id);

            if (livro == null)
                return NotFound();

            await _repository.Emprestar(livro);

            return NoContent();
        }

        [HttpPatch("{id}/devolver")]
        public async Task<ActionResult> DevolverLivro(int id)
        {
            var livro = await _repository.GetByIdAsync(id);

            if (livro == null)
                return NotFound();

            await _repository.Devolver(livro);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeletarLivro")]
        public async Task<ActionResult> DeleteLivro(int id)
        {
                       var livro = await _repository.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
