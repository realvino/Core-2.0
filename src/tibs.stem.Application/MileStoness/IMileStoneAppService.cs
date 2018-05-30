using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.MileStoness.Dto;

namespace tibs.stem.MileStoness
{
    public interface IMileStoneAppService : IApplicationService
    {
        Task<PagedResultDto<MileStoneList>> GetMileStone(GetMileStoneListInput input);
        Task<GetMileStone> GetMileStoneForEdit(NullableIdDto input);
        Task CreateOrUpdateMileStone(CreateMileStoneInput input);
        Task GetDeleteMileStone(EntityDto input);
        Task CreateMileStoneStage(MileStoneDetailInput input);
        Task<FileDto> GetMileStoneToExcel();
        Task GetDeleteMileStoneDetail(EntityDto input);

    }
}
