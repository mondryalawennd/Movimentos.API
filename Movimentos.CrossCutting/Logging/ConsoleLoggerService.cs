using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.CrossCutting.Logging
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG {DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }
    }
}
