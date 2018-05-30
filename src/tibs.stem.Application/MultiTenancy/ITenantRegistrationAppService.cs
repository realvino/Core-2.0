using System.Threading.Tasks;
using Abp.Application.Services;
using tibs.stem.Editions.Dto;
using tibs.stem.MultiTenancy.Dto;

namespace tibs.stem.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}