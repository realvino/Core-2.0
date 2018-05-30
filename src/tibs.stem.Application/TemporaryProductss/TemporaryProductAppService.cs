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
using tibs.stem.TemporaryProducts;
using tibs.stem.TemporaryProductss.Dto;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using tibs.stem.Products;
using System.Data;
using System.Data.SqlClient;
using tibs.stem.QuotationProducts;

namespace tibs.stem.TemporaryProductss
{
    public class TemporaryProductAppService : stemAppServiceBase, ITemporaryProductAppService
    {
        private readonly IRepository<TemporaryProduct> _TempProductRepository;
        private readonly IRepository<TemporaryProductImage> _TempProductImageRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<QuotationProduct> _quotationProductRepository;

        public TemporaryProductAppService(
            IRepository<TemporaryProduct> TempProductRepository,
            IRepository<Product> productRepository,
            IRepository<TemporaryProductImage> TempProductImageRepository,
            IRepository<QuotationProduct> quotationProductRepository
            )
        {
            _TempProductRepository = TempProductRepository;
            _TempProductImageRepository = TempProductImageRepository;
            _productRepository = productRepository;
            _quotationProductRepository = quotationProductRepository;

        }

        public async Task<PagedResultDto<TemporaryProductList>> GetTemporaryProduct(GetTemporaryProductInput input)
        {
            var query = _TempProductRepository.GetAll().Where(p => p.Updated == false)
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.ProductCode.Contains(input.Filter) ||
                     p.SuspectCode.Contains(input.Filter) ||
                     p.Gpcode.Contains(input.Filter)                    
                     );

            var product = from a in query
                          join ur in UserManager.Users on a.CreatorUserId equals ur.Id
                          select new TemporaryProductList
                          {
                              Id = a.Id,
                              ProductCode = a.ProductCode,
                              ProductName = a.ProductName,
                              SuspectCode = a.SuspectCode,
                              Gpcode = a.Gpcode,
                              Description = a.Description,
                              ProductSpecificationName = "Non Standard Product",
                              Price = a.Price,
                              ScreationTime = a.CreationTime.ToString(),
                              DcreationTime = a.CreationTime,
                              Width = a.Width,
                              Height = a.Height,
                              Depth = a.Depth,
                              Dimention = "",
                              CreatorUserId = a.CreatorUserId ?? 0,
                              CreatedBy = ur != null ? ur.UserName : "",
                              LastModifierUserId = a.LastModifierUserId ?? 0,
                              LastModifiedBy = a.LastModifierUserId != null ? (from l in UserManager.Users where l.Id == a.LastModifierUserId select l.UserName).FirstOrDefault() : ""
                          };


            var TempproductCount = await query.CountAsync();

            var TempProducts = await product.OrderByDescending(p => p.DcreationTime)
                                          .PageBy(input)
                                          .ToListAsync();

            foreach (var data in TempProducts)
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

                var quotaion = _quotationProductRepository.GetAll().Where(p => p.TemporaryProductId == data.Id).FirstOrDefault();
                if (quotaion != null)
                {
                    data.IsQuotation = true;
                }
            }

            var productlistoutput = TempProducts.MapTo<List<TemporaryProductList>>();

