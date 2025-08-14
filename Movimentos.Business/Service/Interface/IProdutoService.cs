using Movimentos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.Business.Service.Interface
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> CarregarProdutos();
    }
}
