using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Locations.Dto;

namespace tibs.stem.Locations
{
    public interface ILocationAppService : IApplicationService
    {
        Task<PagedResultDto<LocationListDto>> GetLocation(GetLocationInput input);
        Task<GetLocation> GetlocationForEdit(NullableIdDto<long> input);
        Task CreateOrUpdateLocation(LocationInputDto input);
       // bool GetMappedLocation(EntityDto input);
        Task GetDeleteLocation(EntityDto input);
        Task<FileDto> GetLocationToExcel();
    }
}
