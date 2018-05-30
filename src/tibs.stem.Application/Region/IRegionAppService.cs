using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Region.Dto;

namespace tibs.stem.Region
{
    public interface IRegionAppService : IApplicationService
    {
        ListResultDto<RegionList> GetRegion(RegionListInput input);
        Task<GetRegion> GetRegionForEdit(EntityDto input);
        Task CreateOrUpdateRegion(RegionInput input);
        Task GetDeleteRegionCity(EntityDto input);
        ListResultDto<RegionCityList> GetRegionCity(EntityDto input);
        Task AddRegionCity(RegionCityInput input);
        Task<FileDto> GetRegionToExcel();
        Task GetDeleteRegion(EntityDto input);
    }
}
