using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Industries.Dto;

namespace tibs.stem.Industries
{
    public interface IIndustryAppService : IApplicationService
    {
        ListResultDto<IndustryListDto> GetIndustry(GetIndustryInput input);
        Task<GetIndustry> GetIndustryForEdit(EntityDto input);
        Task CreateOrUpdateIndustry(IndustryInputDto input);
        Task GetDeleteIndustry(EntityDto input);
        int CreateNewIndustry(IndustryInputDto input);
        Task<FileDto> GetIndustryToExcel();
    }
}
