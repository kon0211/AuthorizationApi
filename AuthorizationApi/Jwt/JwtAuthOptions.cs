using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AuthorizationApi
{
    public static class JwtAuthOptions
    {
        public const string ISSUER = "https://localhost:5235/";
        public const string AUDIENCE = "https://localhost:5235/";

        // TODO: move to secrets
        public const string KEY = "big secret key lfkg;lgkasdk'ds;fkldl;fksld;aG#$%G#@YUHGFADH@$ HEBREQGH53ohjrjogtop34jgj";

        public static readonly TimeSpan LIFETIME_MINUTES = TimeSpan.FromMinutes(60);

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
