using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NYC_Subway_Stations_API.Interface;
using NYC_Subway_Stations_API.Models;
using NYC_Subway_Stations_API.Models.Request;
using NYC_Subway_Stations_API.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NYC_Subway_Stations_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubwayStationsController : ControllerBase
    {
        private readonly IGateway _gateway;
        private readonly IMapper _mapper;
        private readonly ISubwayStation _subwayStation;

        public SubwayStationsController(IGateway gateway, ISubwayStation subwayStation, IMapper mapper)
        {
            _gateway = gateway;
            _subwayStation = subwayStation;
            _mapper = mapper;
        }

        [HttpGet("GetAllSubwayStationsAsync")]
        [ProducesResponseType(typeof(IEnumerable<SubwayStationResponse>),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllSubwayStationsAsync()
        {
            try
            {
                using (var response = await _gateway.GetAllSubwayStation())
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok( JsonConvert.DeserializeObject<IEnumerable<SubwayStationResponse>>(apiResponse));
                    }
                    return StatusCode((int)response.StatusCode, apiResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("SaveFrequentlyUserStationAsync")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> SaveFrequentlyUserStationAsync(SubwayStationRequest request)
        {
            try
            {
                int IdUser = getClaimIdUser(HttpContext.User);
                await _subwayStation.SaveFrequentlyUserStation(request, IdUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetFrequentlyUserStationAsync")]
        [ProducesResponseType(typeof(IEnumerable<SubwayStationResponse>),200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult GetFrequentlyUserStation()
        {
            try
            {
                int IdUser = getClaimIdUser(HttpContext.User);
                return Ok(_mapper.Map<List<SubwayStationResponse>>(_subwayStation.GetFrequentlyUserStation(IdUser)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Calculate the distance between two provided station. The result is in meters.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The distance is in meters between two subway stations</returns>
        [HttpPost("DistanceStations")]
        [ProducesResponseType(typeof(double),200)]
        [ProducesResponseType(400)]        
        public IActionResult DistanceStations(SubwayStationDistance request)
        {
            try
            {
                return Ok(_subwayStation.DistanceBetweenSubwayStation(request.Station1, request.Station2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private static int getClaimIdUser(ClaimsPrincipal user)
        {
            return user.Claims
                .Where(c => c.Type == ClaimTypes.NameIdentifier)
                .Select(c => int.Parse(c.Value)).First();
        }
    }
}
