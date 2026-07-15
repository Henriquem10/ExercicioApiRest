using BibliotecaApi.DTOs;
using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;
        public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
        public async Task<List<Usuario>> GetAllUsersAsync(string? nome, string? Email, string? role)
        {
            return await _usuarioRepository.GetAllUsersAsync(nome, Email, role);
        }
        public async Task<Usuario?> GetUsersByIdAsync(int id)
        {
            return await _usuarioRepository.GetUsersByIdAsync(id);
        }
        public async Task<CreateUser> CreateUserAsync(CreateUser user)
        {
            var usuario = new Usuario
            {
                Nome = user.Nome,
                Email = user.Email,
                Role = user.Role,
                Senha = user.Senha
            };
            await _usuarioRepository.CreateUserAsync(usuario);
            _logger.LogInformation("Usuário criado. Nome: {Nome}", user.Nome);
            return user;
        }
        public async Task UpdateUserAsync(Usuario user)
        {
            await _usuarioRepository.UpdateUserAsync(user);
        }
        public async Task DeleteUserAsync(int id)
        {
            await _usuarioRepository.DeleteUserAsync(id);
            _logger.LogInformation("Usuário Deletado. id: {id}", id);
        }

    }
}
