using GestionProfesores.Model.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace GestionProfesores.Api.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        readonly GestionProfesoresContext _dbContext;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, GestionProfesoresContext dbContext)
            : base(options, logger, encoder, clock)
        {
            _dbContext = dbContext;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync() => Task.FromResult(HandleAuthenticate());

        private AuthenticateResult HandleAuthenticate()
        {
            if (!RequestContainsAuthorizationHeader())
            {
                return AuthenticateResult.Fail("Authorization header not found");
            }
            try
            {
                var credentials = GetAuthenticationCredentials();
                var user = AuthenticationHelper.Login(_dbContext, GetUser(credentials), GetPassword(credentials));
                return GetUserAuthenticateResponse(user);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("An error ocurred while authenticating");
            }
        }

        private AuthenticateResult GetUserAuthenticateResponse(User user)
        {
            if (user == null)
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }

            return AuthenticateResult.Success(CreateSuccesAuthenticationTicket(user));
        }

        private AuthenticationTicket CreateSuccesAuthenticationTicket(User user)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationTicket(principal, Scheme.Name);
        }

        private string GetPassword(string[] credentials) => credentials[1];

        private string GetUser(string[] credentials) => credentials[0];

        string[] GetSplittedCredentials(string credentials) => credentials.Split(':');

        string[] GetAuthenticationCredentials()
        {
            var authHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(authHeaderValue.Parameter);
            return GetSplittedCredentials(Encoding.UTF8.GetString(bytes));
        }

        bool RequestContainsAuthorizationHeader() => Request.Headers.ContainsKey("Authorization");
    }
}