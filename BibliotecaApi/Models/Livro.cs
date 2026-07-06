namespace BibliotecaApi.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public required string ISBN { get; set; }
        public required string Titulo { get; set; }
        public required Autor Autor { get; set; }
        public required Autor AutorId { get; set; }
        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();

        private int _anoPublicacao;
        public int AnoPublicacao
        {
            get => _anoPublicacao;
            set => _anoPublicacao = value > 0 ? value : throw new ArgumentException("AnoPublicacao deve ser maior que 0", nameof(value));
        }
        public Emprestimo Emprestimos { get; set; }

    }
}
