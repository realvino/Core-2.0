using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.EnquiryStatusss.Dto;

namespace tibs.stem.EnquiryStatusss
{
    public interface IEnquiryStatusAppService : IApplicationService
    {
        ListResultDto<EnquiryStatusListDto> GetEnquiryStatus(GetEnquiryStatusInput input);
        Task<GetEnquiryStatus> GetEnquiryStatusForEdit(EntityDto input);
        Task CreateOrUpdateEnquiryStatus(EnquiryStatusInputDto input);
        Task GetDeleteEnquiryStatus(EntityDto input);
        Task<FileDto> GetEnquiryStatusToExcel();
    }
}
