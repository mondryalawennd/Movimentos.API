using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.Entities.DTO
{
    public class MovimentoManualDTO
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public string CodigoProduto { get; set; } = string.Empty;
        public string CodigoCosif { get; set; }= string.Empty;
        public string DescricaoProduto { get; set; } = string.Empty;
        public long NumeroLancamento { get; set; }
        public string DescricaoMovimento { get; set; } = string.Empty;
        public decimal ValorMovimento { get; set; }

    }
}
