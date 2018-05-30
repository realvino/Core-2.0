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
using tibs.stem.ProductCategorys;
using tibs.stem.ProductCategoryss.Dto;

namespace tibs.stem.ProductCategoryss
{
    public class ProductCategoryAppService : stemAppServiceBase, IProductCategoryAppService
    {
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        public ProductCategoryAppService(IRepository<ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public ListResultDto<ProductCategoryList> GetProductCategory(GetProductCategoryInput input)
        {

            var query = _productCategoryRepository.GetAll()
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Code.Contains(input.Filter) ||
                        u.Name.Contains(input.Filter) ||
                        u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<ProductCategoryList>(query.MapTo<List<ProductCategoryList>>());
        }
        public async Task<GetProductCategory> GetProductCategoryForEdit(NullableIdDto input)
        {
            var output = new GetProductCategory
            {
            };

            var pcategory = _productCategoryRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();
            output.category = pcategory.MapTo<ProductCategoryInput>();

            return output;

        }
        public async Task CreateOrUpdateProductCategory(ProductCategoryInput input)
        {
            if (input.Id != 0)
            {
                await UpdateProductCategory(input);
            }
            else
            {
                await CreateProductCategory(input);
            }
        }
        public async Task CreateProductCategory(ProductCategoryInput input)
        {
            var category = input.MapTo<ProductCategory>();
            var val = _productCategoryRepository
             .GetAll().Where(p => p.Code == input.Code || p.Name == input.Name).FirstOrDefault();

            if (val == null)
            {
                await _productCategoryRepository.InsertAsync(category);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' orName '" + input.Name + "'...");
            }
        }
        public async Task UpdateProductCategory(ProductCategoryInput input)
        {
            var category = input.MapTo<ProductCategory>();

            var val = _productCategoryRepository
            .GetAll().Where(p => (p.Code == input.Code || p.Name == input.Name) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _productCategoryRepository.UpdateAsync(category);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Code '" + input.Code + "' or Name  '" + input.Name + "'...");
            }

        }
        public async Task DeleteProductCategory(EntityDto input)
        {
            await _productCategoryRepository.DeleteAsync(input.Id);
        }


    }
}
