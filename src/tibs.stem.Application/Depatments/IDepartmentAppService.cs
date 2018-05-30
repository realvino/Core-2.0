using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using tibs.stem.Depatments.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Depatments
{
    public  interface IDepartmentAppService : IApplicationService
    {
        ListResultDto<DepartmentListDto> GetDepartment(DepartmentInput input);
        Task<GetDepartment> GetDepartmentForEdit(EntityDto input);
        Task CreateOrUpdateDepartment(DepartmentInputDto input);
        Task GetDeleteDepartment(EntityDto input);
        bool GetMappedDepartment(EntityDto input);
        Task<FileDto> GetDepartmentToExcel();
    }
}
