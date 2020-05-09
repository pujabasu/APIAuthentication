using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ApiAuthenticationWithApiKey.Authentication
{
    public class ApiKey
    {
        public ApiKey(string key, string owner, List<Claim> claims)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            OwnerName = owner ?? throw new ArgumentNullException(nameof(owner));
            Claims = claims ?? new List<Claim>();
        }

        public string Key { get; }
        public string OwnerName { get; }
        public IReadOnlyCollection<Claim> Claims { get; }
    }
}
