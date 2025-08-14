using Microsoft.AspNetCore.Mvc;
using Movimentos.Business.Service;
using Movimentos.Business.Service.Interface;
using Movimentos.Entities.Entities;

namespace Movimentos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoCosifController: ControllerBase
    {
        private readonly ILogger<ProdutoCosifController> _logger;
        private readonly IProdutoCosifService _produtoService;

        public ProdutoCosifController(IProdutoCosifService produtoService, ILogger<ProdutoCosifController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        }

        [HttpGet("CarregarProdutoCosif/{codigoProduto}")]
        public async Task<IActionResult> CarregarProdutoCosif(string codigoProduto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(codigoProduto))
                {
                    return BadRequest(new { Sucesso = false, Mensagem = "Código do produto não pode ser vazio." });
                }

                var cosifs = await _produtoService.ConsultarProdutoCosif(codigoProduto);

                if (cosifs == null)
                    return NotFound(new { Error = "Nenhum produtoCosif encontrado para os parâmetros informados." });

                return Ok(cosifs);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Erro inesperado ao consultar produtosCosif." });
            }
        }


    }
}
