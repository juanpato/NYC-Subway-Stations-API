using Microsoft.Extensions.Configuration;
using NYC_Subway_Stations_API.Interface;
using System.Net.Http;
using System.Threading.Tasks;

namespace NYC_Subway_Stations_API.Service
{
    public class GatewayService : IGateway
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly IConfiguration _configuration;
        private string _gatewayName;

        public GatewayService(IHttpClientFactory httpClientFactory, IConfiguration Configuration)
        {
            _httpClient = httpClientFactory;
            _configuration = Configuration;
            _gatewayName = _configuration["SubwayApiName"];

        }
        public async Task<HttpResponseMessage> GetAllSubwayStation()
        {
            return await _httpClient.CreateClient(_gatewayName).GetAsync("");
        }
    }
}
