using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions { }
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.NoResult());
            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            try
            {
                return Task.FromResult(ValidateToken(authorizationHeader));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
            }

        }

        private AuthenticateResult ValidateToken(string token)
        {
            var user = JsonSerializer.Deserialize<User>(token);
            var claims = new Claim[]
            {
                new Claim("user_id", user.user_id),
                new Claim("email", user.email),
                new Claim("Role", user.role)
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new GenericPrincipal(identity, user.role.Split(','));
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }

    }
}