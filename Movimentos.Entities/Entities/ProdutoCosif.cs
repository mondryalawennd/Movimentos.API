using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.Entities.Entities
{
    public class ProdutoCosif
    {
        public string CodigoProduto { get; set; } = string.Empty;
        public string CodigoCosif { get; set; } = string.Empty;
        public string? CodigoClassificacao { get; set; }
        public string? Status { get; set; }

        public Produto Produto { get; set; } = null!;
        public ICollection<Movimento> Movimentos { get; set; } = new List<Movimento>();
    }
}
