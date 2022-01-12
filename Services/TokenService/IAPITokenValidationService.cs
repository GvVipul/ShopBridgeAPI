using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowBridge.Services.TokenService
{
    public interface IAPITokenValidationService
    {
        bool IsValid(string clientSecret);
    }
}
