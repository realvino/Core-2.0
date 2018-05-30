using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Collections;
using tibs.stem.ProductFamilys;
using tibs.stem.ProductFamilyss.Dto;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using tibs.stem.ProductFamilyss.Exporting;
using tibs.stem.Dto;
using System.Linq.Dynamic.Core;

namespace tibs.stem.ProductFamilyss
{
   public class ProductFamilyAppService : stemAppServiceBase, IProductFamilyAppService
    {
        private readonly IRepository<ProductFamily> _productFamilyRepository;
        private readonly IRepository<Collection> _CollectionRepository;
        private readonly IProductFamilyListExcelExporter _productFamilyListExcelExporter;


        public ProductFamilyAppService(
            IRepository<ProductFamily> productFamilyRepository,
            IRepository<Collection> CollectionRepository,
            IProductFamilyListExcelExporter productFamilyListExcelExporter

            )
        {
            _productFamilyRepository = productFamilyRepository;
            _CollectionRepository = CollectionRepository;
            _productFamilyListExcelExporter = productFamilyListExcelExporter;
        }
        public ListResultDto<ProductFamilyListDto> GetProductFamily(GetProductFamilyInput input)
        {

            var query = _productFamilyRepository.GetAll();

            query = query
                .Include(u => u.Collections)
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.ProductFamilyCode.Contains(input.Filter) ||
                        u.ProductFamilyName.Contains(input.Filter)||
                        u.Collections.CollectionName.Contains(input.Filter));

            var productFamily = (from a in query
                                 select new ProductFamilyListDto
                                 {
                                     Id = a.Id,
                                     ProductFamilyCode = a.ProductFamilyCode,
                                     ProductFamilyName = a.ProductFamilyName,
                                     Description = a.Description,
                                     Discount=a.Discount,
                                     CollectionId = a.CollectionId != null ? a.CollectionId :0,
                                     CollectionName = a.CollectionId != null ? a.Collections.CollectionName : "",
                                     Warranty = a.Warranty
                                 }).ToList();


            return new ListResultDto<ProductFamilyListDto>(productFamily.MapTo<List<ProductFamilyListDto>>());
        }


        public async Task<GetProductFamily> GetProductFamilyForEdit(NullableIdDto input)
        {
            var output = new GetProductFamily
            {
            };

            var productFamily = _productFamilyRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            if(productFamily != null)
            {
                var data = (from r in _CollectionRepository.GetAll() where r.Id == productFamily.CollectionId select new Collectiondata {
                    Id = r.Id,
                    Name = r.CollectionName
                }).FirstOrDefault();
                output.Collectiondatas = data;
            }

            output.productFamily = productFamily.MapTo<CreateProductFamilyInput>();

            return output;

        }
        public async Task CreateOrUpdateProductFamily(CreateProductFamilyInput input)
        {
            if (input.Id != 0)
            {
                await UpdateProductFamily(input);
            }
            else
            {
                await CreateProductFamily(input);
            }
        }

        public async Task CreateProductFamily(CreateProductFamilyInput input)
        {
            var productFamily = input.MapTo<ProductFamily>();
            var val = _productFamilyRepository
             .GetAll().Where(p => p.ProductFamilyCode == input.ProductFamilyCode || p.ProductFamilyName == input.ProductFamilyName).FirstOrDefault();

            if (val == null)
            {
                await _productFamilyRepository.InsertAsync(productFamily);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductFamilyCode '" + input.ProductFamilyCode + "' or ProductFamilyName '" + input.ProductFamilyName + "'...");
            }
        }
        public async Task UpdateProductFamily(CreateProductFamilyInput input)
        {
            var productFamily = input.MapTo<ProductFamily>();

            var val = _productFamilyRepository
            .GetAll().Where(p =>( p.ProductFamilyCode == input.ProductFamilyCode || p.ProductFamilyName == input.ProductFamilyName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _productFamilyRepository.UpdateAsync(productFamily);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductFamilyCode '" + input.ProductFamilyCode + "' or ProductFamilyName'" + input.ProductFamilyName + "'...");
            }
           
        }

        public async Task GetDeleteProductFamily(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 19);
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
                    throw new UserFriendlyException("Ooops!", "Product Family cannot be deleted '");
                }
                else
                {
                    await _productFamilyRepository.DeleteAsync(input.Id);
                }
            }
        }

        public async Task<FileDto> GetProductFamilyToExcel()
        {

            var query = _productFamilyRepository.GetAll();
            var productfamily = (from a in query
                                 select new ProductFamilyListDto
                                 {
                                     Id = a.Id,
                                     ProductFamilyCode = a.ProductFamilyCode,
                                     ProductFamilyName = a.ProductFamilyName,
                                     Description = a.Description,
                                     Discount = a.Discount,
                                     CollectionId = a.CollectionId ?? 0,
                                     CollectionName = a.CollectionId != null ? a.Collections.CollectionName : "",
                                     Warranty = a.Warranty

                                 });
            var order = await productfamily.OrderBy("ProductFamilyName").ToListAsync();

            var ProductFamilyListDtos = order.MapTo<List<ProductFamilyListDto>>();

            return _productFamilyListExcelExporter.ExportToFile(ProductFamilyListDtos);
        }

    }

    public class Collectiondata
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ProductFamilyListDto
    {
        public virtual int Id { get; set; }
        public virtual string ProductFamilyCode { get; set; }
        public virtual string ProductFamilyName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? Discount { get; set; }
        public virtual int? CollectionId { get; set; }
        public virtual string CollectionName { get; set; }
        public virtual int Warranty { get; set; }

    }
}
