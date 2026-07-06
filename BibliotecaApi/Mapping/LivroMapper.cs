using Azure.Core;
using BibliotecaApi.DTOs;
using BibliotecaApi.DTOs.Responses;
using BibliotecaApi.Models;

namespace BibliotecaApi.Mapping
{
    public class LivroMapper
    {

        public static Livro ToEntity(CreateLivroDto request)
        {
            return new Livro
            {
                Titulo = request.Titulo,
                Autor = request.Autor,
                AutorId = request.AutorId,
                ISBN = request.ISBN,
                AnoPublicacao = request.AnoPublicacao,
                Emprestimos = new Emprestimo { LivroId = request.Id, Status = 0 }

            };
        }
        public LivroRespondeDTO ToResponse (Livro livro)
        {
            return new LivroRespondeDTO
            {
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                ISBN = livro.ISBN,
                AnoPublicacao = livro.AnoPublicacao
            };
        }
    }
}
