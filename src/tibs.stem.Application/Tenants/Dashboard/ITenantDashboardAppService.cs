using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tibs.stem.Inquirys.Dto;
using tibs.stem.Select2;
using tibs.stem.Tenants.Dashboard.Dto;

namespace tibs.stem.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();

        GetDashboardDataOutput GetDashboardData(GetDashboardDataInput input);

        GetSalesSummaryOutput GetSalesSummary(GetSalesSummaryInput input);

        GetWorldMapOutput GetWorldMap(GetWorldMapInput input);

        GetServerStatsOutput GetServerStats(GetServerStatsInput input);

        GetGeneralStatsOutput GetGeneralStats(GetGeneralStatsInput input);

        Task<GetDiscount> GetDiscountForEdit(NullableIdDto input);
        Task CreateOrUpdateDiscount(CreateDiscountInput input);
        Task<Array> GetLeadSummaryGraph(GraphInput input);
        Task<Array> GetLostReasonGraph(GraphInput input);
        Task<RecentInquiryClosureList> GetInquiryRecentClosure(RecentInquiryInput input);
        Task<RecentInquiryActivityList> GetInquiryRecentActivity(RecentInquiryInput input);
        List<SliderDataList> GetSalesExecutive(String datainput, bool IsSales);
        Task<SelectDResult> GetDashboardTeam();
        Task<GetSalesLeadList> GetSalesLeadSummary();
    }
}
