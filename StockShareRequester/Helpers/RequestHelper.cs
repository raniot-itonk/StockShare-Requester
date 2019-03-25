using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace StockShareRequester.Helpers
{
    public static class RequestHelper
    {

        public static (string, Guid) GetJwtFromHeader(HttpRequest request)
        {
            var keyValuePair = request.Headers.First(pair => pair.Key == "Authorization");
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(keyValuePair.Value.ToString().Replace("bearer ", "").Replace("Bearer ", ""));
                var id = Guid.Parse(jwtSecurityToken.Subject);
                return (keyValuePair.Value.ToString(), id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
