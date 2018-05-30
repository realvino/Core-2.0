using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Products.Dto;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using tibs.stem.ProductSubGroups;
using tibs.stem.ProductGroups;
using tibs.stem.ProductSpecificationss.Dto;
using tibs.stem.ProductSpecifications;
using tibs.stem.ProductSpecificationDetails;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.AttributeGroupDetails;
using tibs.stem.ProductAttributes;
using tibs.stem.ProductAttributeGroupss.Dto;
using Newtonsoft.Json;
using System.Collections;
using tibs.stem.ProdutSpecLinks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using tibs.stem.ProductImageUrls;
using System.Data;
using System.Data.SqlClient;
using tibs.stem.Dto;
using tibs.stem.Products.Exporting;
using tibs.stem.Quotationss.Dto;
using tibs.stem.Quotations;
using tibs.stem.QuotationProducts;
using tibs.stem.ProductCategorys;
using tibs.stem.TemporaryProducts;

namespace tibs.stem.Products
{
    public class ProductAppService : stemAppServiceBase, IProductAppService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductSubGroup> _productsubGroupRepository;
        private readonly IRepository<ProductGroup> _productGroupRepository;
        private readonly IRepository<ProductPricelevel> _ProductPricelevelRepository;
        private readonly IRepository<ProductSpecification> _ProductSpecificationRepository;
        private readonly IRepository<ProductSpecificationDetail> _ProductSpecificationDetailRepository;
        private readonly IRepository<ProductAttributeGroup> _AttributeGroupRepository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<AttributeGroupDetail> _AttributeGroupDetailRepository;
        private readonly IRepository<ProdutSpecLink> _ProdutSpecLinkRepository;
        private readonly IRepository<ProductImageUrl> _ProductImageRepository;
        private readonly IRepository<QuotationProduct> _quotationProductRepository;
        private readonly IProductListExcelExporter _productListExcelExporter;
        private readonly IRepository<ProductCategory> _ProductCategoryRepository;
        private readonly IRepository<TemporaryProduct> _TempProductRepository;
        private readonly IRepository<TemporaryProductImage> _TempProductImageRepository;

