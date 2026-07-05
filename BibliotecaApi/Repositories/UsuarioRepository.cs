using BibliotecaApi.Data;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BibliotecaApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BibliotecaContext _context;
        public UsuarioRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAllUsersAsync(string? nome, string? Email, string? role)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(l => l.Nome.Contains(nome));
            }

            if (!string.IsNullOrWhiteSpace(Email))
            {
                query = query.Where(l => l.Email.Contains(Email));
            }
            if (!string.IsNullOrWhiteSpace(role))
            {
                query = query.Where(l => l.Role.Contains(role));
            }

            return await query.ToListAsync();
        }
        public async Task<Usuario?> GetUsersByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task CreateUserAsync(Usuario user)
        {
            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync(Usuario user)
        {
            _context.Usuarios.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user != null)
            {
                _context.Usuarios.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
