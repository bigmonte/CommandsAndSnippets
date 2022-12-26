namespace   CommandsAndSnippets2.Users;

public static class Constants
{

    public const string CorsPolicyName = "_myAllowSpecificOrigins";

    /// <summary>
    /// JwtClaimIdentifiers
    /// </summary>
    public static class JwtClaimIdentifiers
    {
        public const string Rol = "rol", Id = "id";
    }

    /// <summary>
    /// JwtClaims
    /// </summary>
    public static class JwtClaims
    {
        /// <summary>
        /// JwtClaims.ApiAccess
        /// </summary>
        public const string ApiAccess = "api_access";
    }
    
    public static class CookieClaims
    {
        /// <summary>
        /// JwtClaims.ApiAccess
        /// </summary>
        public const string CookieAccess = "cookie_access";
    }
    
    
}