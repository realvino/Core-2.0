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
using System.Text;
using System.Threading.Tasks;
using tibs.stem.LeadStatuss.Dto;

namespace tibs.stem.LeadStatuss
{
    public class LeadStatusAppService : stemAppServiceBase, ILeadStatusAppService
    {
        private readonly IRepository<LeadStatus> _leadStatusRepository;
        
        public LeadStatusAppService(IRepository<LeadStatus> leadStatusRepository)
        {
            _leadStatusRepository = leadStatusRepository;
            
        }

        public ListResultDto<LeadStatusList> GetLeadStatus(GetLeadStatusInput input)
        {
            var leadstatus = _leadStatusRepository
                .GetAll();
            var query = leadstatus
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.LeadStatusCode.Contains(input.Filter) ||
                        u.LeadStatusName.Contains(input.Filter))
                .ToList();

            return new ListResultDto<LeadStatusList>(query.MapTo<List<LeadStatusList>>());
        }

        public async Task<GetLeadStatus> GetLeadStatusForEdit(EntityDto input)
        {
            var output = new GetLeadStatus
            {
            };

            var leadstatus = _leadStatusRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.leadstatus = leadstatus.MapTo<LeadStatusList>();
            return output;
        }


        public async Task CreateOrUpdateLeadStatus(CreateLeadStatusInput input)
        {
            if (input.Id != 0)
            {
                await UpdateLeadStatus(input);
            }
            else
            {
                await CreateLeadStatus(input);
            }
        }

        public async Task CreateLeadStatus(CreateLeadStatusInput input)
        {
            var leadstatus = input.MapTo<LeadStatus>();

            var val = _leadStatusRepository
             .GetAll().Where(p => p.LeadStatusCode == input.LeadStatusCode || p.LeadStatusName == input.LeadStatusName).FirstOrDefault();

            if (val == null)
            {
                await _leadStatusRepository.InsertAsync(leadstatus);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in LeadStatus Name '" + input.LeadStatusName + "' or LeadStatus Code '" + input.LeadStatusCode + "'...");
            }
        }


        public async Task UpdateLeadStatus(CreateLeadStatusInput input)
        {
            var leadstatus = await _leadStatusRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, leadstatus);

            var val = _leadStatusRepository
            .GetAll().Where(p => (p.LeadStatusCode == input.LeadStatusCode || p.LeadStatusName == input.LeadStatusName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _leadStatusRepository.UpdateAsync(leadstatus);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in LeadStatus Name '" + input.LeadStatusName + "' or LeadStatus Code '" + input.LeadStatusCode + "'...");
            }

        }
        public async Task DeleteLeadStatus(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 31);
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
                    throw new UserFriendlyException("Ooops!", "leadStatus cannot be deleted '");
                }
                else
                {
                    await _leadStatusRepository.DeleteAsync(input.Id);
                }
            }
        }

    }
}
