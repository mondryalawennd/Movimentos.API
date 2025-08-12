using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimentos.CrossCutting.Auth.Interface
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username);
    }
}
