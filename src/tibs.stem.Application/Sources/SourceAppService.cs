using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Countrys;
using tibs.stem.Sources.Dto;
using tibs.stem.Tenants.Dashboard;
using tibs.stem.Dto;
using tibs.stem.Sources.Exporting;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.Sources
{
    public class SourceAppService : stemAppServiceBase, IApplicationService
    {
        private readonly IRepository<Source> _sourceRepository;
        private readonly ISourceListExcelExporter _sourceListExcelExporter;

        public SourceAppService(IRepository<Source> sourceRepository, ISourceListExcelExporter sourceListExcelExporter)
        {
            _sourceRepository = sourceRepository;
            _sourceListExcelExporter = sourceListExcelExporter;
        }
        public ListResultDto<SourceListDto> GetSources(GetSourceInput input)
        {
            var srcs = _sourceRepository
                .GetAll().WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.SourceCode.Contains(input.Filter) ||
                        u.SourceName.Contains(input.Filter)
                        );

            var source = (from a in srcs
                          select new SourceListDto
                          {
                              Id = a.Id,
                              SourceCode = a.SourceCode,
                              SourceName = a.SourceName,
                              TypeId = a.TypeId,
                              TypeName = a.SourceTypes.SourceTypeName,
                              ColorCode = a.ColorCode

                          });

            var SourceLists = source.MapTo<List<SourceListDto>>();

            return new ListResultDto<SourceListDto>(SourceLists);

        }

        public async Task<GetSources> GetSourceForEdit(EntityDto input)
        {

            var output = new GetSources();
            var src = _sourceRepository
                .GetAll().Where(p => p.Id == input.Id);

            if (src.Count() > 0)
            {
                var source = (from a in src select new SourceListDto { Id = a.Id, SourceCode = a.SourceCode, SourceName = a.SourceName, TypeId = a.TypeId, TypeName = a.SourceTypes.SourceTypeName, ColorCode = a.ColorCode }).FirstOrDefault();
                output = new GetSources
                {
                    Sources = source
                };
            }
            return output;
        }


        public async Task CreateOrUpdateSource(SourceInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateSource(input);
            }
            else
            {
                await CreateSource(input);
            }
        }

        public async Task CreateSource(SourceInputDto input)
        {
            var src = input.MapTo<Source>();

            var srcs = _sourceRepository
             .GetAll().Where(p => p.SourceName == input.SourceName || p.SourceCode == input.SourceName).FirstOrDefault();

            if (srcs == null)
            {
                await _sourceRepository.InsertAsync(src);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Source Name '" + input.SourceName + "' or Source Code '" + input.SourceCode + "'...");
            }
        }

        public async Task UpdateSource(SourceInputDto input)
        {
            var src = await _sourceRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, src);

            var val = _sourceRepository
            .GetAll().Where(p => (p.SourceCode == input.SourceCode || p.SourceName == input.SourceName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _sourceRepository.UpdateAsync(src);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in source Name '" + input.SourceName + "' or source Code '" + input.SourceCode + "'...");
            }

        }


        public async Task GetDeleteSource(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 7);
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
                    throw new UserFriendlyException("Ooops!", "Source cannot be deleted '");
                }
                else
                {
                    await _sourceRepository.DeleteAsync(input.Id);
                }
            }



        }

        public async Task<FileDto> GetSourceToExcel()
        {

            var source = _sourceRepository.GetAll();
            var sources = (from a in source
                           select new SourceListDto
                           {
                               Id = a.Id,
                               SourceCode = a.SourceCode,
                               SourceName = a.SourceName,
                               TypeId = a.TypeId,
                               TypeName = a.SourceTypes.SourceTypeName,
                               ColorCode = a.ColorCode

                           });

            var order = await sources.ToListAsync();

            var sourceListDtos = order.MapTo<List<SourceListDto>>();

            return _sourceListExcelExporter.ExportToFile(sourceListDtos);
        }
    }
    }