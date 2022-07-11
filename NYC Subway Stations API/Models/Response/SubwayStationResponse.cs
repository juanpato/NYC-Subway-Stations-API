using System.ComponentModel.DataAnnotations;

namespace NYC_Subway_Stations_API.Models.Response
{
    public class SubwayStationResponse
    {
        [Required(ErrorMessage = "url required.")]
        public string url { get; set; }
        [Required(ErrorMessage = "objectid required.")]
        public string objectid { get; set; }
        [Required(ErrorMessage = "name required.")]
        public string name { get; set; }
        [Required(ErrorMessage = "the_geom required.")]
        public The_Geom_Response the_geom { get; set; }
        [Required(ErrorMessage = "line required.")]
        public string line { get; set; }
        [Required(ErrorMessage = "notes required.")]
        public string notes { get; set; }
    }

    public class The_Geom_Response
    {
        [Required(ErrorMessage = "type required.")]
        public string type { get; set; }
        [Required(ErrorMessage = "coordinates required.")]
        public float[] coordinates { get; set; }
    }
}
