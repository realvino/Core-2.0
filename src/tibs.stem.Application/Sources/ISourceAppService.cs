using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Sources.Dto;

namespace tibs.stem.Sources
{
    public interface ISourceAppService : IApplicationService
    {
        ListResultDto<SourceListDto> GetSources(GetSourceInput input);
        Task<GetSources> GetSourceForEdit(EntityDto input);
        Task CreateOrUpdateSource(SourceInputDto input);
        Task GetDeleteSource(EntityDto input);
        bool GetMappedSource(EntityDto input);
        Task<FileDto> GetSourceToExcel();
    }
}