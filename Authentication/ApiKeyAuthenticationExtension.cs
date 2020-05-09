using Microsoft.AspNetCore.Authentication;
using System;

namespace ApiAuthenticationWithApiKey.Authentication
{
    public static class ApiKeyAuthenticationExtension
    {
        public static AuthenticationBuilder AddApiAuthentication(this AuthenticationBuilder builder, Action<ApiKeyOptions> options)
        {
            return builder.AddScheme<ApiKeyOptions, ApiKeyOptionsHandler>(ApiKeyOptions.DefaultScheme, options);
        }
    }
}
