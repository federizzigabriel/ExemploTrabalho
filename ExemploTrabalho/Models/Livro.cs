namespace ExemploTrabalho.Models
{
    public class Livro
    {
        public int? Id { get; set; }
        public string Titulo { get; set; }
        public DateTime AnoLancamento { get; set; }
        public List<AutorLivro>? AutorLivros { get; } = new();

    }
}
