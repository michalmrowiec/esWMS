using esWMS.Domain.Entities.SystemActors;

namespace esWMS.Application.Contracts.Persistence
{
    public interface IContractorRepository
        : IBaseRepository<Contractor>, ISieve<Contractor>
    { }
}
