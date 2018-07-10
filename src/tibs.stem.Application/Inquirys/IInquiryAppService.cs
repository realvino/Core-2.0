using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Companies.Dto;
using tibs.stem.Dto;
using tibs.stem.Inquirys.Dto;
using tibs.stem.Locations.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Inquirys
{
    public interface IInquiryAppService : IApplicationService
    {
        Task<PagedResultDto<InquiryListDto>> GetInquiry(GetInquiryInput input);
        Task<PagedResultDto<InquiryListDto>> GetLeadInquiry(GetInquiryInput input);
        Task<PagedResultDto<InquiryListDto>> GetSalesInquiry(GetInquiryInput input);
        Task<PagedResultDto<InquiryListDto>> GetJunkInquiry(GetInquiryInput input);
        Task<GetInquirys> GetInquiryForEdit(NullableIdDto input);
        Task<int> CreateOrUpdateInquiry(InquiryInputDto input);
        Task CreateOrUpdateLeadDetails(LeadDetailInputDto input);
        Task<Array> GetInquiryTickets(GetTicketInput input);
        Task<ListResultDto<EnqActList>> GetOverAllEnquiryActivitys(GetEnqActOverAllInput input);
        Task<ListResultDto<EnqActList>> GetEnquiryActivitys(GetEnqActInput input);
        Task CreateOrUpdateEnquiryActivitys(EnqActCreate input);
        Task<ListResultDto<EnqActCommentList>> GetEnqActComment(GetEnqActCommentInput input);
        Task CreateOrUpdateEnquiryActivitysComment(EnqActCommentCreate input);
        int NewLocationCreate(LocationInputDto input);
        int NewDesignationCreate(DesignationInputDto input);
        Task<GetEActivity> GetActivityForEdit(NullableIdDto input);
        int NewCompanyCreate(CompanyCreateInput input);
        Task<Array> GetSalesInquiryTickets(GetTicketInput input);
        Task GetDeleteInquiry(EntityDto input);
        Task GetDeleteEnquiryActivity(EntityDto input);
        Task GetCompanyUpdate(CompanyUpdateInput input);
        Task<FileDto> GetGeneralInquiryToExcel();
        Task<FileDto> GetSalesInquiryToExcel();
        Task<FileDto> GetLeadInquiryToExcel();
        Task<PagedResultDto<QuotationListDto>> GetEnquiryQuotations(GetInquiryQuotationInput input);
        bool CheckInquiryDuplicate(CheckInquiryInput input);
        Task CreateInquiryContactInfo(NullableIdDto input);
        ListResultDto<JobActivityList> GetJobActivity(NullableIdDto input);
        Task<GetJobActivity> GetJobActivityForEdit(NullableIdDto input);
        Task CreateOrUpdateJobActivity(CreateJobActivityInput input);
        Task GetDeleteJobActivity(EntityDto input);
        Task<PagedResultDto<QuotationListDto>> GetSalesQuotations(GetQuotationInput input);
        Task<PagedResultDto<JobActivityList>> GetOverallJobActivity(GetoverallJobActivityInput input);
        Task<int> CreateSalesInquiryInformation(InquiryInputDto input);
        Task CreateSalesInquiry(InquiryInputDto input);
        Task UpdateSalesmanAll(SalesmanChange input);
        Task CreateInquiryCompanyInfo(NullableIdDto input);
        Task<PagedResultDto<InquiryListDto>> GetClosedInquiry(GetInquiryInput input);
        Task<FileDto> GetClosedInquiryToExcel();
        Task<FileDto> GetSalesQuotationsToExcel();
        ListResultDto<CompanyEnquiryList> GetCompanyWiseInquiry(CompanyEnquiryInput input);
        Task<PagedResultDto<CompanyEnquiryList>> GetCompanyWiseInquiryGrid(CompanyInquiryInput input);
        Task InquiryDesignerApproval(EntityDto input);
        Task InquiryRevisionApproval(RevisionInput input);
        Task InquiryDesignerReject(EntityDto input);
        ListResultDto<NotificationListDto> GetRevisionNotifications();
        ListResultDto<NotificationListDto> GetSalesManagerNotifications();
    }
}
