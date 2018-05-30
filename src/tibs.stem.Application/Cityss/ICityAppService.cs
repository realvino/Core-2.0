using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Citys.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Citys
{
    public interface ICityAppService : IApplicationService
    {
        Task<PagedResultDto<CityList>> GetCity(GetCItyListInput input);
        Task<GetCity> GetCityForEdit(NullableIdDto input);
        Task CreateOrUpdateCity(CreateCityInput input);
        bool GetMappedCity(EntityDto input);
        Task GetDeleteCity(EntityDto input);
        Task<FileDto> GetCityToExcel();
    }
}
