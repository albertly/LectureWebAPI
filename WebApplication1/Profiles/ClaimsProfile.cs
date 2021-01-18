using AutoMapper;

namespace WebApplication1.Profiles
{
    public class ClaimsProfile : Profile
    {
        public ClaimsProfile()
        {
            CreateMap<Entities.Claim, Models.ClaimDto>();
            CreateMap<Models.ClaimForCreationDto, Entities.Claim>();
        }
    }
}
