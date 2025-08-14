using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;

namespace Movimentos.Data.Repositories.Interface
{
    public interface IMovimentoRepository
    {
        Task<long> ObterUltimoNumeroLancamento(int mes, int ano);
        Task<List<MovimentoManualDTO>> ObterMovimentosManuais();
        Task InserirMovimento(Movimento movimento);
    }
}
