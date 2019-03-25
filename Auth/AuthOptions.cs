using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ToursSoft.Auth
{
    /// <summary>
    /// Auth info
    /// </summary>
    public static class AuthOptions
    {
        /// <summary>
        /// token author
        /// </summary>
        public const string Issuer = "TourSoftServer";
        
        /// <summary>
        /// token customerD
        /// </summary>
        public const string Audience = "TourSoftServer";
        
        /// <summary>
        /// hash key
        /// </summary>
        private const string Key = "speci@l!3dominica987s0ft";
        
        /// <summary>
        /// lifespan token in minutes
        /// </summary>
        public const int Lifetime = 1440;
        
        /// <summary>
        /// Key encoding
        /// </summary>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}