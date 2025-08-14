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
    public class ProdutoCosifService: IProdutoCosifService
    {
        private readonly IGenericRepository<ProdutoCosif> _produtoCosifRepository;

        public ProdutoCosifService(IGenericRepository<ProdutoCosif> produtoCosifRepository)
        {
            _produtoCosifRepository = produtoCosifRepository;
        }

        public async Task<IEnumerable<ProdutoCosif>> ConsultarProdutoCosif(string codigoProduto)
        {
            return await _produtoCosifRepository.BuscarPorCampo(c => c.CodigoProduto == codigoProduto && c.Status == "A");
        }
    }
}
