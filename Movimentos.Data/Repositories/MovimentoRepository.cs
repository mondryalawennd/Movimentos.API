using Microsoft.EntityFrameworkCore;
using Movimentos.Data.Context;
using Movimentos.Data.Repositories.Interface;
using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;

namespace Movimentos.Data.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly AppDBContext _context;

        public MovimentoRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<long> ObterUltimoNumeroLancamento(int mes, int ano)
        {
            return await _context.Movimentos
                .Where(m => m.DataMes == mes && m.DataAno == ano)
                .Select(m => (long?)m.NumeroLancamento)
                .MaxAsync() ?? 0;
        }

        public async Task<List<MovimentoManualDTO>> ObterMovimentosManuais(int mes, int ano)
        {
            return await _context.MovimentosManuaisDTO
                .FromSqlInterpolated($"EXEC dbo.SP_OBTER_MOVIMENTOS_MANUAIS {mes}, {ano}")
                .ToListAsync();
        }

        public async Task InserirMovimento(Movimento movimento)
        {
            _context.Movimentos.Add(movimento);
            await _context.SaveChangesAsync();
        }


    }
}
