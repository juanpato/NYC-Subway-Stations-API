using System.ComponentModel.DataAnnotations;

namespace NYC_Subway_Stations_API.Models.Request
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required.")]
        public string Password { get; set; }
    }
}
