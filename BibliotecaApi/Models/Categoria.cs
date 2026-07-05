namespace BibliotecaApi.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public required string Tipo { get; set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}
