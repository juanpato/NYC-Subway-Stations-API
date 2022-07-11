using NYC_Subway_Stations_API.Models;
using NYC_Subway_Stations_API.Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NYC_Subway_Stations_API.Interface
{
    public interface ISubwayStation
    {
        Task<UserSubwayStation> SaveFrequentlyUserStation(SubwayStationRequest request, int idUser);
        Task<SubwayStation> AddSubwayStation(SubwayStation subwayStation);
        Task<UserSubwayStation> AddUserSubwayStation(UserSubwayStation subwayStation);
        UserSubwayStation FindUserStation(string idSubwayStation, int idUser);
        SubwayStation GetSubwayStationByObjectId(string objectId);        
        IEnumerable<SubwayStation> GetFrequentlyUserStation(int idUser);
        double DistanceBetweenSubwayStation(SubwayStationRequest s1, SubwayStationRequest s2);
    }

}
