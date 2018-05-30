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
using tibs.stem.AttributeGroupDetails;
using tibs.stem.Authorization;
using tibs.stem.Dto;
using tibs.stem.ProductFamilys;
using tibs.stem.ProductGroups.Dto;
using tibs.stem.ProductGroups.Exporting;
using tibs.stem.Tenants.Dashboard;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using tibs.stem.ProdutSpecLinks;
using tibs.stem.ProductCategorys;

namespace tibs.stem.ProductGroups
{
    public class ProductGroupAppService : stemAppServiceBase, IProductGroupAppService
    {
        private readonly IRepository<ProductGroup> _productGroupRepository;
        private readonly IRepository<ProductFamily> _productFamilyRepository;
        private readonly IRepository<ProductGroupDetail> _ProductGroupDetailRepository;
        private readonly IRepository<AttributeGroupDetail> _AttributeGroupDetailRepository;
        private readonly IProductGroupListExcelExporter _productGroupListExcelExporter;
        private readonly IRepository<ProdutSpecLink> _ProdutSpecLinkRepository;
        private readonly IRepository<ProductCategory> _ProdutCategoryRepository;

        public ProductGroupAppService
            (
            IRepository<ProductGroup> productGroupRepository,
            IRepository<ProductFamily> productFamilyRepository,
            IRepository<ProductGroupDetail> ProductGroupDetailRepository,
            IRepository<AttributeGroupDetail> AttributeGroupDetailRepository,
            IProductGroupListExcelExporter productGroupListExcelExporter,
            IRepository<ProdutSpecLink> ProdutSpecLinkRepository,
            IRepository<ProductCategory> ProdutCategoryRepository
            )
        {
            _productGroupRepository = productGroupRepository;
            _productFamilyRepository = productFamilyRepository;
            _ProductGroupDetailRepository = ProductGroupDetailRepository;
            _AttributeGroupDetailRepository = AttributeGroupDetailRepository;
            _productGroupListExcelExporter = productGroupListExcelExporter;
            _ProdutSpecLinkRepository = ProdutSpecLinkRepository;
            _ProdutCategoryRepository = ProdutCategoryRepository;
        }

        public ListResultDto<ProductGroupListDto> GetProductGroup(GetProductGroupInput input)
        {
            var country = _productGroupRepository
                .GetAll();
            var query = country
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.ProductGroupName.Contains(input.Filter))
                .ToList();

            return new ListResultDto<ProductGroupListDto>(query.MapTo<List<ProductGroupListDto>>());
        }

        public async Task<GetProductGroup> GetProductGroupForEdit(EntityDto input)
        {
            var output = new GetProductGroup{};
            var groups = _productGroupRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            if (groups != null)
            {
                var family = (from r in _productFamilyRepository.GetAll()
                              where r.Id == groups.FamilyId
                              select new FamilyData
                              {
                                  Id = r.Id,
                                  Name = r.ProductFamilyName
                              }).FirstOrDefault();

               

                var querys = _ProductGroupDetailRepository.GetAll().Where(p => p.ProductGroupId == input.Id).OrderBy(r => r.OrderBy);

                var ProductGroupDetail = (from r in querys
                                          where r.ProductGroupId == input.Id
                                          select new ProductDetailDtos {
                                              Id = r.Id,
                                              GroupId = r.AttributeGroupId,
                                              GroupName = r.ProductAttributeGroups.AttributeGroupName
                                          }).ToArray();

                var SubListout = new List<GetProductDetailDto>();
                var ChildListout = new List<ProductDetailDto>();

                foreach (var pro in ProductGroupDetail)
                {
                        var productDeta = (from r in _AttributeGroupDetailRepository.GetAll()
                                           where r.AttributeGroupId == pro.GroupId
                                           select new ProductDetailDto
                                           {
                                               Id = r.Id,
                                               AttributeId = r.AttributeId,
                                               AttributeName = r.Attributes.AttributeName,
                                               AttributeCode = r.Attributes.AttributeCode,
                                               ImgPath = r.Attributes.Imageurl ?? "",
                                               Selected = false
                                           });


                    ChildListout = productDeta.ToList(); 
                    SubListout.Add(new GetProductDetailDto
                        {
                            Id = pro.GroupId,
                            rowId = pro.Id,
                            Name = pro.GroupName,
                            ProductDetails = productDeta.ToArray()
                        });
                }

               
                var datas = groups.MapTo<ProductGroupListDto>();
                datas.IsEditable = true;

                foreach (var data in ChildListout)
                {
                    var ProductSpecification = (from c in _ProdutSpecLinkRepository.GetAll().Where(p => p.AttributeId == data.AttributeId && p.ProductGroupId == input.Id) select c.ProductSpecificationId).FirstOrDefault();
                    if (ProductSpecification != null)
                    {
                        datas.IsEditable = false;
                        break;
                    }

                }
                output.ProductGroupDetails = SubListout.ToArray();
                output.FamilyDatas = family;
                if (groups.ProductCategoryId > 0)
                {
                    var category = (from r in _ProdutCategoryRepository.GetAll()
                                    where r.Id == groups.ProductCategoryId
                                    select new categoryData
                                    {
                                        Id = r.Id,
                                        Name = r.Name
                                    }).FirstOrDefault();
                    output.CategoryDatas = category;
                }
                
                output.productGroup = datas;

            }
            return output;
        }

