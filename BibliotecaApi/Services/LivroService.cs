using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        public async Task<List<Livro>> GetAllAsync(string? autor, string? titulo, string? ISBN, int? ano)
        {
            return await _livroRepository.GetAllAsync(autor, titulo, ISBN, ano);
        }
        public async Task<Livro?> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(Livro livro)
        {
            await _livroRepository.AddAsync(livro);
        }
        public async Task UpdateAsync(Livro livro)
        {
            await _livroRepository.UpdateAsync(livro);
        }
        public async Task DeleteAsync(int id)
        {
            await _livroRepository.DeleteAsync(id);
        }
        public async Task Emprestar(Livro livro)
        {
            await _livroRepository.Emprestar(livro);
        }
        public async Task Devolver(Livro livro)
        {
            await _livroRepository.Devolver(livro);
        }
    }
}
