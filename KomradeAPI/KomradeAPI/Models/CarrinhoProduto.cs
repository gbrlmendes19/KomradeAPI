using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuiaAlimentarAPI.Models
{
    public class CarrinhoProduto
    {
        public int CarrinhoId { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

    }
}
