using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Data
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }
        public DbSet<Models.Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
