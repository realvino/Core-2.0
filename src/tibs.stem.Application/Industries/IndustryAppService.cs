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
using tibs.stem.Industries.Dto;
using tibs.stem.Industrys;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using tibs.stem.Industries.Exporting;
using Abp.Linq.Extensions;

namespace tibs.stem.Industries
{
    
    public class IndustryAppService : stemAppServiceBase, IIndustryAppService
    {
        private readonly IRepository<Industry> _IndustryRepository;
        private readonly IndustryListExcelExporter _IndustryListExcelExporter;

        public IndustryAppService(IRepository<Industry> industryRepository , IndustryListExcelExporter industryListExcelExporter)
        {
            _IndustryRepository = industryRepository;
            _IndustryListExcelExporter = industryListExcelExporter;
        }
        public ListResultDto<IndustryListDto> GetIndustry(GetIndustryInput input)
        {
            var query = _IndustryRepository
                .GetAll();
            var industrylist = query
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.IndustryCode.Contains(input.Filter) ||
                        u.IndustryName.Contains(input.Filter) 
                        )
                .ToList();

            var industryDto = (from a in industrylist select new IndustryListDto { Id = a.Id, IndustryCode = a.IndustryCode, IndustryName = a.IndustryName });

            var IndustryLists = industryDto.MapTo<List<IndustryListDto>>();

            return new ListResultDto<IndustryListDto>(IndustryLists);

        }

        public async Task<GetIndustry> GetIndustryForEdit(EntityDto input)
        {

            var output = new GetIndustry();
            var query = _IndustryRepository
                .GetAll().Where(p => p.Id == input.Id);

            if (query.Count() > 0)
            {
                var industryDto = (from a in query select new IndustryListDto { Id = a.Id, IndustryCode = a.IndustryCode, IndustryName = a.IndustryName }).FirstOrDefault();
                output = new GetIndustry
                {
                    Industrys = industryDto

                };
            }
            return output;
        }


        public async Task CreateOrUpdateIndustry(IndustryInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateIndustry(input);
            }
            else
            {
                await CreateIndustry(input);
            }
        }

        public async Task CreateIndustry(IndustryInputDto input)
        {
            var query = input.MapTo<Industry>();

            var industrylist = _IndustryRepository
             .GetAll().Where(p => p.IndustryCode == input.IndustryCode || p.IndustryName == input.IndustryName).FirstOrDefault();

            if (industrylist == null)
            {
                await _IndustryRepository.InsertAsync(query);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Industry  Name '" + input.IndustryName + "' or Industry  Code '" + input.IndustryCode + "'...");
            }
        }

        public async Task UpdateIndustry(IndustryInputDto input)
        {
            var query = await _IndustryRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, query);

            var val = _IndustryRepository
            .GetAll().Where(p => (p.IndustryCode == input.IndustryCode || p.IndustryName == input.IndustryName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _IndustryRepository.UpdateAsync(query);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Industry Name '" + input.IndustryName + "' or Industry Code '" + input.IndustryCode + "'...");
            }

        }

        public  int CreateNewIndustry(IndustryInputDto input)
        {
            int id = 0;
            var query = input.MapTo<Industry>();

            var industrylist = _IndustryRepository
             .GetAll().Where(p => p.IndustryCode == input.IndustryCode || p.IndustryName == input.IndustryName).FirstOrDefault();

            if (industrylist == null)
            {
                id = _IndustryRepository.InsertAndGetId(query);
            }
            else
            {
                id = industrylist.Id;
            }

            return id;
        }

        public async Task GetDeleteIndustry(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 29);
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
                    throw new UserFriendlyException("Ooops!", "Industry cannot be deleted '");

                }
            }
            else
            {
                await _IndustryRepository.DeleteAsync(input.Id);
            }

        }

        public async Task<FileDto> GetIndustryToExcel()
        {
            try
            {
                var query = _IndustryRepository.GetAll();
                var select = (from a in query select new IndustryListDto { Id = a.Id, IndustryCode = a.IndustryCode, IndustryName = a.IndustryName });
                var order = await select.OrderBy("IndustryName").ToListAsync();
                var list = order.MapTo<List<IndustryListDto>>();
                return _IndustryListExcelExporter.ExportToFile(list);
            }
            catch (Exception obj)
            {
                string dd = obj.Message.ToString();
                return null;

            }
        }
    }
}
