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
using tibs.stem.OpportunitySources;
using tibs.stem.OpportunitySourcess.Dto;

namespace tibs.stem.OpportunitySourcess
{
    public class OpportunitySourceAppService : stemAppServiceBase, IOpportunitySourceAppService
    {
        private readonly IRepository<OpportunitySource> _opportunitySourceRepository;

        public OpportunitySourceAppService(IRepository<OpportunitySource> opportunitySourceRepository)
        {
            _opportunitySourceRepository = opportunitySourceRepository;
        }
        public ListResultDto<OpportunitySourceList> GetOpportunitySource(GetOpportunitySourceInput input)
        {

            var query = _opportunitySourceRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Code.Contains(input.Filter) ||
                        u.Name.Contains(input.Filter) ||
                        u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<OpportunitySourceList>(query.MapTo<List<OpportunitySourceList>>()); ;
        }
        public async Task<GetOpportunitySource> GetOpportunitySourceForEdit(NullableIdDto input)
        {
            var output = new GetOpportunitySource
            {
            };

            var Source = _opportunitySourceRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.sources = Source.MapTo<OpportunitySourceInput>();

            return output;

        }
        public async Task CreateOrUpdateOpportunitySource(OpportunitySourceInput input)
        {
            if (input.Id != 0)
            {
                await UpdateOpportunitySource(input);
            }
            else
            {
                await CreateOpportunitySource(input);
            }
        }
        public async Task CreateOpportunitySource(OpportunitySourceInput input)
        {
            var Source = input.MapTo<OpportunitySource>();
            var val = _opportunitySourceRepository
             .GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();

            if (val == null)
            {
                await _opportunitySourceRepository.InsertAsync(Source);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' orName '" + input.Name + "'...");
            }
        }
        public async Task UpdateOpportunitySource(OpportunitySourceInput input)
        {
            var Source = input.MapTo<OpportunitySource>();

            var val = _opportunitySourceRepository
            .GetAll().Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _opportunitySourceRepository.UpdateAsync(Source);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' or Name  '" + input.Name + "'...");
            }

        }
        public async Task DeleteOpportunitySource(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 33);
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
                    throw new UserFriendlyException("Ooops!", "Opportunitysource cannot be deleted '");
                }
                else
                {
                    await _opportunitySourceRepository.DeleteAsync(input.Id);
                }
            }
        }

    }
}
