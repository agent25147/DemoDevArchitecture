using System;
using System.Collections.Generic;

namespace Core.Utilities.Security.Jwt
{
    public interface IAccessToken
    {
        DateTime Expiration { get; set; }
        string Token { get; set; }

        List<string> SiteAccess { get; }
    }
}