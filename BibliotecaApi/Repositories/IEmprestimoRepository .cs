using BibliotecaApi.Models;

namespace BibliotecaApi.Repositories
{
    public interface IEmprestimoRepository
    {
        Task CreateAsync(Emprestimo emprestimo);
        Task UpdateAsync(Emprestimo emprestimo);
        Task<Emprestimo> GetEmprestimoAtivoAsync(int livroId);
    }
}
