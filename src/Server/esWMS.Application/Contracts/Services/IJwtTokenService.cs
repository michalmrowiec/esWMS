using esWMS.Domain.Entities.SystemActors;

namespace esWMS.Application.Contracts.Services
{
    public interface IJwtTokenService
    {
        public string GenerateJwt(Employee user);
    }
}
