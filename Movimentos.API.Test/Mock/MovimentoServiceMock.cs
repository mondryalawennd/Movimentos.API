using Moq;
using Movimentos.Business.Service.Interface;
using Movimentos.Entities.DTO;
using Movimentos.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.API.Test.Mock
{
    public static class MovimentoServiceMock
    {
        public static Mock<IMovimentoService> CriarMock()
        {
            var mock = new Mock<IMovimentoService>();

            // Mock InserirMovimento
            mock.Setup(s => s.InserirMovimento(It.IsAny<Movimento>()))
                .Returns(Task.CompletedTask);

            // Mock ConsultarMovimentos
            mock.Setup(s => s.ConsultarMovimentos())
                .ReturnsAsync(new List<MovimentoManualDTO>
                {
                    new MovimentoManualDTO
                    {
                        Mes = 8,
                        Ano = 2025,
                        CodigoProduto = "PROD001",
                        CodigoCosif = "COSIF001",
                        NumeroLancamento = 1,
                        DescricaoMovimento = "Movimento Teste",
                        ValorMovimento = 100.0M
                    }
                } as IEnumerable<MovimentoManualDTO>);

            return mock;
        }
    }
}