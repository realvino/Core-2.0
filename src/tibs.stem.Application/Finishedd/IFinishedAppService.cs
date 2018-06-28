using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Finishedd.Dto;

namespace tibs.stem.Finishedd
{
    public interface IFinishedAppService : IApplicationService
    {
        Task<PagedResultDto<FinishedList>> GetFinished(GetFinishedInput input);
        Task<FinishedList> GetFinishedForEdit(NullableIdDto input);
        Task CreateOrUpdateFinished(FinishedInput input);
        Task GetDeleteFinished(EntityDto input);
        Task<List<FinishedDetailList>> GetFinishedDetail(NullableIdDto input);
        Task<FinishedDetailList> GetFinishedDetailForEdit(NullableIdDto input);
        Task CreateFinishedDetailAsync(FinishedDetailInput input);
        Task DeleteFinishedDetail(EntityDto input);
        
    }
}
