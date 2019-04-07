using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuiaAlimentarAPI.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public double Valor { get; set; }

        public string Plataforma { get; set; }

        public string Genero { get; set; }

        public string Estudio { get; set; }

        public int IdadeRecomendada { get; set; }

        public DateTime DataLançamento { get; set; }

        public string ResoluçãoMaxima { get; set; }

        public string Tipo { get; set; }


    }
}
