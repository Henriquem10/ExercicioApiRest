using BibliotecaApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs
{
    public class UpdateLivroDto
    {
        public required string Titulo { get; set; }
        public required Autor Autor { get; set; }
        [StringLength(20)]
        public required string ISBN { get; set; }
        [Range(1, 3000)]
        public int AnoPublicacao { get; set; }
        public bool Disponivel { get; set; }
    }
}
