using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Emaildomainss.Dto;

namespace tibs.stem.Emaildomainss
{
     
    public interface IEmaildomainAppService : IApplicationService
    {
        ListResultDto<EmaildomainList> GetEmaildomain(GetEmaildomainListInput input);
        Task<GetEmaildomain> GetEmaildomainForEdit(NullableIdDto input);
        Task CreateOrUpdateEmaildomain(CreateEmaildomainInput input);
        Task GetDeleteEmaildomain(EntityDto input);
        Task<FileDto> GetEmaildomainToExcel();

    }
}
