using Microsoft.AspNetCore.Mvc;
using Movimentos.Business.Service.Interface;
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
        public async Task<IActionResult> InserirMovimento([FromBody] Movimento movimento)
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
                await _movimentoService.InserirMovimento(movimento);

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

        [HttpGet("ConsultarMovimentos/{mes:int}/{ano:int}")]
        public async Task<IActionResult> ConsultarMovimentos(int mes, int ano)
        {
            try
            {
                var movimentos = await _movimentoService.ConsultarMovimentos(mes, ano);

                if (movimentos == null)
                    return NotFound(new { Error = "Nenhum movimento encontrado para os parâmetros informados." });

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
