using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("User")]
        public async Task<ActionResult<User>> AddUser([FromBody] UserForCreationDto user)
        {
          
            var userEntity = _mapper.Map<User>(user);
            _userRepository.AddUser(userEntity);
            await _userRepository.Save();
            var userDto = _mapper.Map<UserDto>(userEntity);
            return CreatedAtRoute("GetUser", new { userId = userEntity.Id }, user);
        }

        [HttpGet("Users", Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto);
            
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
            Policy = "Member")]
        [HttpGet("User", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] Guid userId)
        {
            User user = await _userRepository.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }


    }
}
