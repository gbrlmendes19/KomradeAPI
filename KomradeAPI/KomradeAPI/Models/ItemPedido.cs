﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuiaAlimentarAPI.Models
{
    public class ItemPedido
    {
        public int ProdutoId { get; set; }

        public int PedidoId { get; set; }

        public int Quantidade { get; set; }

    }
}
