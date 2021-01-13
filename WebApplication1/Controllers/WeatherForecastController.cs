using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthService _auth;
        private readonly IUserRepository _userRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAuthService auth, IUserRepository userRepository)
        {
            _logger = logger;
            _auth = auth;
            _userRepository = userRepository;
        }

        [HttpPost("User")]
        public ActionResult<User> AddUser([FromBody] User user)
        {
            _userRepository.AddUser(user);
            _userRepository.Save();
            return CreatedAtRoute("GetUser", new { userId = user.Id }, user);
        }

        [HttpGet("User", Name = "GetUser")]
        public ActionResult<User> GetUser([FromQuery] Guid userId)
        {
            User user = _userRepository.GetUser(userId);

            return Ok(user);
        }
        [HttpPost("JWT")]
        public string GetJWT([FromBody] AuthModel auth)
        {
            
            string name = "albert";
            string email = "albert@gmail.com";

           // JWTService jWTService = new JWTService("TW9zaGVFcmV6UHJpdmF0ZUtleQ==");
            string token = _auth.GenerateToken(new JWTContainerModel
            {
                ExpireMinutes = 3600,
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email)
                }

            });

            return token;
        }

        [HttpPost("Validate")]
        public bool Validate([FromBody] ValidateModel token )
        {
           // JWTService jWTService = new JWTService("TW9zaGVFcmV6UHJpdmF0ZUtleQ==");
            return _auth.IsTokenValid(token.Token);
        } 

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
