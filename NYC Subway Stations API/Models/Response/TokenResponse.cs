using System;

namespace NYC_Subway_Stations_API.Models.Response
{
    public class TokenResponse
    {
        public string Token { get; }
        public DateTime Expiration { get; }

        private TokenResponse(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }

        public static TokenResponse CreateFrom(string token, DateTime expiration)
        {
            return new TokenResponse(token, expiration);
        }

    }
}
