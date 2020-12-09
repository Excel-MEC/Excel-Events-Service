using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly IEnvironmentService _env;
        public CustomAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IEnvironmentService env) : base(options, logger, encoder, clock)
        {
            _env = env;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Request.Headers.ContainsKey("ServiceAuthorization"))
                return ServiceAuthenticator(Request.Headers["ServiceAuthorization"].ToString());
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.NoResult();
            string authorizationHeader = Request.Headers["Authorization"].ToString().Split(" ").Last();
            if (string.IsNullOrEmpty(authorizationHeader))
                return AuthenticateResult.NoResult();
            try
            {
                return AuthenticateUser(authorizationHeader);
            }
            catch (SecurityTokenExpiredException)
            {
                throw;
            }
            catch
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
        }

        private JwtSecurityToken ValidateToken(string token, byte[] key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _env.Issuer,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken) validatedToken;

        }
        private AuthenticateResult AuthenticateUser(string token)
        {
            var key = Encoding.ASCII.GetBytes(_env.AccessToken);
            var jwtToken = ValidateToken(token, key);
            var identity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
            var role = jwtToken.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToArray();
            var principal = new GenericPrincipal(identity,role);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }

        private AuthenticateResult ServiceAuthenticator(string serviceKey)
        {
            if (serviceKey != _env.ServiceKey)
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            var claims = new [] { new Claim("ServiceAccount", "true")};
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principle = new ClaimsPrincipal(identity);
            var token = new AuthenticationTicket(principle, Scheme.Name);
            return AuthenticateResult.Success(token);
        }
    }
}
