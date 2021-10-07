using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CustomerAPI.Authentication
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}