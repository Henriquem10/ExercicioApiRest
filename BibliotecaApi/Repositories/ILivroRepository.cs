using BibliotecaApi.Models;

namespace BibliotecaApi.Repositories
{
    public interface ILivroRepository
    {
        Task<List<Livro>> GetAllAsync(string? autor, string? titulo, string? ISBN, int? ano);
        Task<Livro?> GetByIdAsync(int id);
        Task AddAsync(Livro livro);
        Task UpdateAsync(Livro livro);
        Task DeleteAsync(int id);
    }
}
