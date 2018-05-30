using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.Ybafcos;
using tibs.stem.Ybafcoss.Dto;
using tibs.stem.Ybafcoss.Exporting;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace tibs.stem.Ybafcoss
{
    public class YbafcoAppService : stemAppServiceBase, IYbafcoAppService
    {
        private readonly IRepository<Ybafco> _ybafcoRepository;
        private readonly IYbafcoListExcelExporter _ybafcoListExcelExporter;
        public YbafcoAppService(IRepository<Ybafco> ybafcoRepository, IYbafcoListExcelExporter ybafcoListExcelExporter)
        {
            _ybafcoRepository = ybafcoRepository;
            _ybafcoListExcelExporter = ybafcoListExcelExporter;
        }

        public ListResultDto<YbafcoList> GetYbafco(GetYbafcoListInput input)
        {
            var query = _ybafcoRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.YbafcoCode.Contains(input.Filter) ||
                    p.YbafcoName.Contains(input.Filter)
                );
            var Ybafco = (from a in query
                          select new YbafcoList
                          {
                              Id = a.Id,
                              YbafcoCode = a.YbafcoCode,
                              YbafcoName = a.YbafcoName
                          });

            return new ListResultDto<YbafcoList>(Ybafco.MapTo<List<YbafcoList>>());
        }

        public async Task<GetYbafco> GetYbafcoForEdit(NullableIdDto input)
        {
            var output = new GetYbafco { };
            var query = _ybafcoRepository
               .GetAll().Where(p => p.Id == input.Id);

            
            if (query.Count() > 0)
            {
                var Ybafco = (from a in query
                                 select new YbafcoList
                                 {
                                     Id = a.Id,
                                     YbafcoCode = a.YbafcoCode,
                                     YbafcoName = a.YbafcoName
                                 }).FirstOrDefault();

                output = new GetYbafco
                {
                    ybafcoList = Ybafco
                };
            }
            return output;
        }
        
        public async Task CreateOrUpdateYbafco(CreateYbafcoInput input)
        {
            if (input.Id != 0)
            {
                await UpdateYbafcoAsync(input);
            }
            else
            {
                await CreateYbafcoAsync(input);
            }
        }

        public virtual async Task CreateYbafcoAsync(CreateYbafcoInput input)
        {
            var ybafco = input.MapTo<Ybafco>();
            var val = _ybafcoRepository
              .GetAll().Where(p => p.YbafcoName == input.YbafcoName || p.YbafcoCode == input.YbafcoCode).FirstOrDefault();

            if (val == null)
            {
                await _ybafcoRepository.InsertAsync(ybafco);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Ybafco Name '" + input.YbafcoName + "' or Ybafco Code '" + input.YbafcoCode + "'...");
            }
        }

        public virtual async Task UpdateYbafcoAsync(CreateYbafcoInput input)
        {
            var ybafco = await _ybafcoRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, ybafco);

            var val = _ybafcoRepository
             .GetAll().Where(p => (p.YbafcoName == input.YbafcoName || p.YbafcoCode == input.YbafcoCode) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _ybafcoRepository.UpdateAsync(ybafco);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Ybafco Name '" + input.YbafcoName + "' or Ybafco Code '" + input.YbafcoCode + "'...");
            }
        }
        public async Task GetDeleteYbafco(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 34);
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
                    throw new UserFriendlyException("Ooops!", "Ybafco cannot be deleted '");
                }
                else
                {
                    await _ybafcoRepository.DeleteAsync(input.Id);
                }
            }
        }

        public async Task<FileDto> GetYbafcoToExcel()
        {

            var ybafco = _ybafcoRepository.GetAll();

            var ybafcolist = (from a in ybafco
                              select new YbafcoList
                              {
                                  Id = a.Id,
                                  YbafcoCode = a.YbafcoCode,
                                  YbafcoName = a.YbafcoName
                              });
            var order = await ybafcolist.OrderBy("YbafcoName").ToListAsync();

            var Ybafcolistoutput = order.MapTo<List<YbafcoList>>();

            return _ybafcoListExcelExporter.ExportToFile(Ybafcolistoutput);
        }
    }
}
