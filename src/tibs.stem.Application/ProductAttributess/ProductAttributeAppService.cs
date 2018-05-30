using Abp.Application.Services.Dto;
using Abp.Authorization;
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
using tibs.stem.ProductAttributes;
using tibs.stem.ProductAttributess.Dto;
using tibs.stem.ProductAttributess.Exporting;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using tibs.stem.ProdutSpecLinks;

namespace tibs.stem.ProductAttributess
{
    public class ProductAttributeAppService : stemAppServiceBase, IProductAttributeAppService
    {
        private readonly IRepository<ProductAttribute> _productAttributeRepository;

        private readonly IProductAttributeListExcelExporter _productAttributeListExcelExporter;

        private readonly IRepository<ProdutSpecLink> _ProdutSpecLinkRepository;

        public ProductAttributeAppService(
            IRepository<ProductAttribute> productAttributeRepository, 
            IProductAttributeListExcelExporter productAttributeListExcelExporter,
            IRepository<ProdutSpecLink> ProdutSpecLinkRepository)
        {
            _productAttributeRepository = productAttributeRepository;
            _productAttributeListExcelExporter = productAttributeListExcelExporter;
            _ProdutSpecLinkRepository = ProdutSpecLinkRepository;
        }
        public ListResultDto<ProductAttributeListDto> GetProductAttribute(GetProductAttributeInput input)
        {

            var query = _productAttributeRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.AttributeCode.Contains(input.Filter) ||
                        u.AttributeName.Contains(input.Filter)).ToList();

            var data = query.MapTo<List<ProductAttributeListDto>>();
            foreach (var d in data)
            {
                d.IsEdit = true;
                var attGrpquery = _ProdutSpecLinkRepository.GetAll().Where(p => p.AttributeId == d.Id).ToList();
                var attGrpCount = attGrpquery.Count();
                if (attGrpCount > 0)
                    d.IsEdit = false;
            }


            return new ListResultDto<ProductAttributeListDto>(data);
        }
        public async Task<GetProductAttribute> GetProductAttributeForEdit(NullableIdDto input)
        {
            var output = new GetProductAttribute
            {
            };
            var attribute = _productAttributeRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.attribute = attribute.MapTo<CreateProductAttributeInput>();
            return output;
        }
        public async Task CreateOrUpdateProductAttribute(CreateProductAttributeInput input)
        {
            if (input.Id != 0)
            {
                await UpdateProductAttribute(input);
            }
            else
            {
                await CreateProductAttribute(input);
            }
        }
        public async Task CreateProductAttribute(CreateProductAttributeInput input)
        {
            var attribute = input.MapTo<ProductAttribute>();
            var val = _productAttributeRepository
             .GetAll().Where(p => p.AttributeCode == input.AttributeCode || p.AttributeName == input.AttributeName).FirstOrDefault();

            if (val == null)
            {
                await _productAttributeRepository.InsertAsync(attribute);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in AttributeCode '" + input.AttributeCode + "' or AttributeName '" + input.AttributeName + "'...");
            }
        }
        public async Task UpdateProductAttribute(CreateProductAttributeInput input)
        {
            var attribute = input.MapTo<ProductAttribute>();

            var val = _productAttributeRepository
            .GetAll().Where(p => (p.AttributeCode == input.AttributeCode || p.AttributeName == input.AttributeName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _productAttributeRepository.UpdateAsync(attribute);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in AttributeCode '" + input.AttributeCode + "' or AttributeName'" + input.AttributeName + "'...");
            }
            
        }

        public async Task GetDeleteProductAttribute(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 21);
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
                    throw new UserFriendlyException("Ooops!", "Product Attribute cannot be deleted '");
                }
                else
                {
                    await _productAttributeRepository.DeleteAsync(input.Id);
                }
            }
        }

        public async Task<FileDto> GetProductAttributeToExcel()
        {

            var query = _productAttributeRepository.GetAll();
            var Productattribute = (from a in query
                                    select new ProductAttributeListDto
                                    {
                                        Id = a.Id,
                                        AttributeCode = a.AttributeCode,
                                        AttributeName = a.AttributeName,
                                        Imageurl = a.Imageurl
                                    });

            var order = await Productattribute.OrderBy("AttributeName").ToListAsync();

            var ProductAttributeListDtos = order.MapTo<List<ProductAttributeListDto>>();

            return _productAttributeListExcelExporter.ExportToFile(ProductAttributeListDtos);
        }

    }
}
