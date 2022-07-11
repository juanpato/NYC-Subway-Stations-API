using System.ComponentModel.DataAnnotations;

namespace NYC_Subway_Stations_API.Models.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Name required.")]
        public string Name { get; set; }
    }
}