        public async Task CreateOrUpdateProductGroup(ProductGroupInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateProductGroup(input);
            }
            else
            {
                await CreateProductGroup(input);
            }
        }

        public async Task CreateProductGroup(ProductGroupInputDto input)
        {
            var productGroup = input.MapTo<ProductGroup>();

            var val = _productGroupRepository
             .GetAll().Where(p =>  p.ProductGroupName == input.ProductGroupName).FirstOrDefault();

            if (val == null)
            {
                await _productGroupRepository.InsertAsync(productGroup);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in productGroup Name '" + input.ProductGroupName +"...");
            }
        }

        public async Task UpdateProductGroup(ProductGroupInputDto input)
        {
            var productGroup = input.MapTo<ProductGroup>();
            productGroup.LastModificationTime = DateTime.Now;

            var val = _productGroupRepository
            .GetAll().Where(p => (p.ProductGroupName == input.ProductGroupName) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _productGroupRepository.UpdateAsync(productGroup);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in productGroup Name '" + input.ProductGroupName + "'...");
            }
            await _productGroupRepository.UpdateAsync(productGroup);
        }

        public async Task CreateOrUpdateProductGroupDetail(CreateProductGroupDetailInput input)
        {
            if (input.Id != 0)
            {
                await UpdateProductGroupDetail(input);
            }
            else
            {
                await CreateProductGroupDetail(input);
            }
        }
        public async Task CreateProductGroupDetail(CreateProductGroupDetailInput input)
        {
            var ProductGrpDetail = input.MapTo<ProductGroupDetail>();

            var productmax = _ProductGroupDetailRepository.GetAll().Where(p => p.ProductGroupId == input.ProductGroupId);
            var maxorderby = productmax.Where(a => a.OrderBy == productmax.Max(x => x.OrderBy)).FirstOrDefault();
            if (maxorderby == null)
            {
                ProductGrpDetail.OrderBy = 0;
                ProductGrpDetail.ReturnBy = 0;
            }
            else
            {
                ProductGrpDetail.OrderBy = maxorderby.OrderBy + 1;
                ProductGrpDetail.ReturnBy = maxorderby.OrderBy + 1;
            }
            var val = _ProductGroupDetailRepository
             .GetAll().Where(p => p.AttributeGroupId == input.AttributeGroupId && p.ProductGroupId == input.ProductGroupId).FirstOrDefault();

            if (val == null)
            {
                await _ProductGroupDetailRepository.InsertAsync(ProductGrpDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This data already exixts '");
            }
        }
        public async Task UpdateProductGroupDetail(CreateProductGroupDetailInput input)
        {
            var ProductGrpDetail = input.MapTo<ProductGroupDetail>();
            var val = _ProductGroupDetailRepository
             .GetAll().Where(p => p.AttributeGroupId == input.AttributeGroupId && p.ProductGroupId == input.ProductGroupId && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                ProductGrpDetail.LastModificationTime = DateTime.Now;
                await _ProductGroupDetailRepository.UpdateAsync(ProductGrpDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This data already exixts '");
            }

        }
        public async Task GetDeleteGroupDetail(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 26);
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
                    throw new UserFriendlyException("Ooops!", "ProductGroupDetail cannot be deleted '");
                }
                else
                {
                    await _ProductGroupDetailRepository.DeleteAsync(input.Id);
                }
            }
        }
        public async Task GetDeleteProductGroup(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 14);
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
                    throw new UserFriendlyException("Ooops!", "Product Group cannot be deleted '");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                    {
                        SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                        sqlComm.Parameters.AddWithValue("@TableId", 3);
                        sqlComm.Parameters.AddWithValue("@Id", input.Id);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlComm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public async Task<FileDto> GetProductGroupToExcel()
        {

            var productgroup = _productGroupRepository.GetAll();
            var productgroups = (from a in productgroup
                                 select new ProductGroupListDto
                                 {
                                     Id = a.Id,
                                     ProductGroupName = a.ProductGroupName,
                                     AttributeData = a.AttributeData,
                                     FamilyId = a.FamilyId,

                                 });
            var order = await productgroups.OrderBy("ProductGroupName").ToListAsync();

            var ProductGroupListDtos = order.MapTo<List<ProductGroupListDto>>();

            return _productGroupListExcelExporter.ExportToFile(ProductGroupListDtos);
        }

    }
    public class FindDelete
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class FamilyData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class categoryData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}