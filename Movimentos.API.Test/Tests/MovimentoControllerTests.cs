using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Movimentos.API.Controllers;
using Movimentos.API.Test.Mock;
using Movimentos.API.Test.Model;
using Movimentos.Business.Service.Interface;
using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;
using Newtonsoft.Json;
using Xunit;

namespace Movimentos.API.Test.Tests
{
    public class MovimentoControllerTests
    {
        private readonly MovimentoController _controller;
        private readonly Mock<IMovimentoService> _serviceMock;
        private readonly Mock<ILogger<MovimentoController>> _loggerMock;

        public MovimentoControllerTests()
        {
            _serviceMock = new Mock<IMovimentoService>();
            _loggerMock = new Mock<ILogger<MovimentoController>>();

            // Setup default mock para ConsultarMovimentos
            _serviceMock.Setup(s => s.ConsultarMovimentos())
                        .ReturnsAsync(new List<MovimentoManualDTO>
                        {
                            new MovimentoManualDTO
                            {
                                Mes = 8,
                                Ano = 2025,
                                CodigoProduto = "PROD001",
                                CodigoCosif = "COSIF001",
                                NumeroLancamento = 1,
                                DescricaoMovimento = "Teste",
                                ValorMovimento = 150
                            }
                        });

            _controller = new MovimentoController(_serviceMock.Object, _loggerMock.Object);
        }


        [Fact]
        public async Task InserirMovimento_DeveRetornarOk()
        {
            var dto = new MovimentoManualDTO
            {
                Mes = 8,
                Ano = 2025,
                CodigoProduto = "PROD001",
                CodigoCosif = "COSIF001",
                NumeroLancamento = 1,
                DescricaoMovimento = "Teste",
                ValorMovimento = 150
            };

            _serviceMock.Setup(s => s.InserirMovimento(It.IsAny<Movimento>()))
                        .Returns(Task.CompletedTask);

            var result = await _controller.InserirMovimento(dto) as OkObjectResult;

            Assert.NotNull(result);

            var json = JsonConvert.SerializeObject(result.Value);
            var response = JsonConvert.DeserializeObject<ResponseDTO>(json);

            Assert.NotNull(response);
            Assert.True(response.Sucesso);
            Assert.Equal("Movimento inserido com sucesso.", response.Mensagem);
        }

        [Fact]
        public async Task InserirMovimento_ModelStateInvalido_DeveRetornarBadRequest()
        {
            _controller.ModelState.AddModelError("Mes", "Campo obrigatório");
            var dto = new MovimentoManualDTO();

            var result = await _controller.InserirMovimento(dto) as BadRequestObjectResult;

            Assert.NotNull(result);

            var json = JsonConvert.SerializeObject(result.Value);
            var response = JsonConvert.DeserializeObject<ResponseDTO>(json);

            Assert.NotNull(response);
            Assert.False(response.Sucesso);
            Assert.Contains("Campo obrigatório", response.Erros);
        }

        [Fact]
        public async Task ConsultarMovimentos_DeveRetornarLista()
        {
            // Act
            var result = await _controller.ConsultarMovimentos() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var movimentos = result.Value as IEnumerable<MovimentoManualDTO>;
            Assert.NotNull(movimentos);
            Assert.Single(movimentos);
            Assert.Equal("PROD001", movimentos.First().CodigoProduto);
        }

        [Fact]
        public async Task ConsultarMovimentos_ServiceLancaException_DeveRetornar500()
        {
            // Arrange
            _serviceMock.Setup(s => s.ConsultarMovimentos())
                        .ThrowsAsync(new System.Exception("Erro inesperado"));

            // Act
            var result = await _controller.ConsultarMovimentos() as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }
    }
}