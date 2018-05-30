using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.QuotationStatuss.Dto;

namespace tibs.stem.QuotationStatuss
{
    public interface IQuotationStatusAppService: IApplicationService
    {
        ListResultDto<QuotationStatusList> GetQuotationStatus(GetQuotationStatusInput input);
        Task<GetQuotationStatus> GetQuotationStatusForEdit(NullableIdDto input);
        Task CreateOrUpdateQuotationStatus(QuotationStatusInput input);
        Task DeleteQuotationStatus(EntityDto input);
    }
}
