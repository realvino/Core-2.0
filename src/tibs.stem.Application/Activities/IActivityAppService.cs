using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Activities.Dto;
using tibs.stem.Countrys.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Countrys
{
   public interface IActivityAppService : IApplicationService
    {
        ListResultDto<ActivityListDto> GetActivity(ActivityInput input);
        Task<GetActivity> GetActivityForEdit(EntityDto input);
        Task CreateOrUpdateActivity(ActivityInputDto input);
        //bool GetMappedActivityType(EntityDto input);
        Task GetDeleteActivityType(EntityDto input);
        Task<FileDto> GetActivityTypeToExcel();
    }
}