        public ProductAppService
            (
            IRepository<Product> productRepository,
            IRepository<ProdutSpecLink> ProdutSpecLinkRepository,
            IRepository<ProductSubGroup> productsubGroupRepository,
            IRepository<ProductGroup> productGroupRepository,
            IRepository<ProductImageUrl> ProductImageRepository,
            IRepository<ProductPricelevel> ProductPricelevelRepository,
            IRepository<ProductSpecification> ProductSpecificationRepository,
            IRepository<ProductSpecificationDetail> ProductSpecificationDetailRepository,
            IRepository<ProductAttributeGroup> attributeGroupRepository,
            IRepository<ProductAttribute> productAttributeRepository,
            IRepository<AttributeGroupDetail> AttributeGroupDetailRepository,
            IProductListExcelExporter productListExcelExporter,
            IRepository<QuotationProduct> quotationProductRepository,
            IRepository<ProductCategory> ProductCategoryRepository,
            IRepository<TemporaryProduct> TempProductRepository,
            IRepository<TemporaryProductImage> TempProductImageRepository
            )
        {
            _ProductCategoryRepository = ProductCategoryRepository;
            _TempProductRepository = TempProductRepository;
            _productRepository = productRepository;
            _productsubGroupRepository = productsubGroupRepository;
            _productGroupRepository = productGroupRepository;
            _ProductPricelevelRepository = ProductPricelevelRepository;
            _ProductSpecificationRepository = ProductSpecificationRepository;
            _ProductSpecificationDetailRepository = ProductSpecificationDetailRepository;
            _AttributeGroupRepository = attributeGroupRepository;
            _productAttributeRepository = productAttributeRepository;
            _AttributeGroupDetailRepository = AttributeGroupDetailRepository;
            _ProdutSpecLinkRepository = ProdutSpecLinkRepository;
            _ProductImageRepository = ProductImageRepository;
            _productListExcelExporter = productListExcelExporter;
            _quotationProductRepository = quotationProductRepository;
            _TempProductImageRepository = TempProductImageRepository;
        }
        public async Task<PagedResultDto<ProductList>> GetProduct(GetProductInput input)
        {
            var query = _productRepository.GetAll();

            query = query
               .Include(u => u.ProductSpecifications)
               .WhereIf(
               !input.Filter.IsNullOrEmpty(),
               p => p.ProductCode.Contains(input.Filter) ||
                    p.SuspectCode.Contains(input.Filter) ||
                    p.Gpcode.Contains(input.Filter) ||
                    p.ProductSpecifications.Name.Contains(input.Filter)
                    );


            var productCount = await query.CountAsync();

            var product = from a in query
                          join ur in UserManager.Users on a.CreatorUserId equals ur.Id
                          select new ProductList
                          {
                              Id = a.Id,
                              ProductCode = a.ProductCode,
                              ProductName = a.ProductName,
                              SuspectCode = a.SuspectCode,
                              Gpcode = a.Gpcode,
                              Description = a.Description,
                              ProductSpecificationId = a.ProductSpecificationId,
                              ProductSpecificationName = a.ProductSpecificationId > 0 ? a.ProductSpecifications.Name : "",
                              Price = a.Price,
                              ScreationTime = a.CreationTime.ToString(),
                              DCreationTime = a.CreationTime,
                              Width = a.Width,
                              Height = a.Height,
                              Depth = a.Depth,
                              Dimention = "",
                              ProductStateId = a.ProductStateId,
                              ProductState = a.ProductState.Name,
                              BafcoMade = a.ProductSpecificationId > 0 ? a.ProductSpecifications.BafcoMade : false,
                              CategoryName = a.ProductSpecificationId > 0 ? (a.ProductSpecifications.ProductGroups.ProductCategoryId > 0 ? a.ProductSpecifications.ProductGroups.ProductCategorys.Name : "") : "",
                              CreatedBy = ur != null ? ur.UserName : "",
                              CreatorUserId = a.CreatorUserId ?? 0,
                              LastModifierUserId = a.LastModifierUserId ?? 0,
                              LastModifiedBy = a.LastModifierUserId != null ? (from l in UserManager.Users where l.Id == a.LastModifierUserId select l.UserName).FirstOrDefault() : "",

                          };
            var productlt = await product
             .OrderByDescending(p => p.DCreationTime)
             .PageBy(input)
             .ToListAsync();

            if (input.Sorting != "ProductCode,ProductSpecificationName,SuspectCode,Gpcode,Price,CategoryName,BafcoMade")
            {
                productlt = await product
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();
            }

            var productlistoutput = productlt.MapTo<List<ProductList>>();

            foreach (var data in productlistoutput)
            {
                var width = "";
                var depth = "";
                var height = "";

                if (data.Width > 0)
                    width = data.Width.ToString() + " - ";
                if (data.Depth > 0)
                    depth = data.Depth.ToString() + " - ";
                if (data.Height > 0)
                    height = data.Height.ToString();

                data.Dimention = width + "" + depth + "" + height;

                var quotaion = _quotationProductRepository.GetAll().Where(p => p.ProductId == data.Id).FirstOrDefault();
                if (quotaion != null)
                {
                    data.IsQuotation = true;
                }
            }

            return new PagedResultDto<ProductList>(productCount, productlistoutput);

        }
        public Array GetProductSpecificationDetails(EntityDto input)
        { 
            var ProductSpecificationDetail = (from r in _ProductSpecificationDetailRepository.GetAll()
                                              where r.ProductSpecificationId == input.Id
                                              select new ProductSpecificationDetailList
                                              {
                                                  Id = r.Id,
                                                  ProductSpecificationName = r.ProductSpecifications.Name,
                                                  AttributeGroupId = r.AttributeGroupId,
                                                  AttributeGroupName = r.ProductAttributeGroups.AttributeGroupName,
                                                  AttributeGroupCode = r.ProductAttributeGroups.AttributeGroupCode,
                                              }).ToArray();

            string Serializeddata = JsonConvert.SerializeObject(ProductSpecificationDetail);
            ProductSpecificationDetailList[] deserializeddata = JsonConvert.DeserializeObject<ProductSpecificationDetailList[]>(Serializeddata);

            var SubListout = new List<GetProductSpecificationListDto>();


            foreach (var groups in ProductSpecificationDetail)
            {

                SubListout.Add(new GetProductSpecificationListDto
                {
                    Id = groups.AttributeGroupId,
                    Name = groups.AttributeGroupName,
                    AttributeGroups = (from r in _AttributeGroupDetailRepository.GetAll()
                                       where r.AttributeGroupId == groups.AttributeGroupId
                                       select new AttributeGroupDetailListDto
                                       {
                                           AttributeGroupId = r.AttributeGroupId,
                                           AttributeGroupName = r.AttributeGroups.AttributeGroupName,
                                           AttributeId = r.AttributeId,
                                           AttributeName = r.Attributes.AttributeName,
                                           AttributeCode = r.AttributeGroups.AttributeGroupCode,
                                           ImgPath = r.Attributes.Imageurl,
                                           Selected = false
                                       }).ToArray()
                });

            }
            return SubListout.ToArray();
        }
        public async Task<GetProduct> GetProductForEdit(NullableIdDto input)
        {
            var output = new GetProduct
            {
            };
            var Groups = new GetProductGroupss();
            var lead = _productRepository.GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            if (lead != null)
            {
                
                var Images = (from c in _ProductImageRepository.GetAll()
                              where c.ProductId == input.Id
                              select new ProductImages
                              {
                                  Id = c.Id,
                                  ImageUrl = c.ImageUrl
                              }).ToArray();

                output.Images = Images;
              
                output.ProductLists = (from a in _productRepository.GetAll() where a.Id == input.Id
                                       select new ProductList
                                       {
                                           Id = a.Id,
                                           ProductCode = a.ProductCode,
                                           ProductName = a.ProductName,
                                           SuspectCode = a.SuspectCode,
                                           Gpcode = a.Gpcode,
                                           Description = a.Description,
                                           ProductSpecificationId = a.ProductSpecificationId,
                                           ProductSpecificationName = a.ProductSpecifications.Name,
                                           Price = a.Price,
                                           Width = a.Width,
                                           Height = a.Height,
                                           Depth = a.Depth,
                                           ProductStateId = a.ProductStateId,
                                           ProductState = a.ProductState.Name
                                       }).FirstOrDefault();

                var quotaion = _quotationProductRepository.GetAll().Where(p => p.ProductId == input.Id).FirstOrDefault();
                if (quotaion != null)
                {
                    output.ProductLists.IsQuotation = true;
                }

       }
            return output;
        }
        public async Task<int> CreateOrUpdateProduct(ProductInput input)
        {
            if (input.Id != 0)
            {
                await UpdateProductAsync(input);
            }
            else
            {
                input.Id =  CreateProductAsync(input);
            }

            return input.Id;
        }
        public int CreateProductAsync(ProductInput input)
        {
            var Id = 0;
            var product = input.MapTo<Product>();
            var val = _productRepository.GetAll().Where(p => p.ProductName == input.ProductName || p.ProductCode == input.ProductCode||p.SuspectCode==input.SuspectCode||p.Gpcode==input.Gpcode).FirstOrDefault();

            if (val == null)
            {
                Id =  _productRepository.InsertAndGetId(product);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in MileProduct Name '" + input.ProductName + "' or Product Code '" + input.ProductCode + "' or Suspect Code '" + input.SuspectCode + "' or Gp Code '" + input.Gpcode + "'...");
            }
            return Id;
        }
        public virtual async Task UpdateProductAsync(ProductInput input)
        {
                var product = await _productRepository.GetAsync(input.Id);
                ObjectMapper.Map(input, product);

                var val = _productRepository.GetAll().Where(p => (p.ProductCode == input.ProductCode || p.SuspectCode == input.SuspectCode || p.Gpcode == input.Gpcode) && p.Id != input.Id).FirstOrDefault();
                if (val == null)
                {
                    await _productRepository.UpdateAsync(product);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in MileProduct Name '" + input.ProductName + "' or Product Code '" + input.ProductCode + "'or Suspect Code '" + input.SuspectCode + "' or Gp Code '" + input.Gpcode + "'...");
                }
           
        }
        public async Task CreateProductImages(ProductImagesInput input)
        {
            var ProductImages = input.MapTo<ProductImageUrl>();

            var val = _ProductImageRepository
             .GetAll().Where(p => p.ProductId == input.ProductId && p.ImageUrl == input.ImageUrl).FirstOrDefault();

            if (val == null)
            {
                await _ProductImageRepository.InsertAsync(ProductImages);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This image already exixts for this product '");
            }

        }
        public async Task GetDeleteProductImages(EntityDto input)
        {
            await _ProductImageRepository.DeleteAsync(input.Id);
        }
        public async Task GetDeleteProduct(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 23);
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
                    throw new UserFriendlyException("Ooops!", "Product cannot be deleted '");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                    {
                        SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                        sqlComm.Parameters.AddWithValue("@TableId", 2);
                        sqlComm.Parameters.AddWithValue("@Id", input.Id);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlComm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }

        }
        public async Task CreateOrUpdateProductPriceLevel(ProductPriceLevelInput input)

        {
            if (input.Id == 0)
            {
                await CreateProductPriceLevel(input);
            }
            else
            {
                await UpdateProductPriceLevel(input);
            }
        }
        public virtual async Task CreateProductPriceLevel(ProductPriceLevelInput input)
        {
            var productPricelevel = input.MapTo<ProductPricelevel>();

            var query = _ProductPricelevelRepository.GetAll().Where(p => p.ProductId == input.ProductId && p.PriceLevelId == input.PriceLevelId).FirstOrDefault();
            if (query == null)
            {
                await _ProductPricelevelRepository.InsertAsync(productPricelevel);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Product Pricelevel...");

            }

        }
        public virtual async Task UpdateProductPriceLevel(ProductPriceLevelInput input)
        {

            var productPricelevel = input.MapTo<ProductPricelevel>();

            var query = _ProductPricelevelRepository.GetAll().Where(p => (p.ProductId == input.ProductId && p.PriceLevelId == input.PriceLevelId) && p.Id != input.Id).FirstOrDefault();
            if (query == null)
            {
                await _ProductPricelevelRepository.UpdateAsync(productPricelevel);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Product Pricelevel...");

            }

        }
        public async Task GetDeleteProductPriceLevel(EntityDto input)
        {
            await _ProductPricelevelRepository.DeleteAsync(input.Id);
        }
        public IEnumerable<string> SpecificationDetail(EntityDto input)
        {
            List<IEnumerable<string>> result = new List<IEnumerable<string>>();

            var data = (from r in _ProdutSpecLinkRepository.GetAll()
                        where r.ProductSpecificationId == input.Id
                        group r by r.AttributeGroupId into g
                        select new DetailDto { GroupId = g.Key }).ToArray();

            if (data.Count() > 0)
            {
                foreach (var d in data)
                {
                    var datas = (from r in _AttributeGroupDetailRepository.GetAll()
                                 where r.AttributeGroupId == d.GroupId
                                 select r.Attributes.AttributeCode).ToArray();
                    result.Add(datas);
                }
            }
            Func<IEnumerable<IEnumerable<string>>,IEnumerable<IEnumerable<string>>> f0 = null;
            f0 = xss =>
            {
                if (!xss.Any())
                {
                    return new[] { Enumerable.Empty<string>() };
                }
                else
                {
                    var query =
                        from x in xss.First()
                        from y in f0(xss.Skip(1))
                        select new[] { x }.Concat(y);
                    return query;
                }
            };

            Func<IEnumerable<IEnumerable<string>>, IEnumerable<string>> f = xss => f0(xss).Select(xs => String.Join("-", xs));

            var results = f(result);
            return results.ToArray();
        }

        public async Task<FileDto> GetProductToExcel()
        {
            var query = _productRepository.GetAll();
            var product = from a in query
                          select new ProductList
                          {
                              Id = a.Id,
                              ProductCode = a.ProductCode,
                              ProductName = a.ProductName,
                              SuspectCode = a.SuspectCode,
                              Gpcode = a.Gpcode,
                              Description = a.Description,
                              ProductSpecificationId = a.ProductSpecificationId,
                              ProductSpecificationName = a.ProductSpecifications.Name,
                              Price = a.Price,
                              ScreationTime = a.CreationTime.ToString()
                          };
            var order = await product.OrderByDescending( p => p.CreationTime).ToListAsync();

            var ProductListDtos = order.MapTo<List<ProductList>>();

            return _productListExcelExporter.ExportToFile(ProductListDtos);
        }
        public async Task LinkProductToQuotation(ProductLinkInput input)
        {
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_LinkQuotationProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TempProductId", SqlDbType.VarChar).Value = input.TempProductId;
                    cmd.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = input.ProductId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task<PagedResultDto<ProductList>> GetAdvancedProducts(GetProductFilterInput input)
        {
            var query = _productRepository.GetAll().Where(p => p.Id == 0);

            if (input.ProductSpecificationId > 0)
            {
                query = _productRepository.GetAll().Where(r => r.ProductSpecificationId == input.ProductSpecificationId);
            } else if (input.ProductCategoryId > 0)
            {
                query = (from p in _productRepository.GetAll()
                         join ps in _ProductSpecificationRepository.GetAll() on p.ProductSpecificationId equals ps.Id
                         join pg in _productGroupRepository.GetAll() on ps.ProductGroupId equals pg.Id
                         join pc in _ProductCategoryRepository.GetAll() on pg.ProductCategoryId equals pc.Id
                         where pc.Id == input.ProductCategoryId
                         select p);
            } else if(input.ProductCategoryId == -2)
            {
                query = _productRepository.GetAll().Where(p => p.ProductSpecificationId == null);
            }
            else
            {
                query = _productRepository.GetAll();
            }

            query = query
           .Include(u => u.ProductSpecifications)
           .WhereIf(
           !input.Filter.IsNullOrEmpty(),
           p => p.ProductCode.Contains(input.Filter) ||
                p.SuspectCode.Contains(input.Filter) ||
                p.Gpcode.Contains(input.Filter) ||
                p.ProductName.Contains(input.Filter) ||
                p.ProductSpecifications.ProductGroups.ProductGroupName.Contains(input.Filter) ||
                p.ProductSpecifications.ProductGroups.ProductCategorys.Name.Contains(input.Filter) ||
                p.ProductSpecifications.Name.Contains(input.Filter)
                );


            var product = from a in query
                          select new ProductList
                          {
                              IsSelected = false,
                              Id = a.Id,
                              ProductCode = a.ProductCode,
                              ProductName = a.ProductName,
                              SuspectCode = a.SuspectCode,
                              Gpcode = a.Gpcode,
                              Description = a.Description,
                              ProductSpecificationId = a.ProductSpecificationId,
                              ProductSpecificationName = a.ProductSpecificationId > 0 ? a.ProductSpecifications.Name : "",
                              Price = a.Price,
                              ScreationTime = a.CreationTime.ToString(),
                              Width = a.Width,
                              Height = a.Height,
                              Depth = a.Depth,
                              Dimention = "",
                              BafcoMade = a.ProductSpecificationId > 0 ? a.ProductSpecifications.BafcoMade : false,
                              CategoryName = a.ProductSpecificationId > 0 ? (a.ProductSpecifications.ProductGroups.ProductCategoryId > 0 ? a.ProductSpecifications.ProductGroups.ProductCategorys.Name : "Custom Product") : "Custom Product",
                              IsDiscountable = false
                          };


            var productCount = await product.CountAsync();
            var productlist = await product.ToListAsync();

            if (input.Sorting == "ProductCode,ProductSpecificationName,SuspectCode,Gpcode,Price,CategoryName,BafcoMade,CreationTime")
            {
                productlist = await product
                .OrderByDescending(a => a.CreationTime)
               .PageBy(input)
               .ToListAsync();
            }
            else
            {
                productlist = await product
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();
            }
            var productlistoutput = productlist.MapTo<List<ProductList>>();

            foreach (var data in productlistoutput)
            {
                var width = "";
                var depth = "";
                var height = "";

                if (data.Width > 0)
                    width = data.Width.ToString() + " - ";
                if (data.Depth > 0)
                    depth = data.Depth.ToString() + " - ";
                if (data.Height > 0)
                    height = data.Height.ToString();

                data.Dimention = width + "" + depth + "" + height;
                var imageqry = _ProductImageRepository.GetAll().Where(p => p.ProductId == data.Id).FirstOrDefault();

                if (imageqry != null)
                {
                    data.ProductImage = imageqry.ImageUrl;
                }
               
                var quotaion = _quotationProductRepository.GetAll().Where(p => p.ProductId == data.Id).FirstOrDefault();
                if (quotaion != null)
                {
                    data.IsQuotation = true;
                }
                if (data.ProductSpecificationId > 0)
                {
                    var Family = (from s in _ProductSpecificationRepository.GetAll()
                                  join g in _productGroupRepository.GetAll() on s.ProductGroupId equals g.Id
                                  where s.Id == data.ProductSpecificationId
                                  select g.prodFamily).FirstOrDefault();

                    data.IsDiscountable = (bool)(Family.Discount ?? false);
                }
                else
                {
                    data.IsDiscountable = false;
                }
            }

            return new PagedResultDto<ProductList>(productCount, productlistoutput);

        }
        public async Task<PagedResultDto<TempProductList>> GetAdvancedTempProducts(GetProductFilterInput input)
        {
            var tempquery = _TempProductRepository.GetAll().Where(c => c.Updated == false)
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.ProductCode.Contains(input.Filter) ||
                     p.SuspectCode.Contains(input.Filter) ||
                     p.Gpcode.Contains(input.Filter)
                     );
            var product = from a in tempquery
                          select new TempProductList
                          {
                              IsSelect = false,
                              Id = a.Id,
                              ProductCode = a.ProductCode,
                              ProductName = a.ProductName,
                              SuspectCode = a.SuspectCode,
                              Gpcode = a.Gpcode,
                              Description = a.Description,
                              ProductSpecificationId = 0,
                              ProductSpecificationName = "",
                              Price = a.Price,
                              CreationTime = a.CreationTime,
                              GMTCreationTime = a.CreationTime,
                              Width = (int)a.Width,
                              Height = (int)a.Height,
                              Depth = (int)a.Depth,
                              Dimention = "",
                              BafcoMade = false,
                              CategoryName = "",
                              ScreationTime = a.CreationTime.ToString(),
                          };

            var productCount = await product.CountAsync();
            var productlist = await product.ToListAsync(); 

            if (input.Sorting == "ProductCode,ProductSpecificationName,SuspectCode,Gpcode,Price,CategoryName,BafcoMade,CreationTime")
            {
                productlist = await product
                .OrderByDescending(a => a.CreationTime)
               .PageBy(input)
               .ToListAsync();
            }
            else
            {
                productlist = await product
               .OrderBy(input.Sorting)
               .PageBy(input)
               .ToListAsync();
            }
            var productlistoutput = productlist.MapTo<List<TempProductList>>();

            foreach (var data in productlistoutput)
            {
                var imageqry = _TempProductImageRepository.GetAll().Where(p => p.TemporaryProductId == data.Id).FirstOrDefault();

                if (imageqry != null)
                {
                    data.ProductImage = imageqry.ImageUrl;
                }
                var width = "";
                var depth = "";
                var height = "";

                if (data.Width > 0)
                    width = data.Width.ToString() + " - ";
                if (data.Depth > 0)
                    depth = data.Depth.ToString() + " - ";
                if (data.Height > 0)
                    height = data.Height.ToString();

                data.Dimention = width + "" + depth + "" + height;

                var quotaion = _quotationProductRepository.GetAll().Where(p => p.TemporaryProductId == data.Id).FirstOrDefault();
                if (quotaion != null)
                {
                    data.IsQuotation = true;
                }
            }

            return new PagedResultDto<TempProductList>(productCount, productlistoutput);
        }
        public virtual int CreateCustomProduct(NullableIdDto input)
        {
            int id = 0;
            var OldProduct = (from p in _productRepository.GetAll() where p.Id == input.Id select p).FirstOrDefault();
            var OldProductImages = (from q in _ProductImageRepository.GetAll() where q.ProductId == input.Id select q.ImageUrl).ToList();
            if (OldProduct != null)
            {
                Product NewProduct = new Product();
                NewProduct.ProductCode = "CP-" + OldProduct.ProductCode;
                NewProduct.ProductName = "CP-" + OldProduct.ProductName;
                NewProduct.SuspectCode = "CP-" + OldProduct.SuspectCode;
                NewProduct.Gpcode = "CP-" + OldProduct.Gpcode;
                NewProduct.ProductSpecificationId = null;
                NewProduct.Price = OldProduct.Price;
                NewProduct.Height = OldProduct.Height;
                NewProduct.Width = OldProduct.Width;
                NewProduct.Depth = OldProduct.Depth;
                NewProduct.Description = OldProduct.Description;
                NewProduct.ProductStateId = OldProduct.ProductStateId;

                var Product = NewProduct.MapTo<Product>();
                id = _productRepository.InsertAndGetId(Product);

                if (OldProductImages.Count > 0)
                {
                    foreach (var item in OldProductImages)
                    {
                        ProductImageUrl Images = new ProductImageUrl();
                        Images.ImageUrl = item;
                        Images.ProductId = id;
                        var NewProductImages = Images.MapTo<ProductImageUrl>();
                        _ProductImageRepository.InsertAsync(NewProductImages);
                    }
                }
            }
            return id;
        }

    }
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
