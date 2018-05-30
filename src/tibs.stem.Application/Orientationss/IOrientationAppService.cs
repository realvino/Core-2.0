using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using tibs.stem.Orientations.Dto;

namespace tibs.stem.Orientations
{
    public interface IOrientationAppService : IApplicationService
    {
        ListResultDto<OrientationListDto> GetOrientation(GetOrientationInput input);
        Task<GetOrientation> GetOrientationForEdit(EntityDto input);
        Task CreateOrUpdateOrientation(OrientationInputDto input);
        Task DeleteOrientation(EntityDto input);
    }
    
}
