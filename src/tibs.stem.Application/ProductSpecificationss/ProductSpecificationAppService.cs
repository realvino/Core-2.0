using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using tibs.stem.AttributeGroupDetails;
using System.Linq.Dynamic.Core;
using tibs.stem.ProductAttributeGroups;
using tibs.stem.ProductAttributeGroupss.Dto;
using tibs.stem.ProductAttributes;
using tibs.stem.ProductGroups;
using tibs.stem.ProductSpecificationDetails;
using tibs.stem.ProductSpecifications;
using tibs.stem.ProductSpecificationss.Dto;
using tibs.stem.ProdutSpecLinks;
using tibs.stem.Products.Dto;
using tibs.stem.Products;
using System.Data.SqlClient;
using System.Data;
using tibs.stem.ProductGroups.Dto;
using tibs.stem.ProductSpecificationss.Exporting;
using tibs.stem.Dto;
using Microsoft.EntityFrameworkCore;
using tibs.stem.QuotationProducts;

namespace tibs.stem.ProductSpecificationss
{
    public class ProductSpecificationAppService : stemAppServiceBase, IProductSpecificationAppService
    {
        private readonly IRepository<ProductSpecification> _ProductSpecificationRepository;
        private readonly IRepository<ProductSpecificationDetail> _ProductSpecificationDetailRepository;
        private readonly IRepository<ProductAttributeGroup> _AttributeGroupRepository;
        private readonly IRepository<ProductAttribute> _productAttributeRepository;
        private readonly IRepository<AttributeGroupDetail> _AttributeGroupDetailRepository;
        private readonly IRepository<ProductGroupDetail> _ProductGroupDetailRepository;
        private readonly IRepository<ProdutSpecLink> _ProdutSpecLinkRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IProductSpecificationListExcelExporter _productSpecificationListExcelExporter;
        private readonly IRepository<QuotationProduct> _quotationProductRepository;

        public ProductSpecificationAppService(
            IRepository<ProductSpecification> ProductSpecificationRepository, 
            IRepository<ProductSpecificationDetail> ProductSpecificationDetailRepository,
            IRepository<ProductAttributeGroup> attributeGroupRepository,
            IRepository<ProductAttribute> productAttributeRepository,
            IRepository<AttributeGroupDetail> AttributeGroupDetailRepository,
            IRepository<ProductGroupDetail> ProductGroupDetailRepository,
            IRepository<ProdutSpecLink> ProdutSpecLinkRepository,
            IProductSpecificationListExcelExporter productSpecificationListExcelExporter,
            IRepository<Product> productRepository,
            IRepository<QuotationProduct> quotationProductRepository
            )
        {
            _ProductSpecificationRepository = ProductSpecificationRepository;
            _ProductSpecificationDetailRepository = ProductSpecificationDetailRepository;
            _AttributeGroupRepository = attributeGroupRepository;
            _productAttributeRepository = productAttributeRepository;
            _AttributeGroupDetailRepository = AttributeGroupDetailRepository;
            _ProductGroupDetailRepository = ProductGroupDetailRepository;
            _ProdutSpecLinkRepository = ProdutSpecLinkRepository;
            _productRepository = productRepository;
            _productSpecificationListExcelExporter = productSpecificationListExcelExporter;
            _quotationProductRepository = quotationProductRepository;
        }

        public ListResultDto<ProductSpecificationList> GetProductSpecification(GetProductSpecificationInput input)
        {
            var ProductSpecification = _ProductSpecificationRepository
                .GetAll();
            var query = ProductSpecification
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter))
                .ToList();

