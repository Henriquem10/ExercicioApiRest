using BibliotecaApi.Models;

namespace BibliotecaApi.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUsersByIdAsync(int id);
        Task<List<Usuario>> GetAllUsersAsync(string? nome, string? Email, string? role);
        Task CreateUserAsync(Usuario user);
        Task UpdateUserAsync(Usuario user);
        Task DeleteUserAsync(int id);
    }
}
