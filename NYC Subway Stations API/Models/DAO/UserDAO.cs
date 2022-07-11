using NYC_Subway_Stations_API.Helpers;
using NYC_Subway_Stations_API.Interface;
using NYC_Subway_Stations_API.Models.Request;
using System.Linq;

namespace NYC_Subway_Stations_API.Models.DAO
{
    public class UserDAO : IUser
    {
        private readonly SubwayStationsContext _context;

        public UserDAO(SubwayStationsContext context)
        {
            _context = context;
        }

        public User FindUserByEmail(string email)
        {
           return _context.User.Where(x => x.Email == email).FirstOrDefault();
        }

        public User FindUserByEmailPassword(string email, string password)
        {
            return _context.User.Where(u=>u.Email == email && u.Password == Encryptation.PasswordHash(password)).FirstOrDefault();
        }

        public User RegisterUser(RegisterRequest request)
        {
            User user = new User
            {
                Email = request.Email,
                Password = Encryptation.PasswordHash(request.Password),
                Name = request.Name
            };
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
