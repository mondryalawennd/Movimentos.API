using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Movimentos.Business.Service.Interface;
using Movimentos.Data.Repositories.Interface;
using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;
using System.Net.Http;

namespace Movimentos.Business.Service
{
    public class MovimentoService : IMovimentoService
    {
        private readonly ILogger<MovimentoService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMovimentoRepository _movimentoRepository;       

        public MovimentoService(IMovimentoRepository movimentoRepository, IHttpContextAccessor httpContext, ILogger<MovimentoService> logger)
        {
             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
             _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _movimentoRepository = movimentoRepository ?? throw new ArgumentNullException(nameof(movimentoRepository));
            
        }

        public async Task InserirMovimento(Movimento movimento)
        {
            if (movimento == null)
                throw new ArgumentNullException(nameof(movimento), "Movimento não pode ser nulo.");

            try
            {
                var ultimoNumero = await _movimentoRepository.ObterUltimoNumeroLancamento(movimento.DataMes, movimento.DataAno);

                movimento.NumeroLancamento = (int)ultimoNumero + 1;
                movimento.DataMovimento = DateTime.UtcNow; 
                movimento.CodigoUsuario = _httpContext.HttpContext?.User?.Identity?.Name ?? "SISTEMA";

                await _movimentoRepository.InserirMovimento(movimento);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao inserir movimento. Dados: {@Movimento}", movimento);
                throw new ApplicationException("Erro ao inserir movimento.", ex);
            }
        }

        public async Task<IEnumerable<MovimentoManualDTO>> ConsultarMovimentos(int mes, int ano)
        {
            try
            {
                return await _movimentoRepository.ObterMovimentosManuais(mes, ano);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao consultar movimentos.", ex);
            }
        }
    }
}