            return new ListResultDto<ProductSpecificationList>(query.MapTo<List<ProductSpecificationList>>());
        }

        public async Task<GetProductSpecification> GetProductSpecificationForEdit(EntityDto input)
        {
            var output = new GetProductSpecification{};

            List<IEnumerable<string>> result = new List<IEnumerable<string>>();

            var ProductSpecification = _ProductSpecificationRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            var SubListout = new List<GetProductSpecificationListDto>();

            var AttributrGroups = (from r in _ProductGroupDetailRepository.GetAll()
                                               where r.ProductGroupId == ProductSpecification.ProductGroupId
                                              orderby r.OrderBy
                                              select new ProductSpecificationDetailList
                                              {
                                                  Id = r.Id,
                                                  AttributeGroupId = r.AttributeGroupId,
                                                  AttributeGroupName = r.ProductAttributeGroups.AttributeGroupName,
                                                  AttributeGroupCode = r.ProductAttributeGroups.AttributeGroupCode,
                                              } ).ToArray();

            foreach (var groups in AttributrGroups)
            {
                var OverAll = (from r in _AttributeGroupDetailRepository.GetAll()
                               where r.AttributeGroupId == groups.AttributeGroupId
                               select new AttributeGroupDetailListDto
                               {
                                   AttributeGroupId = r.AttributeGroupId,
                                   AttributeGroupName = r.AttributeGroups.AttributeGroupName,
                                   AttributeId = r.AttributeId,
                                   AttributeName = r.Attributes.AttributeName,
                                   AttributeCode = r.Attributes.AttributeCode,
                                   ImgPath = r.Attributes.Imageurl,
                                   Selected = false
                               }).ToArray();
               
                foreach(var data in OverAll)
                {
                    var Attributes = (from p in _ProdutSpecLinkRepository.GetAll() where p.ProductSpecificationId == input.Id && p.AttributeId == data.AttributeId select new SpesDretail
                    {
                        Id = p.Id,
                        AttributeId = p.AttributeId
                    }).FirstOrDefault();

                    if (Attributes != null)
                    {
                        data.Id = Attributes.Id;
                        data.Selected = true;
                    }
                }

                SubListout.Add(new GetProductSpecificationListDto
                {
                    Id = groups.Id,
                    Name = groups.AttributeGroupName,
                    AttributeGroups = OverAll
                });

            }

            output.productSpecificationDetails = SubListout.ToArray();

            output.ProductSpecification = (from d in _ProductSpecificationRepository.GetAll()
                                           where d.Id == input.Id
                                           select new ProductSpecificationList
                                           {
                                               Id = d.Id,
                                               Name = d.Name,
                                               ImageUrl = d.ImageUrl,
                                               ProductGroupId = d.ProductGroupId,
                                               ProductGroupName = d.ProductGroups.ProductGroupName,
                                               Description = d.Description,
                                               Reset = d.Reset,
                                               BafcoMade = d.BafcoMade
                                           }).FirstOrDefault();

            var datass = (from r in _ProdutSpecLinkRepository.GetAll()
                        where r.ProductSpecificationId == input.Id
                        group r by r.AttributeGroupId into g
                        select new { GroupId = g.Key });

            var idd = (int)output.ProductSpecification.ProductGroupId;
            var ProdutSpecs = (from r in datass
                               join s in _ProductGroupDetailRepository.GetAll() on r.GroupId equals s.AttributeGroupId 
                               where s.ProductGroupId == idd
                               select new { GroupId = r.GroupId,Orderby = s.OrderBy} ).ToArray();

            var ProdutSpecss = ProdutSpecs.OrderBy(c=>c.Orderby).ToList();

         
            if (ProdutSpecs.Count() > 0)
            {
                var FamilyCode = (from r in _ProdutSpecLinkRepository.GetAll()
                                  where r.ProductSpecificationId == input.Id
                                  group r by r.ProductGroups.prodFamily.ProductFamilyCode into g
                                  select g.Key).ToArray();

                result.Add(FamilyCode);
                foreach (var d in ProdutSpecss)
                {
                    var datas = (from r in _AttributeGroupDetailRepository.GetAll()
                                 join p in _ProdutSpecLinkRepository.GetAll() on r.AttributeId equals p.AttributeId
                                 where r.AttributeGroupId == d.GroupId && p.ProductSpecificationId == input.Id
                                 select r.Attributes.AttributeCode).ToArray();
                    result.Add(datas);
                }
            }

            Func<IEnumerable<IEnumerable<string>>, IEnumerable<IEnumerable<string>>> f0 = null;
            f0 = xss =>
            {
                if (!xss.Any())
                {
                    return new[] { Enumerable.Empty<string>() };
                }
                else
                {
                    var querys =
                        from x in xss.First()
                        from y in f0(xss.Skip(1))
                        select new[] { x }.Concat(y);
                    return querys;
                }
            };

            Func<IEnumerable<IEnumerable<string>>, IEnumerable<string>> f = xss => f0(xss).Select(xs => String.Join("-", xs));

            var results = f(result);

            var productCodes = results.ToArray();

            var Created = (from r in _productRepository.GetAll() where r.ProductSpecificationId == input.Id select r.ProductCode).ToArray();

            string[] same = Created.Intersect(productCodes).ToArray();

            if(result.Count() > 0)
            {
                output.Available = productCodes.Count();
            }
            else
            {
                output.Available = 0;
            }
            bool datamap = false;
            var records = _productRepository.GetAll().Where(p => p.ProductSpecificationId == input.Id).ToList();

            foreach (var item in records)
            {
                var maprecord = _quotationProductRepository.GetAll().Where(p => p.ProductId == item.Id).Count();
                if (maprecord > 0 && datamap == false)
                {
                    datamap = true;
                }
            }
            output.DataMapped = datamap;
            output.Created = same.Count();
            return output;
        }
        public async Task CreateOrUpdateProductSpecification(CreateProductSpecification input)
        {
            if (input.Id != 0)
            {
                await UpdateProductSpecification(input);
            }
            else
            {
                await CreateProductSpecification(input);
            }
        }
        public async Task CreateProductSpecification(CreateProductSpecification input)
        {
            var ProductSpecification = input.MapTo<ProductSpecification>();
            var val = _ProductSpecificationRepository
             .GetAll().Where(p => p.Name == input.Name).FirstOrDefault();

            if (val == null)
            {
                await _ProductSpecificationRepository.InsertAsync(ProductSpecification);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductSpecification Name '" + input.Name + "'...");
            }
        }
        public async Task UpdateProductSpecification(CreateProductSpecification input)
        {
            var ProductSpecification = input.MapTo<ProductSpecification>();
            
            var val = _ProductSpecificationRepository
              .GetAll().Where(p => p.Name == input.Name && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                ProductSpecification.LastModificationTime = DateTime.Now;
                await _ProductSpecificationRepository.UpdateAsync(ProductSpecification);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductSpecification Name '" + input.Name + "'...");
            }

        }
        public async Task GetDeleteProductSpecification(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 20);
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
                    throw new UserFriendlyException("Ooops!", "Product Specification cannot be deleted '");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                    {
                        SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                        sqlComm.Parameters.AddWithValue("@TableId", 4);
                        sqlComm.Parameters.AddWithValue("@Id", input.Id);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlComm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

      
       
        public async Task CreateProductSpecificationDetail(CreateProductSpecificationInput input)
        {
            var ProductSpecificationDetail = input.MapTo<ProdutSpecLink>();

            var val = _ProdutSpecLinkRepository
             .GetAll().Where(p => p.AttributeGroupId == input.AttributeGroupId && p.ProductSpecificationId == input.ProductSpecificationId && p.ProductGroupId == input.ProductGroupId && p.AttributeId == input.AttributeId).FirstOrDefault();

            if (val == null)
            {
                await _ProdutSpecLinkRepository.InsertAsync(ProductSpecificationDetail);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This data already exixts '");
            }

        }      
        public async Task GetDeleteProductSpecificationDetail(EntityDto input)
        {
            await _ProdutSpecLinkRepository.DeleteAsync(input.Id);
        }

        public Array GetProductGroupDetails(EntityDto input)
        {
            var querys = _ProductGroupDetailRepository.GetAll().Where(p => p.ProductGroupId == input.Id).OrderBy(r => r.OrderBy);

            var ProductSpecificationDetail = (from r in querys
                                              select new ProductSpecificationDetailList
                                              {
                                                  Id = r.Id,
                                                  AttributeGroupId = r.AttributeGroupId,
                                                  AttributeGroupName = r.ProductAttributeGroups.AttributeGroupName,
                                                  AttributeGroupCode = r.ProductAttributeGroups.AttributeGroupCode,
                                              }).ToArray();

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
                                           AttributeCode = r.Attributes.AttributeCode,
                                           ImgPath = r.Attributes.Imageurl,
                                           Selected = false

                                       }).ToArray()
                });

            }
            return SubListout.ToArray();
        }

        public async Task GenerateProduct(EntityDto input)
        {
            List<IEnumerable<string>> result = new List<IEnumerable<string>>();
            var ProductSpecification = _ProductSpecificationRepository
            .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            var datass = (from r in _ProdutSpecLinkRepository.GetAll()
                          where r.ProductSpecificationId == input.Id
                          group r by r.AttributeGroupId into g
                          select new { GroupId = g.Key });

            var idd = (int)ProductSpecification.ProductGroupId;

            var ProdutSpecs = (from r in datass
                               join s in _ProductGroupDetailRepository.GetAll() on r.GroupId equals s.AttributeGroupId
                               where s.ProductGroupId == idd
                               select new { GroupId = r.GroupId, Orderby = s.OrderBy }).ToArray();

            var ProdutSpecss = ProdutSpecs.OrderBy(c => c.Orderby).ToList();

            var FamilyCode = (from r in _ProdutSpecLinkRepository.GetAll()
                        where r.ProductSpecificationId == input.Id
                        group r by r.ProductGroups.prodFamily.ProductFamilyCode into g
                        select g.Key).ToArray();

            result.Add(FamilyCode);

            if (ProdutSpecss.Count() > 0)
            {
                foreach (var d in ProdutSpecss)
                {
                    var datas = (from r in _AttributeGroupDetailRepository.GetAll() join p in _ProdutSpecLinkRepository.GetAll() on r.AttributeId equals p.AttributeId
                                 where r.AttributeGroupId == d.GroupId && p.ProductSpecificationId == input.Id
                                 select r.Attributes.AttributeCode).ToArray();
                    result.Add(datas);
                }
            }
            Func<IEnumerable<IEnumerable<string>>, IEnumerable<IEnumerable<string>>> f0 = null;
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
            var resultsList = results.ToList();
            var fruitSentence = resultsList.Aggregate((current, next) => $"{current}//{next}");

            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_BulkProductGeneration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = input.Id;
                    cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = fruitSentence;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task RegenerateProduct(EntityDto input)
        {
            List<IEnumerable<string>> result = new List<IEnumerable<string>>();
            var ProductSpecification = _ProductSpecificationRepository
            .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            var datass = (from r in _ProdutSpecLinkRepository.GetAll()
                          where r.ProductSpecificationId == input.Id
                          group r by r.AttributeGroupId into g
                          select new { GroupId = g.Key });

            var idd = (int)ProductSpecification.ProductGroupId;

            var ProdutSpecs = (from r in datass
                               join s in _ProductGroupDetailRepository.GetAll() on r.GroupId equals s.AttributeGroupId
                               where s.ProductGroupId == idd
                               select new { GroupId = r.GroupId, Orderby = s.OrderBy }).ToArray();

            var ProdutSpecss = ProdutSpecs.OrderBy(c => c.Orderby).ToList();

            var FamilyCode = (from r in _ProdutSpecLinkRepository.GetAll()
                              where r.ProductSpecificationId == input.Id
                              group r by r.ProductGroups.prodFamily.ProductFamilyCode into g
                              select g.Key).ToArray();

            result.Add(FamilyCode);

            if (ProdutSpecss.Count() > 0)
            {
                foreach (var d in ProdutSpecss)
                {
                    var datas = (from r in _AttributeGroupDetailRepository.GetAll()
                                 join p in _ProdutSpecLinkRepository.GetAll() on r.AttributeId equals p.AttributeId
                                 where r.AttributeGroupId == d.GroupId && p.ProductSpecificationId == input.Id
                                 select r.Attributes.AttributeCode).ToArray();
                    result.Add(datas);
                }
            }
            Func<IEnumerable<IEnumerable<string>>, IEnumerable<IEnumerable<string>>> f0 = null;
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
            var resultsList = results.ToList();
            var fruitSentence = resultsList.Aggregate((current, next) => $"{current}//{next}");

            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection con = new SqlConnection(db.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_ResetProductGeneration", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = input.Id;
                    cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = fruitSentence;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public virtual async Task ProductGroupDetailChange(ProductGroupDetailChangeInput input)
        {
            ConnectionAppService db = new ConnectionAppService();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                var Source = new SqlParameter
                {
                    ParameterName = "Source",
                    Value = input.Source
                };

                var Destination = new SqlParameter
                {
                    ParameterName = "Destination",
                    Value = input.Destination
                };

                var ProductGroupId = new SqlParameter
                {
                    ParameterName = "ProductGroupId",
                    Value = input.ProductGroupId
                };

                var RowId = new SqlParameter
                {
                    ParameterName = "RowId",
                    Value = input.RowId
                };
                SqlCommand sqlComm = new SqlCommand("Sp_ProductGroupDetailOrder", conn);
                sqlComm.Parameters.Add(Source);
                sqlComm.Parameters.Add(Destination);
                sqlComm.Parameters.Add(ProductGroupId);
                sqlComm.Parameters.Add(RowId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();
                try
                {
                    sqlComm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                conn.Close();
            }
        }

        public async Task<FileDto> GetProductSpecificationToExcel()
        {

            var ProductSpecification = _ProductSpecificationRepository.GetAll();
            var ProductSpecifications = (from a in ProductSpecification
                                         select new ProductSpecificationList
                                         {
                                             Id = a.Id,
                                             Name = a.Name,
                                             ImageUrl = a.ImageUrl,
                                             ProductGroupId = a.ProductGroupId,
                                             ProductGroupName = a.ProductGroups.ProductGroupName
                                         });
            var order = await ProductSpecifications.OrderBy("Name").ToListAsync();

            var ProductSpecificationDtos = order.MapTo<List<ProductSpecificationList>>();

            return _productSpecificationListExcelExporter.ExportToFile(ProductSpecificationDtos);
        }
        public async Task<PagedResultDto<ProductList>> GetProducts(GetProductSpecInput input)
        {

            var query = (from r in _productRepository.GetAll() where r.ProductSpecificationId == input.Id select r);

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
            var product = (from a in query
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
                               ScreationTime = a.CreationTime.ToString(),
                               InQuotationProduct = false,
                           });

            var products = product
               .OrderByDescending(c => c.CreationTime)
               .PageBy(input)
               .ToList();

            foreach (var d in products)
            {
                var quotaionquery = _quotationProductRepository.GetAll().Where(p => p.ProductId == d.Id).FirstOrDefault();
                if (quotaionquery != null)
                    d.InQuotationProduct = true;
            }

            var productlistoutput = products.MapTo<List<ProductList>>();

            return new PagedResultDto<ProductList>(productCount, productlistoutput);

        }
        public async Task CreateOrDeleteProductGroupDetails(ProductSpecArray input)
        {
            AttributeArray[] SpecAtrrArray = input.ArributeGroupSelect;
            if (SpecAtrrArray.Length > 0)
            {
                foreach (var specarray in SpecAtrrArray)
                {
                    if (specarray.AttributeId.Length > 0)
                    {
                        SpecLinkArray(input.ProductGroupId, input.ProductSpecId, specarray.AttributeGroupId, specarray.AttributeId);
                    }
                    else
                    {
                        var recorddelete = _ProdutSpecLinkRepository.GetAll().Where(p => p.ProductGroupId == input.ProductGroupId
                                && p.ProductSpecificationId == input.ProductSpecId && p.AttributeGroupId == specarray.AttributeGroupId).ToList();
                        foreach (var delete in recorddelete)
                        {
                            await _ProdutSpecLinkRepository.DeleteAsync(delete.Id);
                        }
                    }
                }
            }
            else
            {
                var recorddelete = _ProdutSpecLinkRepository.GetAll().Where(p => p.ProductGroupId == input.ProductGroupId
                                && p.ProductSpecificationId == input.ProductSpecId).ToList();
                foreach (var delete in recorddelete)
                {
                    await _ProdutSpecLinkRepository.DeleteAsync(delete.Id);
                }

            }
            //await GenerateProduct(input.ProductSpecId);
        }

        private async void SpecLinkArray(int productGroupId, int productSpecId, int attributeGroupId, int[] attributeId)
        {
            int[] attribute = attributeId;

            var records = _AttributeGroupDetailRepository.GetAll().Where(p => p.AttributeGroupId == attributeGroupId).ToList();

            foreach (var attri in records)
            {
                bool checkvalue = Array.Exists(attribute, r => r == attri.AttributeId);
                var previousrecord = _ProdutSpecLinkRepository.GetAll().Where(p => p.ProductGroupId == productGroupId
                                && p.ProductSpecificationId == productSpecId && p.AttributeGroupId == attributeGroupId
                                && p.AttributeId == attri.AttributeId
                                ).FirstOrDefault();

                if (checkvalue == true)
                {
                    if (previousrecord == null)
                    {
                        CreateProductSpecificationInput recup = new CreateProductSpecificationInput();
                        var attrupdate = recup.MapTo<ProdutSpecLink>();
                        attrupdate.ProductGroupId = productGroupId;
                        attrupdate.ProductSpecificationId = productSpecId;
                        attrupdate.AttributeGroupId = attributeGroupId;
                        attrupdate.AttributeId = attri.AttributeId;
                        await _ProdutSpecLinkRepository.InsertAsync(attrupdate);
                    }
                }
                else
                {
                    if (previousrecord != null)
                    {
                        await _ProdutSpecLinkRepository.DeleteAsync(previousrecord.Id);
                    }
                }
            }
        }
        public async Task ConfirmDeleteProductSpecification(EntityDto input)
        {

                ConnectionAppService db = new ConnectionAppService();
                DataTable ds = new DataTable();
                using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                {
                    SqlCommand sqlComm = new SqlCommand("Sp_SpecProductDelete", conn);
                    sqlComm.Parameters.AddWithValue("@SpecId", input.Id);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter da = new SqlDataAdapter(sqlComm))
                    {

                        da.Fill(ds);
                    }

                }
        }
    }
    public class SpesDretail
    {
        public int Id { get; set; }
        public int AttributeId { get; set; }

    }
}
