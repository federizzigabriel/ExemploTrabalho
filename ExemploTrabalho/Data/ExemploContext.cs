using ExemploTrabalho.Models;
using Microsoft.EntityFrameworkCore;


namespace ExemploTrabalho.Data
{
    public class ExemploContext : DbContext
    {
        public ExemploContext(DbContextOptions<ExemploContext> options) : base(options)
        {
        }

        public DbSet<Autor>? Autores { get; set; }
        public DbSet<Livro>? Livros { get; set; }

        public DbSet<AutorLivro>? AutorLivros { get; set; }
    }
}
