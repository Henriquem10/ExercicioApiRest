namespace BibliotecaApi.DTOs
{
    public class UpdateUser
    {
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public string Role { get; set; }
    }
}
