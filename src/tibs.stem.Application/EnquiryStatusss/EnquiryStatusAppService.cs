using Abp.Application.Services.Dto;
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
using tibs.stem.Dto;
using tibs.stem.EnquiryStatuss;
using tibs.stem.EnquiryStatusss.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using tibs.stem.EnquiryStatusss.Exporting;
using Abp.Linq.Extensions;

namespace tibs.stem.EnquiryStatusss  
{
    
    public class EnquiryStatusAppService : stemAppServiceBase, IEnquiryStatusAppService
    {
        private readonly IRepository<EnquiryStatus> _enqStatusRepository;
        private readonly EnquiryStatusListExcelExporter _EnquiryStatusListExcelExporter;


        public EnquiryStatusAppService(IRepository<EnquiryStatus> enqStatusRepository , EnquiryStatusListExcelExporter enquiryStatusListExcelExporter)
        {
            _enqStatusRepository = enqStatusRepository;
            _EnquiryStatusListExcelExporter = enquiryStatusListExcelExporter;
        }
        public ListResultDto<EnquiryStatusListDto> GetEnquiryStatus(GetEnquiryStatusInput input)
        {
            var query = _enqStatusRepository
                .GetAll();
            var enqStatuslist = query
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.EnqStatusCode.Contains(input.Filter) ||
                        u.EnqStatusName.Contains(input.Filter) ||
                        u.EnqStatusColor.Contains(input.Filter)
                        )
                .ToList();

            var enqStatusDto = (from a in enqStatuslist select new EnquiryStatusListDto {
                Id = a.Id,
                EnqStatusCode = a.EnqStatusCode,
                EnqStatusName = a.EnqStatusName,
                EnqStatusColor = a.EnqStatusColor,
                Percentage = a.Percentage,
                StagestateName = "",
                StagestateId = a.StagestateId ?? 0
            });

           var EnquiryStatusLists = enqStatusDto.MapTo<List<EnquiryStatusListDto>>();

            return new ListResultDto<EnquiryStatusListDto>(EnquiryStatusLists);

        }

        public async Task<GetEnquiryStatus> GetEnquiryStatusForEdit(EntityDto input)
        {

            var output = new GetEnquiryStatus();
            var query = _enqStatusRepository
                .GetAll().Where(p => p.Id == input.Id);

            if (query.Count() > 0)
            {
                var enqStatusDto = (from a in query select new EnquiryStatusListDto {
                    Id = a.Id,
                    EnqStatusCode = a.EnqStatusCode,
                    EnqStatusName = a.EnqStatusName,
                    EnqStatusColor = a.EnqStatusColor,
                    Percentage = a.Percentage,
                    StagestateId = a.StagestateId,
                    StagestateName = a.StagestateId != null ? a.Stagestatess.Name : ""
                }).FirstOrDefault();
                output = new GetEnquiryStatus
                {
                    EnquiryStatus = enqStatusDto

                };
            }
            return output;
        }


        public async Task CreateOrUpdateEnquiryStatus(EnquiryStatusInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateEnquiryStatus(input);
            }
            else
            {
                await CreateEnquiryStatus(input);
            }
        }

        public async Task CreateEnquiryStatus(EnquiryStatusInputDto input)
        {
            var query = input.MapTo<EnquiryStatus>();

            var enqStatuslist = _enqStatusRepository
             .GetAll().Where(p => p.EnqStatusCode == input.EnqStatusCode || p.EnqStatusName == input.EnqStatusName).FirstOrDefault();

            if (enqStatuslist == null)
            {
                await _enqStatusRepository.InsertAsync(query);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Enquiry Status Name '" + input.EnqStatusName + "' or Enquiry Status Code '" + input.EnqStatusCode + "'...");
            }
        }

        public async Task UpdateEnquiryStatus(EnquiryStatusInputDto input)
        {
            var query = await _enqStatusRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, query);

            var val = _enqStatusRepository
            .GetAll().Where(p => (p.EnqStatusCode == input.EnqStatusCode || p.EnqStatusName == input.EnqStatusName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _enqStatusRepository.UpdateAsync(query);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Enquiry Status Name '" + input.EnqStatusName + "' or Enquiry Status Code '" + input.EnqStatusCode + "'...");
            }

        }

        public async Task GetDeleteEnquiryStatus(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 28);
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
                    throw new UserFriendlyException("Ooops!", "status cannot be deleted '");

                }
                else
                {
                    await _enqStatusRepository.DeleteAsync(input.Id);
                }
            }

        }
        public async Task<FileDto> GetEnquiryStatusToExcel()
        {
            var query = _enqStatusRepository.GetAll();
            var select = (from a in query select new EnquiryStatusListDto { Id = a.Id, EnqStatusCode = a.EnqStatusCode, EnqStatusName = a.EnqStatusName, EnqStatusColor = a.EnqStatusColor });
            var order = await select.OrderBy("EnqStatusName").ToListAsync();
            var list = order.MapTo<List<EnquiryStatusListDto>>();
            return _EnquiryStatusListExcelExporter.ExportToFile(list);
        }
    }

}
