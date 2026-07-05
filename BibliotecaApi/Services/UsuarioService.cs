using BibliotecaApi.DTOs;
using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
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
            return user;
        }
        public async Task UpdateUserAsync(Usuario user)
        {
            await _usuarioRepository.UpdateUserAsync(user);
        }
        public async Task DeleteUserAsync(int id)
        {
            await _usuarioRepository.DeleteUserAsync(id);
        }

    }
}
