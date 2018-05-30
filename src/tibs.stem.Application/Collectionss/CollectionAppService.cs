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
using tibs.stem.Collections;
using tibs.stem.Collectionss.Dto;
using tibs.stem.Collectionss.Exporting;
using tibs.stem.Dto;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
namespace tibs.stem.Collectionss
{
    public class CollectionAppService : stemAppServiceBase, ICollectionAppService
    {
        private readonly IRepository<Collection> _CollectionRepository;
        private readonly ICollectionListExcelExporter _collectionListExcelExporter;


        public CollectionAppService(
                    IRepository<Collection> CollectionRepository,
                    ICollectionListExcelExporter collectionListExcelExporter)
        {
            _CollectionRepository = CollectionRepository;
            _collectionListExcelExporter = collectionListExcelExporter;
        }
        public ListResultDto<CollectionListDto> GetCollection(GetCollectionInput input)
        {
            var Collection = _CollectionRepository.GetAll()

                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                       u.CollectionCode.Contains(input.Filter) ||
                       u.CollectionName.Contains(input.Filter) ||
                       u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<CollectionListDto>(Collection.MapTo<List<CollectionListDto>>());
        }

        public async Task<GetCollection> GetCollectionForEdit(NullableIdDto input)
        {
            var output = new GetCollection
            {
            };

            var Collection = _CollectionRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.Collections = Collection.MapTo<CollectionListDto>();
            return output;
        }

        public async Task CreateOrUpdateCollection(CreateCollectionInput input)
        {
            if (input.Id != 0)
            {
                await UpdateCollection(input);
            }
            else
            {
                await CreateCollection(input);
            }
        }

        public async Task CreateCollection(CreateCollectionInput input)
        {
            var collection = input.MapTo<Collection>();
            var val = _CollectionRepository
             .GetAll().Where(p => p.CollectionName == input.CollectionName || p.CollectionCode == input.CollectionCode).FirstOrDefault();

            if (val == null)
            {
                await _CollectionRepository.InsertAsync(collection);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in CollectionName '" + input.CollectionName + "' or CollectionCode '" + input.CollectionCode + "'...");
            }
        }

        public async Task UpdateCollection(CreateCollectionInput input)
        {
            var collection = input.MapTo<Collection>();
            var val = _CollectionRepository
             .GetAll().Where(p => (p.CollectionName == input.CollectionName || p.CollectionCode == input.CollectionCode) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _CollectionRepository.UpdateAsync(collection);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in CollectionName '" + input.CollectionName + "' or CollectionCode '" + input.CollectionCode + "'...");
            }

        }

        public async Task GetDeleteCollection(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 18);
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
                    throw new UserFriendlyException("Ooops!", "Collection cannot be deleted '");
                }
                else
                {
                    await _CollectionRepository.DeleteAsync(input.Id);
                }
            }
        }


        public async Task<FileDto> GetCollectionToExcel()
        {

            var Collection = _CollectionRepository.GetAll();
            var Collections = (from a in Collection
                               select new CollectionListDto
                               {
                                   Id = a.Id,
                                   CollectionCode = a.CollectionCode,
                                   CollectionName = a.CollectionName

                               });
            var order = await Collections.OrderBy("CollectionName").ToListAsync();

            var CollectionListDtos = order.MapTo<List<CollectionListDto>>();

            return _collectionListExcelExporter.ExportToFile(CollectionListDtos);
        }

      

    }
}
