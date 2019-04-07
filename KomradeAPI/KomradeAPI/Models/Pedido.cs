using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuiaAlimentarAPI.Models
{
    public class Pedido
    {
        public int Total { get; set; }

        public int FormaPagamento { get; set; }

        public DateTime Data { get; set; }

        public int EndereçoId { get; set; }

    }
}
