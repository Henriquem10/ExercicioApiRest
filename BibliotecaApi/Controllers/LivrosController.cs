using BibliotecaApi.DTOs;
using BibliotecaApi.Models;
using BibliotecaApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroRepository _repository;

        public LivrosController(ILivroRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Lista todos os livros cadastrados.
        /// </summary>
        /// <param name="autor">Filtro opcional por autor.</param>
        /// <param name="titulo">Filtro opcional por título.</param>
        /// <param name="ISBN">Filtro opcional por ISBN.</param>
        /// <param name="ano">Filtro opcional por ano de publicação.</param>
        /// <returns>Lista de livros encontrados.</returns>
        /// <response code="200">Livros retornados com sucesso.</response>
        [ProducesResponseType(typeof(List<Livro>), StatusCodes.Status200OK)]
        [HttpGet(Name = "ListarLivros")]
        public async Task<ActionResult<List<Livro>>> GetAll(string? autor, string? titulo, string? ISBN, int? ano)
        {
            var livros = await _repository.GetAllAsync(autor,titulo, ISBN, ano);
            return livros;
        }

        /// <summary>
        /// Busca um livro pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <returns>Livro encontrado.</returns>
        /// <response code="200">Livro encontrado com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Adiciona um novo livro ao catálogo.
        /// </summary>
        /// <param name="dto">Dados do livro a ser cadastrado.</param>
        /// <returns>Livro criado com sucesso.</returns>
        /// <response code="201">Livro criado com sucesso.</response>
        /// <response code="400">Dados inválidos para criação do livro.</response>
        /// <response code="401">Usuário não autenticado.</response>
        [ProducesResponseType(typeof(Livro), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        [HttpPost("AdicionaLivro", Name = "AdicionarLivro")]
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

        /// <summary>
        /// Atualiza os dados de um livro existente.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <param name="dto">Novos dados do livro.</param>
        /// <returns>Confirmação da atualização.</returns>
        /// <response code="204">Livro atualizado com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        /// <response code="400">Dados inválidos.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Realiza o empréstimo de um livro.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <returns>Retorna sucesso caso o empréstimo seja realizado.</returns>
        /// <response code="204">Livro emprestado com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        /// <response code="400">Livro já está emprestado.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}/emprestar")]
        public async Task<ActionResult> EmprestarLivro(int id)
        {
            var livro = await _repository.GetByIdAsync(id);

            if (livro == null)
                return NotFound();

            await _repository.Emprestar(livro);

            return NoContent();
        }

        /// <summary>
        /// Registra a devolução de um livro.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <response code="204">Livro devolvido com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        /// <response code="400">Livro já está disponível.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id}/devolver")]
        public async Task<ActionResult> DevolverLivro(int id)
        {
            var livro = await _repository.GetByIdAsync(id);

            if (livro == null)
                return NotFound();

            await _repository.Devolver(livro);

            return NoContent();
        }

        /// <summary>
        /// Remove um livro do catálogo.
        /// </summary>
        /// <param name="id">Identificador do livro.</param>
        /// <returns>Confirmação da remoção.</returns>
        /// <response code="204">Livro removido com sucesso.</response>
        /// <response code="404">Livro não encontrado.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
