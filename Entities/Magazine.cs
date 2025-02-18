using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOActivity.Entities
{
    class Magazine : libraryItems   
    {
        public string Edition { get; set; }
        
        public Magazine(string title, string edition)
        {
            Title = title;
            Edition = edition;
        }
        public override void ExibirDetalhes()
        {
            Console.WriteLine($"[Livro] Título: {Title}, Edição: {Edition}, Disponível: {Availability}");
        }
    }

}
