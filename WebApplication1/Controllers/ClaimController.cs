using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("/users/{userId}/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ClaimController(ILogger<UserController> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<ClaimDto> CreateClaimForUser(Guid userId, ClaimForCreationDto claim)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var claimEntity = _mapper.Map<Entities.Claim>(claim);
            _userRepository.AddClaim(userId, claimEntity);
            _userRepository.Save();

            var claimToReturn = _mapper.Map<ClaimDto>(claimEntity);
            return CreatedAtRoute("GetClaimForUser",
                new { userId = userId, claimId = claimToReturn.Id },
                claimToReturn);
        }


        [HttpGet("{claimId}", Name = "GetClaimForUser")]
        public ActionResult<ClaimDto> GetClaimForUser(Guid userId, Guid claimId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var claim = _userRepository.GetClaim(userId, claimId);

            if (claim == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ClaimDto>(claim));
        }
    }
}
