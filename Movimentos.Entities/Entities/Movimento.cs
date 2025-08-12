using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movimentos.Entities.Entities
{
    public class Movimento
    {
        public int DataMes { get; set; }
        public int DataAno { get; set; }
        public long NumeroLancamento { get; set; }
        public string CodigoProduto { get; set; } = string.Empty;
        public string CodigoCosif { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataMovimento { get; set; }
        public string CodigoUsuario { get; set; } = string.Empty;
        [JsonIgnore]
        public ProdutoCosif? ProdutoCosif { get; set; }
    }

}
