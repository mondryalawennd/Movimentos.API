using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.API.Test.Model
{
    public class ResponseDTO
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public IEnumerable<string> Erros { get; set; } = Enumerable.Empty<string>();
    }
}