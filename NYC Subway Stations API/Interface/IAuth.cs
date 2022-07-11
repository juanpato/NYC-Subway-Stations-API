using NYC_Subway_Stations_API.Models;
using NYC_Subway_Stations_API.Models.Request;
using NYC_Subway_Stations_API.Models.Response;

namespace NYC_Subway_Stations_API.Interface
{
    public interface IAuth
    {
        TokenResponse Login(LoginRequest request);
        User RegisterUser(RegisterRequest request);
    }
}
