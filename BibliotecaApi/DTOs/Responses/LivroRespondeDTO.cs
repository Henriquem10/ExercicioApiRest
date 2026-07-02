using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.DTOs.Responses
{
    public class LivroRespondeDTO
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        [StringLength(20)]
        public required string ISBN { get; set; }
        [Range(1, 3000)]
        public int AnoPublicacao { get; set; }
        public bool Disponivel { get; set; }
    }
}
