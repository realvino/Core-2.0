
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Authorization;
using tibs.stem.Departments;
using tibs.stem.Depatments.Dto;
using tibs.stem.Depatments.Exporting;
using tibs.stem.Dto;
using tibs.stem.Tenants.Dashboard;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.Depatments
{
    public class DepartmentAppService : stemAppServiceBase, IDepartmentAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IDepartmentListExcelExporter _departmentListExcelExporter;
        public DepartmentAppService(IRepository<Department> departmentRepository, IDepartmentListExcelExporter departmentListExcelExporter)
        {
            _departmentRepository = departmentRepository;
            _departmentListExcelExporter = departmentListExcelExporter;
        }

       // [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Department)]
        public ListResultDto<DepartmentListDto> GetDepartment(DepartmentInput input)
        {
            var dept = _departmentRepository
                .GetAll();
            var query = dept
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.DepartmentCode.Contains(input.Filter) ||
                        u.DepatmentName.Contains(input.Filter))
                        .ToList();

            return new ListResultDto<DepartmentListDto>(query.MapTo<List<DepartmentListDto>>());
        }

        public async Task<GetDepartment> GetDepartmentForEdit(EntityDto input)
        {
            var output = new GetDepartment
            {
            };

            var persion = _departmentRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Departments = persion.MapTo<DepartmentListDto>();
            return output;
        }


        public async Task CreateOrUpdateDepartment(DepartmentInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateDepartments(input);
            }
            else
            {
                await CreateDepartments(input);
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Department_Create)]
        public async Task CreateDepartments(DepartmentInputDto input)
        {
            var dept = input.MapTo<Department>();
            await _departmentRepository.InsertAsync(dept);
        }

        // [AbpAuthorize(AppPermissions.Pages_Tenant_Geography_Department_Edit)]
        public async Task UpdateDepartments(DepartmentInputDto input)
        {
            var dept = await _departmentRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, dept);
            await _departmentRepository.UpdateAsync(dept);
        }

        public async Task GetDeleteDepartment(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 11);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
            }

            if (input.Id > 0)
            {
                var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
                if (results.Count() > 0)
                {
                    throw new UserFriendlyException("Ooops!", "Department cannot be deleted '");

                }
                else
                {
                    await _departmentRepository.DeleteAsync(input.Id);  
                }
            }

        }

        public bool GetMappedDepartment(EntityDto input)
        {
            bool ok = false;
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 11);
                sqlComm.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                {
                    da.Fill(ds);
                }
            }

            if (input.Id > 0)
            {
                var results = ds.Rows.Cast<DataRow>().Where(myRow => (int)myRow["Id"] == input.Id);
                if (results.Count() > 0)
                {
                    ok = true;
                }
            }
            return ok;
        }
        public async Task<FileDto> GetDepartmentToExcel()
        {
            var dept = _departmentRepository.GetAll();
            var select = (from a in dept
                          select new DepartmentListDto
                          {
                           Id = a.Id,
                           DepartmentCode = a.DepartmentCode,
                           DepatmentName = a.DepatmentName
            });
            var order = await select.OrderBy("DepatmentName").ToListAsync();
            var list = order.MapTo<List<DepartmentListDto>>();
            return _departmentListExcelExporter.ExportToFile(list);
        }
    }
    public class FindDelete
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}
