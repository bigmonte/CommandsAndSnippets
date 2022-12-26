using System;

namespace  CommandsAndSnippets2.Auth
{
    public static class SessionConstants
    {
        
        /// <summary>
        /// Max age for the Session 
        /// </summary>
        public static TimeSpan DefaultSessionMaxAge = TimeSpan.FromHours(24);
        /// <summary>
        /// API User policy Key
        /// </summary>
        public const string ApiNamePolicy = "Bearer";
        public const string CookieNamePolicy = "Cookies";
        public const string SessionTokenHeaderName = "x-bw2-auth";
        public const string CookieDomain = "https://localhost:7022";
        
    }
}

