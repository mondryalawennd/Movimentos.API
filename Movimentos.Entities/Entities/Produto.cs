using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.Entities.Entities
{
    public class Produto
    {
        public string CodigoProduto { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? Status { get; set; }

        public ICollection<ProdutoCosif>? ProdutosCosif { get; set; } = new List<ProdutoCosif>();
    }
}
