
using AutoMapper;
using NYC_Subway_Stations_API.Models.Response;

namespace NYC_Subway_Stations_API.Models.MappingProfiles
{
    public class SubwayStationProfile : Profile
    {
        public SubwayStationProfile()
        {
            CreateMap<SubwayStation, SubwayStationResponse>()
            .ForPath(d => d.the_geom.type, opt => opt.MapFrom(src => src.TheGeomtype))
            .ForPath(d => d.the_geom.coordinates, opt => opt.MapFrom(src => new float[] { (float)src.TheGeomcoordinates0, (float)src.TheGeomcoordinates1 }));
        }
    }
}
