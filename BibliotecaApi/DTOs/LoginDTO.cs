using BibliotecaApi.Models;

namespace BibliotecaApi.DTOs
{
    public class LoginDTO
    {
        public Usuario Usuario { get; set; }
        public string Senha { get; set; }
    }
}
