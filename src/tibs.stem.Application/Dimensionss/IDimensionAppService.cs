using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using tibs.stem.Dimensions.Dto;

namespace tibs.stem.Dimensions
{
   public interface IDimensionAppService : IApplicationService
    {
        ListResultDto<DimensionListDto> GetDimension(GetDimensionInput input);
        Task<GetDimension> GetDimensionForEdit(EntityDto input);
        Task CreateOrUpdateDimension(DimensionInputDto input);
        Task DeleteDimension(EntityDto input);
    }
}
