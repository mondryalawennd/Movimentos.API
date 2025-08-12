using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.Business.Service.Interface
{
    public interface IMovimentoService
    {
        Task InserirMovimento(Movimento movimento);
        Task<IEnumerable<MovimentoManualDTO>> ConsultarMovimentos(int mes, int ano);
    }
}
