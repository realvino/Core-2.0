using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.EnquiryContactss.Dto;

namespace tibs.stem.EnquiryContactss
{
    public interface IEnquiryContactAppService : IApplicationService
    {
        ListResultDto<EnquiryContactListDto> GetEnquiryWiseEnquiryContact(NullableIdDto<long> input);
        Task CreateOrUpdateEnquiryContact(EnquiryContactInputDto input);
        Task GetDeleteEnquiryContact(EntityDto input);
    }
}
