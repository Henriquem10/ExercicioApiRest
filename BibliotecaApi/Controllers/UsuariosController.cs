using BibliotecaApi.DTOs;
using BibliotecaApi.DTOs.Responses;
using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar operações relacionadas a usuários.
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        /// <summary>
        /// Inicializa uma nova instância do controlador UsuariosController.
        /// </summary>
        /// <param name="service">Serviço de usuários injetado por dependência.</param>
        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém uma lista de todos os usuários com filtros opcionais.
        /// </summary>
        /// <param name="nome">Filtro opcional pelo nome do usuário.</param>
        /// <param name="Email">Filtro opcional pelo email do usuário.</param>
        /// <param name="role">Filtro opcional pelo papel/role do usuário.</param>
        /// <returns>Uma lista de usuários que correspondem aos critérios de filtro.</returns>
        /// <response code="200">Usuários listados com sucesso.</response>
        [ProducesResponseType(typeof(ApiResponse<List<Usuario>>), StatusCodes.Status200OK)]
        [HttpGet(Name = "ListarUsuarios")]
        public async Task<ActionResult<List<Usuario>>> GetAll(string? nome, string? Email, string? role)
        {
            var usuarios = await _service.GetAllUsersAsync(nome, Email, role);
            return Ok(new ApiResponse<List<Usuario>>
            {
                Success = true,
                Message = "Usuários listados com sucesso.",
                Data = usuarios
            });
        }

        /// <summary>
        /// Obtém um usuário específico pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador único do usuário.</param>
        /// <returns>O usuário encontrado.</returns>
        /// <response code="200">Usuário encontrado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [ProducesResponseType(typeof(ApiResponse<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [HttpGet("{id}", Name = "ObterUsuarioPorId")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _service.GetUsersByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Usuário não encontrado.",
                    Data = null
                });
            }
            return Ok(new ApiResponse<Usuario>
            {
                Success = true,
                Message = "Usuário encontrado com sucesso.",
                Data = usuario
            });
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <param name="user">Os dados do novo usuário a ser criado.</param>
        /// <returns>O usuário criado com seu identificador gerado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        [ProducesResponseType(typeof(ApiResponse<CreateUser>), StatusCodes.Status201Created)]
        [HttpPost(Name = "CriarUsuario")]
        public async Task<ActionResult<CreateUser>> Create(CreateUser user)
        {
            var createdUser = await _service.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, new ApiResponse<CreateUser>
            {
                Success = true,
                Message = "Usuário criado com sucesso.",
                Data = createdUser
            });
        }

        /// <summary>
        /// Atualiza um usuário existente no sistema.
        /// </summary>
        /// <param name="id">O identificador único do usuário a ser atualizado.</param>
        /// <param name="user">Os dados atualizados do usuário.</param>
        /// <returns>Nenhum conteúdo se bem-sucedido.</returns>
        /// <response code="204">Usuário atualizado com sucesso.</response>
        /// <response code="400">ID do usuário não corresponde ao ID fornecido.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [HttpPut("{id}", Name = "AtualizarUsuario")]
        public async Task<IActionResult> Update(int id, Usuario user)
        {
            if (id != user.Id)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = "ID do usuário não corresponde ao ID fornecido.",
                    Data = null
                });
            }
            var existingUser = await _service.GetUsersByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Usuário não encontrado.",
                    Data = null
                });
            }
            await _service.UpdateUserAsync(user);
            return NoContent();
        }

        /// <summary>
        /// Deleta um usuário do sistema.
        /// </summary>
        /// <param name="id">O identificador único do usuário a ser deletado.</param>
        /// <returns>Nenhum conteúdo se bem-sucedido.</returns>
        /// <response code="204">Usuário deletado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}", Name = "DeletarUsuario")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _service.GetUsersByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Usuário não encontrado.",
                    Data = null
                });
            }
            await _service.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
