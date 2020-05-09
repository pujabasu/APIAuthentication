using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ApiAuthenticationWithApiKey.Authentication
{
    public class ApiKeyOptionsHandler : AuthenticationHandler<ApiKeyOptions>
    {
        private const string ApiKeyHeader = "X-Api-Key";
        private IHttpContextAccessor _accessor;

        public ApiKeyOptionsHandler(
            IOptionsMonitor<ApiKeyOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IHttpContextAccessor accessor) : base(options, logger, encoder, clock)
        {
            _accessor = accessor;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeader, out var providedApiKeyHeaderValue))
            {
                return AuthenticateResult.NoResult();
            }

            if (string.IsNullOrEmpty(providedApiKeyHeaderValue))
            {
                return AuthenticateResult.NoResult();
            }

            var configuration = _accessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>(ApiKeyHeader);
            if (apiKey.Equals(providedApiKeyHeaderValue))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Api") };
                var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
                var identities = new List<ClaimsIdentity> { identity };
                var principal = new ClaimsPrincipal(identities);
                var ticket = new AuthenticationTicket(principal, Options.Scheme);

                return AuthenticateResult.Success(ticket);
            }
            return AuthenticateResult.Fail("Invalid Api Key provided");
        }
    }
}