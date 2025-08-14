using Movimentos.Business.Service.Interface;
using Movimentos.Data.Repositories.Interface;
using Movimentos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.Business.Service
{
    public class ProdutoService: IProdutoService
    {
        private readonly IGenericRepository<Produto> _produtoRepository;

        public ProdutoService(IGenericRepository<Produto> produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> CarregarProdutos()
        {
            return await _produtoRepository.List();
        }

    }
}
