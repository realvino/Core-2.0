using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Viewss.Dto;

namespace tibs.stem.Viewss
{
    public interface IViewAppService : IApplicationService
    {
        ListResultDto<ViewListDto> GetViews(ViewListInput input);
        Task<GetViews> GetViewForEdit(EntityDto input);
        Task CreateOrUpdateView(ViewInput input);
        Task GetDeleteView(EntityDto input);
        ListResultDto<ReportColumnListDto> GetReportColumn(GetReportColumnInput input);
        Task<GetReportColumn> GetReportColumnForEdit(NullableIdDto input);
        Task CreateOrUpdateReportColumn(ReportColumnInputDto input);
        Task DeleteReportColumn(EntityDto input);
    }
}
