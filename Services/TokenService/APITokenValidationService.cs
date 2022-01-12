
using Microsoft.Extensions.Configuration;

namespace ShowBridge.Services.TokenService
{
    public class APITokenValidationService : IAPITokenValidationService
    {
        private readonly IConfiguration _configuration;
        public APITokenValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValid(string clientSecret)
        {
            if (_configuration["APITokenClientSecret"].Equals(clientSecret))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
