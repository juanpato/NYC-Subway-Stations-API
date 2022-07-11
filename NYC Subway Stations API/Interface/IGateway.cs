using System.Net.Http;
using System.Threading.Tasks;

namespace NYC_Subway_Stations_API.Interface
{
    public interface IGateway
    {
        Task<HttpResponseMessage> GetAllSubwayStation();
    }
}
