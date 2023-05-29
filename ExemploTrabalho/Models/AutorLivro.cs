using Microsoft.Extensions.Hosting;

namespace ExemploTrabalho.Models
{
    public class AutorLivro
    {
        public int Id { get; set; }
        public int AutorId { get; set; }
        public int LivroId { get; set; }
        public Autor? Autor { get; set; }
        public Livro? Livro { get; set; }
    }
}
