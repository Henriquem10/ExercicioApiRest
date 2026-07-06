using BibliotecaApi.Data;
using BibliotecaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaApi.Repositories
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly BibliotecaContext _context;
        public EmprestimoRepository(BibliotecaContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Emprestimo emprestimo)
        {
            _context.Emprestimos.Add(emprestimo);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Emprestimo emprestimo)
        {
            _context.Emprestimos.Update(emprestimo);
            await _context.SaveChangesAsync();
        }
        public async Task<Emprestimo> GetEmprestimoAtivoAsync(int livroId)
        {
            var result = await _context.Emprestimos.FirstOrDefaultAsync(e => e.LivroId == livroId && e.Status == 1);
            if (result == null)
                {
                throw new Exception("Nenhum empréstimo ativo encontrado para o livro especificado.");
            }
            return result;
        }
    }
}
