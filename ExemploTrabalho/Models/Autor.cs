﻿namespace ExemploTrabalho.Models
{
    public class Autor
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Nacionalidade { get; set; }
        public List<AutorLivro>? AutorLivros { get; } = new();




    }
}
