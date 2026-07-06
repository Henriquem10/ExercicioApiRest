using BibliotecaApi.DTOs;
using BibliotecaApi.DTOs.Responses;
using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _service;

        public LivrosController(ILivroService service)
        {
            _service = service;
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
        public async Task<ActionResult<List<Livro>>> GetAll(Autor? autor, string? titulo, string? ISBN, int? ano)
        {
            var livros = await _service.GetAllAsync(autor,titulo, ISBN, ano);
            return Ok(new ApiResponse<List<Livro>>
            {
                Success = true,
                Message = "Livros listados com sucesso.",
                Data = livros
            });
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
            var livro = await _service.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Livro não encontrado."
                });
            }
            return Ok(new ApiResponse<Livro>
            {
                Success = true,
                Message = "Livro encontrado com sucesso.",
                Data = livro
            });
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

            var livro = await _service.AddAsync(dto);
            return CreatedAtRoute(
                "ObterLivroPorId",
                new { id = livro.Id },
                new ApiResponse<CreateLivroDto>
                {
                    Success = true,
                    Message = "Livro cadastrado com sucesso.",
                    Data = livro
                });
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
            var livro = await _service.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            livro.Titulo = dto.Titulo;
            livro.Autor = dto.Autor;
            livro.ISBN = dto.ISBN;
            livro.AnoPublicacao = dto.AnoPublicacao;
            await _service.UpdateAsync(livro);
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
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var livro = await _service.GetByIdAsync(id);

            if (livro == null)
                return NotFound();

            await _service.Emprestar(livro, usuarioId);

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
            var livro = await _service.GetByIdAsync(id);

            if (livro == null)
                return NotFound();

            await _service.Devolver(livro);

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
                       var livro = await _service.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
