using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NYC_Subway_Stations_API.Interface;
using NYC_Subway_Stations_API.Models;
using NYC_Subway_Stations_API.Models.Request;
using NYC_Subway_Stations_API.Models.Response;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NYC_Subway_Stations_API.Service
{
    public class AuthService : IAuth
    {
        private readonly IUser _userDAO;
        private readonly IConfiguration _configuration;

        public AuthService(IUser user,IConfiguration configuration)
        {
            _userDAO = user;
            _configuration = configuration;
        }
        public TokenResponse Login(LoginRequest request)
        {
            User user = _userDAO.FindUserByEmailPassword(request.Email, request.Password);

            if (user != null)
            {
                var secretKey = _configuration.GetSection("JWT").GetValue<string>("SecretKey");
                var expiration = _configuration.GetSection("JWT").GetValue<int>("Expiration");
                DateTime expirationDate = DateTime.UtcNow.AddMinutes(expiration);
                string token = GenerateToken(secretKey, expirationDate, user);
                return TokenResponse.CreateFrom(token, expirationDate);
            }
            return null;
        }

        public User RegisterUser(RegisterRequest request)
        {
            if (_userDAO.FindUserByEmail(request.Email) != null)
                throw new Exception("User already registered.");
            return _userDAO.RegisterUser(request);
        }

        private string GenerateToken(string secretKey, DateTime expirationDate, User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(createdToken);
        }
    }
}
