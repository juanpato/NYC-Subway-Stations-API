using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NYC_Subway_Stations_API.Interface;
using NYC_Subway_Stations_API.Models;
using NYC_Subway_Stations_API.Models.DTO;
using NYC_Subway_Stations_API.Models.Request;
using NYC_Subway_Stations_API.Models.Response;
using System;

namespace NYC_Subway_Stations_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuth _auth;
        private readonly IMapper _mapper;

        public AuthController(IConfiguration configuration, IAuth auth,IMapper mapper)
        {
            _configuration = configuration;
            _auth = auth;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponse), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                TokenResponse response = _auth.Login(request);

                if (response != null)
                    return Ok(response);
                return NotFound("User not found.");
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Register(RegisterRequest request)
        {
            try
            {
                
                UserDTO user = _mapper.Map<UserDTO>(_auth.RegisterUser(request));
                if (user != null)
                    return Ok(user);

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
