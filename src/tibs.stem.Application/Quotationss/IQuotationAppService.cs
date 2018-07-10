using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Quotationss.Dto;

namespace tibs.stem.Quotationss
{
   public interface IQuotationAppService : IApplicationService
    {
        Task<PagedResultDto<QuotationListDto>> GetQuotation(GetQuotationInput input);
        Task<GetQuotation> GetQuotationForEdit(NullableIdDto input);
        Task<int> CreateOrUpdateQuotation(CreateQuotationInput input);
        Task GetDeleteQuotation(EntityDto input);
        Task<GetSection> GetSectionForEdit(NullableIdDto input);
        Task CreateOrUpdateSection(CreateSectionInput input);
        Task DeleteSection(EntityDto input);
        Task<Array> GetQuotationProduct(GetQuotationProductInput input);
        Task<GetQuotationProduct> GetQuotationProductForEdit(NullableIdDto input);
        Task CreateOrUpdateQuotationProduct(QuotationProductInput input);
        Task DeleteQuotationProduct(EntityDto input);
        Task GetProductImport(ImportQuotationInput input);
        Task<PagedResultDto<ImportHistoryList>> GetImportHistory(GetImportHistoryInput input);
        Task GetQuotationProductUnlock(ProductLinkInput input);
        Task GetExchangeProduct(ExchangeInput input);
        Task GetApproveProduct(EntityDto input);
        bool CheckQuotationIsOptional(NullableIdDto input);
        void UpdateQuotationOptional(int Id);
        Task UpdateQuotationWonorLost(UpdateQuotationInput input);
        ListResultDto<QuotationListDto> GetInquiryWiseQuotation(NullableIdDto input);
        Task<int> QuotationRevision(QuotationRevisionInput input);
        Task<PagedResultDto<QuotationListDto>> GetRevisedQuotation(NullableIdDto input);
        Task UpdateQuotationVatAmount(NullableIdDto input);
        Task SetDiscountForProducts(int TypeId,int QuotationId, Decimal NewDiscount);
        Task<FileDto> GetQuotationToExcel();
        Task<PagedResultDto<QuotationReportListDto>> GetTeamEnquiryReport(QuotationReportInput input);
        Task SendDiscountMail(NullableIdDto input);
        Task<FileDto> GetQuotationInquiryFilterToExcel(NullableIdDto input);
        Task<FileDto> GetTeamEnquiryReportExcel(NullableIdDto input);
        Task<FileDto> GetTeamReportExcel(NullableIdDto input);
        Task<FileDto> GetAllTeamReportExcel();
        Task QuotationRevaluation(QuotationRevaluationInput input);
        Task SendLostMail(int id, int? CompatitorId, int? ReasonId, string ReasonRemark);
    }

}
