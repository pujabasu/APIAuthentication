using Microsoft.AspNetCore.Authentication;

namespace ApiAuthenticationWithApiKey.Authentication
{
    public class ApiKeyOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "X-Api-Key";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
