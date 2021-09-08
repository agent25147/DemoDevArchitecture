using System;
using System.Collections.Generic;

namespace Core.Utilities.Security.Jwt
{
    public class AccessToken : IAccessToken
    {
        public AccessToken()
        {
            SiteAccess = new List<string>();
        }
        public List<string> Claims { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> SiteAccess { get; set; }
    }
}
