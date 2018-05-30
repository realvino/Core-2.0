using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.LeadStatuss.Dto;

namespace tibs.stem.LeadStatuss
{
    public interface ILeadStatusAppService : IApplicationService
    {
        ListResultDto<LeadStatusList> GetLeadStatus(GetLeadStatusInput input);
        Task<GetLeadStatus> GetLeadStatusForEdit(EntityDto input);
        Task CreateOrUpdateLeadStatus(CreateLeadStatusInput input);
        Task DeleteLeadStatus(EntityDto input);
    }
}
