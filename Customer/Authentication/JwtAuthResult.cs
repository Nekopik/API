using System.Text.Json.Serialization;

namespace CustomerAPI.Authentication
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }

        public RefreshToken RefreshToken { get; set; }
    }
}