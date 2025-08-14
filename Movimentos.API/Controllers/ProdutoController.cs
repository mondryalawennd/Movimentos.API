using Microsoft.AspNetCore.Mvc;
using Movimentos.Business.Service.Interface;

namespace Movimentos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController: ControllerBase
    {

        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        }

        [HttpGet("CarregarProdutos")]
        public async Task<IActionResult> CarregarProdutos()
        {
            try
            {
                var produtos = await _produtoService.CarregarProdutos();

                if (produtos == null)
                    return NotFound(new { Error = "Nenhum produto encontrado para os parâmetros informados." });

                return Ok(produtos);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Error = "Erro inesperado ao consultar produtos." });
            }
        }
    }
}
