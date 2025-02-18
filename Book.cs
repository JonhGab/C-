using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOActivity.Entities
{
    public class Book : libraryItems
    {
        public string Author { get; set; }

        public Book(string title, string author) 
        {
            Title = title;
            Author = author;
        }

        public override void ExibirDetalhes()
        {
            Console.WriteLine($"[Livro] Título: {Title}, Autor: {Author}, Disponível: {Availability}");
        }
    }
}
