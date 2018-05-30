using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Countrys.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Countrys
{
   public interface ICountryAppService : IApplicationService
    {
        ListResultDto<CountryListDto> GetCountry(CountryInput input);
        Task<GetCountry> GetCountryForEdit(EntityDto input);
        Task CreateOrUpdateCountry(CountryInputDto input);
        bool GetMappedCountry(EntityDto input);

       // DataSet GetDeleteCountrys();
        Task GetDeleteCountry(EntityDto input);
    }
}
