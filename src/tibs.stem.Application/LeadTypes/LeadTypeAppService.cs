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
using tibs.stem.LeadTypes.Dto;
using tibs.stem.LeadTypes.Dto.Exporting;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.LeadTypes
{
    public class LeadTypeAppService : stemAppServiceBase, ILeadTypeAppService
    {
        private readonly IRepository<LeadType> _leadTypeRepository;
        private readonly ILeadTypeListExcelExporter _leadTypeListExcelExporter;
        public LeadTypeAppService(IRepository<LeadType> leadTypeRepository, ILeadTypeListExcelExporter leadTypeListExcelExporter)
        {
            _leadTypeRepository = leadTypeRepository;
            _leadTypeListExcelExporter = leadTypeListExcelExporter;
        }

        public ListResultDto<LeadTypeList> GetLeadType(GetLeadTypeInput input)
        {
            var leadtypes = _leadTypeRepository
                .GetAll();
            var query = leadtypes
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.LeadTypeCode.Contains(input.Filter) ||
                        u.LeadTypeName.Contains(input.Filter))
                .ToList();

            return new ListResultDto<LeadTypeList>(query.MapTo<List<LeadTypeList>>());
        }

        public async Task<GetLeadType> GetLeadTypeForEdit(EntityDto input)
        {
            var output = new GetLeadType
            {
            };

            var leadtypes = _leadTypeRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.leadtypes = leadtypes.MapTo<LeadTypeList>();
            return output;
        }


        public async Task CreateOrUpdateLeadType(CreateLeadTypeInput input)
        {
            if (input.Id != 0)
            {
                await UpdateLeadType(input);
            }
            else
            {
                await CreateLeadType(input);
            }
        }

        public async Task CreateLeadType(CreateLeadTypeInput input)
        {
            var leadtypes = input.MapTo<LeadType>();

            var val = _leadTypeRepository
             .GetAll().Where(p => p.LeadTypeCode == input.LeadTypeCode || p.LeadTypeName == input.LeadTypeName).FirstOrDefault();

            if (val == null)
            {
                await _leadTypeRepository.InsertAsync(leadtypes);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in LeadType Name '" + input.LeadTypeName + "' or LeadType Code '" + input.LeadTypeCode + "'...");
            }
        }


        public async Task UpdateLeadType(CreateLeadTypeInput input)
        {
            var leadtypes = await _leadTypeRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, leadtypes);

            var val = _leadTypeRepository
            .GetAll().Where(p => (p.LeadTypeCode == input.LeadTypeCode || p.LeadTypeName == input.LeadTypeName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _leadTypeRepository.UpdateAsync(leadtypes);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in LeadType Name '" + input.LeadTypeName + "' or LeadType Code '" + input.LeadTypeCode + "'...");
            }

        }

        public async Task GetDeleteLeadType(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 27);
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
                    throw new UserFriendlyException("Ooops!", "Lead cannot be deleted '");
                }
                else
                {
                    await _leadTypeRepository.DeleteAsync(input.Id);
                }
            }
           
        }
        public async Task<FileDto> GetLeadTypeToExcel()
        {
            var leadtypes = _leadTypeRepository.GetAll();

            var LeadTypeListDtos = (from a in leadtypes
                                    select new LeadTypeList
                                    {
                                        Id = a.Id,
                                        LeadTypeCode = a.LeadTypeCode,
                                        LeadTypeName = a.LeadTypeName
                                    });
            var order = await LeadTypeListDtos.OrderBy("LeadTypeName").ToListAsync();

            var LeadTypeListDtoss = order.MapTo<List<LeadTypeList>>();

            return _leadTypeListExcelExporter.ExportToFile(LeadTypeListDtoss);
        }

    }
}
