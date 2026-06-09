namespace BibliotecaApi.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public required string ISBN { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        
        private int _anoPublicacao;
        public int AnoPublicacao
        {
            get => _anoPublicacao;
            set => _anoPublicacao = value > 0 ? value : throw new ArgumentException("AnoPublicacao deve ser maior que 0", nameof(value));
        }
        public bool Disponivel { get; set; } = true;

    }
}
