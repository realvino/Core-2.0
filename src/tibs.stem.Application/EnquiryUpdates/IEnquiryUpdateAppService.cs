using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static tibs.stem.EnquiryUpdates.EnquiryUpdateAppService;

namespace tibs.stem.EnquiryUpdates
{
    public interface IEnquiryUpdateAppService : IApplicationService
    {
        int createORupdateInquiry(EnquiryUpdateInputDto input);
        Task createORupdateInquiryJunk(EnquiryJunkUpdateInputDto input);
        void ContactUpdate(ContactUpdateInputDto input);
        void EnquiryStatusUpdate(EnquiryStatusUpdateInput input);
        Task ReverseJunk(EnquiryJunkUpdateInputDto input);
        void GetUpdateQuotation(UpdateQuotationInput input);
        void QuotationStatusUpdate(QuotationStatusUpdateInput input);
        int CheckEnquiryStages(EnquiryStatusUpdateInput input);
        void CreateActivityDefault(EnquiryUpdateInputDto input);
        void UpdateEnquiryClosureDate(ClosureUpdateDateInput input);
        void ReverseClosed(NullableIdDto input);
    }
}
