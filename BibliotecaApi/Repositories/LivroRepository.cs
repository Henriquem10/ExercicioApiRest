using BibliotecaApi.Data;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repositories
{
    public class LivroRepository : ILivroRepository 
    {
        private readonly BibliotecaContext _context;
        public LivroRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<List<Livro>> GetAllAsync(string? autor, string? titulo, string? ISBN, int? ano)
        {
            var query = _context.Livros.AsQueryable();

            if (!string.IsNullOrWhiteSpace(autor))
            {
                query = query.Where(l => l.Autor.Contains(autor));
            }

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                query = query.Where(l => l.Titulo.Contains(titulo));
            }
            if (!string.IsNullOrWhiteSpace(ISBN))
            {
                query = query.Where(l => l.ISBN.Contains(ISBN));
            }
            if (ano.HasValue)
            {
                query = query.Where(l => l.AnoPublicacao.Equals(ano));
            }

            return await query.ToListAsync();
        }
        public async Task <Livro?> GetByIdAsync(int id)
        {
            return await _context.Livros.FindAsync(id);
        }

        public async Task AddAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }
        public async Task Emprestar(Livro livro)
        {
            if (!livro.Disponivel)
            {
                throw new InvalidOperationException("Livro já emprestado.");
            }
            livro.Disponivel = false;
            await UpdateAsync(livro);
        }

        public async Task Devolver(Livro livro)
        {
            if (livro.Disponivel)
            {
                throw new InvalidOperationException("Livro já devolvido.");
            }
            livro.Disponivel = true;
            await UpdateAsync(livro);
        }

    }
}
