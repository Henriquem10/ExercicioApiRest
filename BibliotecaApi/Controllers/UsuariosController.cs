using BibliotecaApi.DTOs;
using BibliotecaApi.DTOs.Responses;
using BibliotecaApi.Models;
using BibliotecaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }
        [ProducesResponseType(typeof(List<Usuario>), StatusCodes.Status200OK)]
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

        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [ProducesResponseType(typeof(CreateUser), StatusCodes.Status201Created)]
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
