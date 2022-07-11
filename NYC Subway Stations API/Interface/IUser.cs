using NYC_Subway_Stations_API.Models;
using NYC_Subway_Stations_API.Models.Request;

namespace NYC_Subway_Stations_API.Interface
{
    public interface IUser
    {
        User FindUserByEmailPassword(string email, string password);
        User RegisterUser(RegisterRequest request);
        User FindUserByEmail(string email);
    }
}