            return new PagedResultDto<TemporaryProductList>(TempproductCount, productlistoutput);
        }

        public async Task<GetTemporaryProduct> GetTemporaryProductForEdit(NullableIdDto input)
        {
            if(input.Id == null)
            {
                input.Id = 0;
            }
            var output = new GetTemporaryProduct();
            var query = _TempProductRepository
               .GetAll().Where(p => p.Id == input.Id);

            if(query.Count() > 0)
            {
                output.TemporaryProductLists = query.FirstOrDefault().MapTo<TemporaryProductList>();

                var Images = (from c in _TempProductImageRepository.GetAll()
                              where c.TemporaryProductId == input.Id
                              select new TemporaryProdImages
                              {
                                  Id = c.Id,
                                  ImageUrl = c.ImageUrl
                              }).ToArray();

                output.TempProductImages = Images;
            }
            
            return output;

        }
        public async Task CreateOrUpdateTemporaryProduct(TemporaryProductInput input)
        {
            if (input.Id > 0)
            {
                await UpdateTemporaryProduct(input);
            }
            else
            {
                await CreateTemporaryProduct(input);
            }
        }
        public virtual async Task CreateTemporaryProduct(TemporaryProductInput input)
        {

            input.Width = input.Width ?? 0;
            input.Depth = input.Depth ?? 0;
            input.Height = input.Height ?? 0;

            var TempProduct = input.MapTo<TemporaryProduct>();

            var dup = _productRepository.GetAll().Where(p => p.ProductCode == input.ProductCode || p.ProductCode == input.SuspectCode || p.SuspectCode == input.ProductCode || p.SuspectCode == input.SuspectCode);
            if (dup.Count() == 0)
            {
                var val = _TempProductRepository.GetAll().Where(p => p.ProductCode == input.ProductCode || p.SuspectCode == input.SuspectCode || p.Gpcode == input.Gpcode);

                if (val.Count() == 0)
                {
                    await _TempProductRepository.InsertAsync(TempProduct);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Non Standard Product...");
                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This Product already exixts in Product Master...");
            }
        }
        public virtual async Task UpdateTemporaryProduct(TemporaryProductInput input)
        {
            input.Width = input.Width ?? 0;
            input.Depth = input.Depth ?? 0;
            input.Height = input.Height ?? 0;

            var TempProduct = await _TempProductRepository.GetAsync(input.Id);

            var val = _TempProductRepository.GetAll().Where(p => (p.ProductCode == input.ProductCode || p.SuspectCode == input.SuspectCode ) && p.Id != input.Id);
            if (val.Count() == 0)
            {
                ObjectMapper.Map(input, TempProduct);
                await _TempProductRepository.UpdateAsync(TempProduct);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Non Standard Product...");
            }

        }
        public async Task GetDeleteTemporaryProduct(EntityDto input)
        {
            ConnectionAppService db = new ConnectionAppService();
            DataTable ds = new DataTable();
            using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
            {
                SqlCommand sqlComm = new SqlCommand("Sp_FindMappedTable", conn);
                sqlComm.Parameters.AddWithValue("@TableId", 36);
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
                    throw new UserFriendlyException("Ooops!", "Non Standard Product cannot be deleted '");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(db.ConnectionString()))
                    {
                        SqlCommand sqlComm = new SqlCommand("Sp_DeleteAllDetails", conn);
                        sqlComm.Parameters.AddWithValue("@TableId", 10);
                        sqlComm.Parameters.AddWithValue("@Id", input.Id);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        sqlComm.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public async Task CreateTemporaryProductImage(TemporaryProductImageInput input)
        {
            var ProductImages = input.MapTo<TemporaryProductImage>();

            var val = _TempProductImageRepository
             .GetAll().Where(p => p.TemporaryProductId == input.TemporaryProductId && p.ImageUrl == input.ImageUrl);

            if (val.Count() == 0)
            {
                await _TempProductImageRepository.InsertAsync(ProductImages);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This image already exixts for this product '");
            }

        }

        public async Task GetDeleteTemporaryProductImage(EntityDto input)
        {
            await _TempProductImageRepository.DeleteAsync(input.Id);
        }

        public async Task<GetTemporaryProduct> GetTemporaryProductForEditBySuspectCode(SuspectCodeSearch input)
        {
            var output = new GetTemporaryProduct();
            var query = _TempProductRepository
               .GetAll().Where(p => p.SuspectCode == input.SuspectCode).FirstOrDefault();

            if (query != null)
            {
                output.TemporaryProductLists = query.MapTo<TemporaryProductList>();

                var Images = (from c in _TempProductImageRepository.GetAll()
                              where c.TemporaryProductId == query.Id
                              select new TemporaryProdImages
                              {
                                  Id = c.Id,
                                  ImageUrl = c.ImageUrl
                              }).ToArray();

                output.TempProductImages = Images;
            }

            return output;

        }
        public virtual async Task<int> CreateTemporaryProductandId(TemporaryProductInput input)
        {

            var TempProduct = input.MapTo<TemporaryProduct>();
            var Id = 0;
            var dup = _productRepository.GetAll().Where(p => p.ProductCode == input.ProductCode || p.ProductCode == input.SuspectCode || p.SuspectCode == input.ProductCode || p.SuspectCode == input.SuspectCode).FirstOrDefault();
            if (dup == null)
            {
                var val = _TempProductRepository.GetAll().Where(p => p.ProductCode == input.ProductCode || p.SuspectCode == input.SuspectCode).FirstOrDefault();

                if (val == null)
                {
                    Id =  _TempProductRepository.InsertAndGetId(TempProduct);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Non Standard Product...");
                }
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "This Product already exixts in Product Master...");
            }
            return Id;
        }

    }
    public class SuspectCodeSearch
    {
        public string SuspectCode { get; set; }
    }

}
