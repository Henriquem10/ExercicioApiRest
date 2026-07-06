namespace BibliotecaApi.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public required int LivroId { get; set; }
        public Livro Livro { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public DateTime DataEmprestimo { get; set; }
        //public DateTime? DataPrevistaDevolucao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int Status { get; set; } // 0 - Devolvido, 1 - Pendente, 2 - Atrasado
    }
}
