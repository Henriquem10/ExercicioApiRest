using BibliotecaApi.DTOs;
using BibliotecaApi.Mapping;
using BibliotecaApi.Models;
using BibliotecaApi.Repositories;

namespace BibliotecaApi.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILogger<LivroService> _logger;
        public LivroService(ILivroRepository livroRepository, ILogger<LivroService> logger, IEmprestimoRepository emprestimoRepository)
        {
            _livroRepository = livroRepository;
            _logger = logger;
            _emprestimoRepository = emprestimoRepository;
        }
        public async Task<List<Livro>> GetAllAsync(Autor? autor, string? titulo, string? ISBN, int? ano)
        {
            return await _livroRepository.GetAllAsync(autor, titulo, ISBN, ano);
        }
        public async Task<Livro?> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }
        public async Task<CreateLivroDto> AddAsync(CreateLivroDto request)
        {
            var livro = LivroMapper.ToEntity(request);
            await _livroRepository.AddAsync(livro);
            _logger.LogInformation("Livro {Titulo} criado.", livro.Titulo);
            return request;
        }
        public async Task UpdateAsync(Livro livro)
        {
            await _livroRepository.UpdateAsync(livro);
        }
        public async Task DeleteAsync(int id)
        {
            await _livroRepository.DeleteAsync(id);
        }
        public async Task Emprestar(Livro livro, int usuarioId)
        {
            if (livro.Emprestimos.Status != 0)
            {
                _logger.LogInformation("Livro {Titulo} já emprestado.", livro.Titulo);
                throw new InvalidOperationException();
            }

            var emprestimo = new Emprestimo
            {
                LivroId = livro.Id,
                UsuarioId = usuarioId,
                DataEmprestimo = DateTime.Now,
                Status = 1,
            };
            await _emprestimoRepository.CreateAsync(emprestimo);
        }
        public async Task Devolver(Livro livro)
        {
            if (livro.Emprestimos.Status == 0)
            {
                _logger.LogInformation("Livro Não esta emprestado.");
                throw new InvalidOperationException();
            }
            var emprestimo = await _emprestimoRepository.GetEmprestimoAtivoAsync(livro.Id);
            emprestimo.DataDevolucao = DateTime.Now;
            emprestimo.Status = 0;

            await _emprestimoRepository.UpdateAsync(emprestimo);
        }
    }
}
