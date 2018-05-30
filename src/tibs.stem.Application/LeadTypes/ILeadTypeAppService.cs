using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.LeadTypes.Dto;

namespace tibs.stem.LeadTypes
{
    public interface ILeadTypeAppService : IApplicationService
    {
        ListResultDto<LeadTypeList> GetLeadType(GetLeadTypeInput input);
        Task<GetLeadType> GetLeadTypeForEdit(EntityDto input);
        Task CreateOrUpdateLeadType(CreateLeadTypeInput input);
        Task GetDeleteLeadType(EntityDto input);
    }
}
