using System.Text.Json.Serialization;

namespace CustomerAPI.Authentication
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}