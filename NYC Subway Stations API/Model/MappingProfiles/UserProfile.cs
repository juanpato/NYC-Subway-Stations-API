using AutoMapper;
using NYC_Subway_Stations_API.Models.DTO;

namespace NYC_Subway_Stations_API.Models.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserDTO>();
        }
    }
}
