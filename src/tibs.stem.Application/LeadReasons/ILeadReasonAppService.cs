using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.LeadReasons.Dto;

namespace tibs.stem.LeadReasons
{
    public interface ILeadReasonAppService : IApplicationService
    {
        ListResultDto<LeadReasonList> GetLeadReason(GetLeadReasonInput input);
        Task<GetLeadReason> GetLeadReasonForEdit(EntityDto input);
        Task CreateOrUpdateLeadReason(CreateLeadReasonInput input);
        Task DeleteLeadReason(EntityDto input);
        Task<FileDto> GetLeadReasonToExcel();
    }
}
