using System.ComponentModel.DataAnnotations;

namespace NYC_Subway_Stations_API.Models.Request
{
    public class SubwayStationDistance
    {
        [Required(ErrorMessage = "Station 1 required.")]        
        public SubwayStationRequest Station1 { get; set; }
        [Required(ErrorMessage = "Station 2 required.")]
        public SubwayStationRequest Station2 { get; set; }
    }
}
