using Microsoft.EntityFrameworkCore;
using NYC_Subway_Stations_API.Interface;
using NYC_Subway_Stations_API.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NYC_Subway_Stations_API.Models.DAO
{
    public class SubwayStationDAO : ISubwayStation
    {
        private readonly SubwayStationsContext _context;

        public SubwayStationDAO(SubwayStationsContext context)
        {
            _context = context;
        }
        public async Task<UserSubwayStation> SaveFrequentlyUserStation(SubwayStationRequest request, int idUser)
        {
            if (FindUserStation(request.objectid, idUser) != null)
                throw new Exception("Already saved frequently station");

            if (GetSubwayStationByObjectId(request.objectid) == null)
            {
                SubwayStation SubwayStation = new SubwayStation();
                SubwayStation.Url = request.url;
                SubwayStation.Objectid = request.objectid;
                SubwayStation.Name = request.name;
                SubwayStation.Line = request.line;
                SubwayStation.Notes = request.notes;
                SubwayStation.TheGeomcoordinates0 = (decimal)request.the_geom.coordinates[0];
                SubwayStation.TheGeomcoordinates1 = (decimal)request.the_geom.coordinates[1];
                SubwayStation.TheGeomtype = request.the_geom.type;

                await AddSubwayStation(SubwayStation);
            }
            UserSubwayStation userSubwayStation = new UserSubwayStation
            {
                IdSubwayStation = request.objectid,
                IdUser = idUser
            };
            return await AddUserSubwayStation(userSubwayStation);
            

        }

        public UserSubwayStation FindUserStation(string idSubwayStation, int idUser)
        {
           return _context.UserSubwayStation.Where(u => u.IdUser == idUser && u.IdSubwayStation == idSubwayStation).FirstOrDefault();
        }

        public SubwayStation GetSubwayStationByObjectId(string objectId)
        {
            return _context.SubwayStation.Where(s => s.Objectid == objectId).FirstOrDefault();
        }
        public async Task<UserSubwayStation> AddUserSubwayStation(UserSubwayStation subwayStation)
        {
            _context.UserSubwayStation.Add(subwayStation);
            await _context.SaveChangesAsync();
            return subwayStation;
        }
        public async Task<SubwayStation> AddSubwayStation(SubwayStation subwayStation)
        {
            _context.SubwayStation.Add(subwayStation);
            await _context.SaveChangesAsync();
            return subwayStation;
        }

        public IEnumerable<SubwayStation> GetFrequentlyUserStation(int idUser)
        {
            return _context.UserSubwayStation.Where(u => u.IdUser == idUser).Include(x => x.IdSubwayStationNavigation).Select(u=>u.IdSubwayStationNavigation).ToList();            
        }

        public double DistanceBetweenSubwayStation(SubwayStationRequest s1, SubwayStationRequest s2)
        {            
            return GetDistance(s1.the_geom.coordinates[0], s1.the_geom.coordinates[1], s2.the_geom.coordinates[0], s2.the_geom.coordinates[1]);
        }
        private double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
