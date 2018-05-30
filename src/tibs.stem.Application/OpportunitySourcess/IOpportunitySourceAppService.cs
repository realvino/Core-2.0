using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.OpportunitySourcess.Dto;

namespace tibs.stem.OpportunitySourcess
{
    public interface IOpportunitySourceAppService : IApplicationService
    {
        ListResultDto<OpportunitySourceList> GetOpportunitySource(GetOpportunitySourceInput input);
        Task<GetOpportunitySource> GetOpportunitySourceForEdit(NullableIdDto input);
        Task CreateOrUpdateOpportunitySource(OpportunitySourceInput input);
        Task DeleteOpportunitySource(EntityDto input);


    }
}
