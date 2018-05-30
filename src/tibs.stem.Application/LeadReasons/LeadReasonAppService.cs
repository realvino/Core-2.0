using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.LeadReasons;
using tibs.stem.LeadReasons.Dto;
using tibs.stem.LeadReasons.Exporting;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace tibs.stem.leadreasons
{
    public class LeadReasonAppService : stemAppServiceBase, ILeadReasonAppService
    {
        private readonly IRepository<LeadReason> _leadreasonRepository;
        private readonly ILeadReasonListExcelExporter _leadReasonListExcelExporter;

        public LeadReasonAppService(IRepository<LeadReason> leadreasonRepository, ILeadReasonListExcelExporter leadReasonListExcelExporter)
        {
            _leadreasonRepository = leadreasonRepository;
            _leadReasonListExcelExporter = leadReasonListExcelExporter;
        }

        public ListResultDto<LeadReasonList> GetLeadReason(GetLeadReasonInput input)
        {
            var leadreasons = _leadreasonRepository
                .GetAll();
            var query = leadreasons
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.LeadReasonCode.Contains(input.Filter) ||
                        u.LeadReasonName.Contains(input.Filter))
                .ToList();

            return new ListResultDto<LeadReasonList>(query.MapTo<List<LeadReasonList>>());
        }

        public async Task<GetLeadReason> GetLeadReasonForEdit(EntityDto input)
        {
            var output = new GetLeadReason
            {
            };

            var leadreasons = _leadreasonRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.leadreasons = leadreasons.MapTo<LeadReasonList>();
            return output;
        }


        public async Task CreateOrUpdateLeadReason(CreateLeadReasonInput input)
        {
            if (input.Id != 0)
            {
                await UpdateLeadReason(input);
            }
            else
            {
                await CreateLeadReason(input);
            }
        }

        public async Task CreateLeadReason(CreateLeadReasonInput input)
        {
            var leadreasons = input.MapTo<LeadReason>();

            var val = _leadreasonRepository
             .GetAll().Where(p => p.LeadReasonCode == input.LeadReasonCode || p.LeadReasonName == input.LeadReasonName).FirstOrDefault();

            if (val == null)
            {
                await _leadreasonRepository.InsertAsync(leadreasons);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in LeadReason Name '" + input.LeadReasonName + "' or LeadReason Code '" + input.LeadReasonCode + "'...");
            }
        }


        public async Task UpdateLeadReason(CreateLeadReasonInput input)
        {
            var leadreasons = await _leadreasonRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, leadreasons);

            var val = _leadreasonRepository
            .GetAll().Where(p => (p.LeadReasonCode == input.LeadReasonCode || p.LeadReasonName == input.LeadReasonName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _leadreasonRepository.UpdateAsync(leadreasons);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in LeadReason Name '" + input.LeadReasonName + "' or LeadReason Code '" + input.LeadReasonCode + "'...");
            }

        }

        public async Task DeleteLeadReason(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 35);
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
                    throw new UserFriendlyException("Ooops!", "LeadReason cannot be deleted '");
                }
                else
                {
                    await _leadreasonRepository.DeleteAsync(input.Id);
                }
            }

        }
        public async Task<FileDto> GetLeadReasonToExcel()
        {
            var leadreasons = _leadreasonRepository.GetAll();

            var LeadReasonListDtos = (from a in leadreasons
                                    select new LeadReasonList
                                    {
                                        Id = a.Id,
                                        LeadReasonCode = a.LeadReasonCode,
                                        LeadReasonName = a.LeadReasonName
                                    });
            var order = await LeadReasonListDtos.OrderBy("LeadReasonName").ToListAsync();

            var LeadReasonListDtoss = order.MapTo<List<LeadReasonList>>();

            return _leadReasonListExcelExporter.ExportToFile(LeadReasonListDtoss);
        }
    }
}
