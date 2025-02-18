using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POOActivity.Entities
{
// Classe abstrata pois é generica e não completa
    public abstract class libraryItems
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public bool Availability { get; set; }
        // metodo para exibir os detalhes, abstrato também por que é generico e sozinho não faz nada
        public abstract void ExibirDetalhes();
    }
}
