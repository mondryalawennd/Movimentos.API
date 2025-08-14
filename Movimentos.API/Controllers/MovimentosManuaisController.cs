using Microsoft.AspNetCore.Mvc;
using Movimentos.Business.Service.Interface;
using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;
using System;
using System.Threading.Tasks;

namespace Movimentos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentoController : ControllerBase
    {
        private readonly ILogger<MovimentoController> _logger;
        private readonly IMovimentoService _movimentoService;

        public MovimentoController(IMovimentoService movimentoService, ILogger<MovimentoController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _movimentoService = movimentoService ?? throw new ArgumentNullException(nameof(movimentoService));
        }

        [HttpPost("InserirMovimento")]
        public async Task<IActionResult> InserirMovimento([FromBody] MovimentoManualDTO movimentoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Sucesso = false,
                    Mensagem = "Dados inválidos.",
                    Erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var movimentoEntity = new Movimento
                {
                    DataMes = movimentoDTO.Mes,
                    DataAno = movimentoDTO.Ano,
                    CodigoProduto = movimentoDTO.CodigoProduto,
                    CodigoCosif = movimentoDTO.CodigoCosif,
                    NumeroLancamento = movimentoDTO.NumeroLancamento,
                    Descricao = movimentoDTO.DescricaoMovimento,
                    Valor = movimentoDTO.ValorMovimento
                };

                await _movimentoService.InserirMovimento(movimentoEntity);

                return Ok(new
                {
                    Sucesso = true,
                    Mensagem = "Movimento inserido com sucesso."
                });
            }
            catch (ApplicationException ex)
            {
                _logger.LogWarning(ex, "Erro de aplicação ao inserir movimento.");
                return StatusCode(500, new
                {
                    Sucesso = false,
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao inserir movimento.");
                return StatusCode(500, new
                {
                    Sucesso = false,
                    Mensagem = "Ocorreu um erro interno no servidor."
                });
            }
        }

        [HttpGet("ConsultarMovimentos")]
        public async Task<IActionResult> ConsultarMovimentos()
        {
            try
            {
                var movimentos = await _movimentoService.ConsultarMovimentos();

               
                return Ok(movimentos);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Erro inesperado ao consultar movimentos." });
            }
        }
    }
}
