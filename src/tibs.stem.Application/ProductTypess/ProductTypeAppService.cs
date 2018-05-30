using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem;
using System.Linq.Dynamic.Core;
using tibs.stem.Dto;
using tibs.stem.ProductTypes.Dto;

namespace tibs.stem.ProductTypes
{
    public class ProductTypeAppService : stemAppServiceBase, IProductTypeAppService
    {
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductTypeAppService(IRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
            
        }

        public ListResultDto<ProductTypeListDto> GetProductType(ProductTypeInput input)
        {
            var act = _productTypeRepository
                .GetAll();
            var query = act
                 .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.ProductTypeName.Contains(input.Filter) ||
                         u.ProductTypeCode.Contains(input.Filter) ||
                        u.Id.ToString().Contains(input.Filter))
                .ToList();

            return new ListResultDto<ProductTypeListDto>(query.MapTo<List<ProductTypeListDto>>());
        }

        public async Task<GetProductType> GetProductTypeForEdit(NullableIdDto input)
        {
            var output = new GetProductType
            {
            };

            var product = _productTypeRepository
                .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

            output.producttype = product.MapTo<ProductTypeListDto>();
            return output;
        }

        public async Task CreateOrUpdateProductType(ProductTypeInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateProductType(input);
            }
            else
            {
                await CreateProductType(input);
            }
        }

        public async Task CreateProductType(ProductTypeInputDto input)
        {
            var product = input.MapTo<ProductType>();
            var val = _productTypeRepository
             .GetAll().Where(p =>  p.ProductTypeName == input.ProductTypeName ||p.ProductTypeCode == input.ProductTypeCode ).FirstOrDefault();

            if (val == null)
            {
                await _productTypeRepository.InsertAsync(product);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductType Name '" + input.ProductTypeName + "' or ProductType Code '" + input.ProductTypeCode + "'...");
            }
        }

        public async Task UpdateProductType(ProductTypeInputDto input)
        {
            var product = await _productTypeRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, product);

            var val = _productTypeRepository
              .GetAll().Where(p => (p.ProductTypeName == input.ProductTypeName || p.ProductTypeCode == input.ProductTypeCode) && p.Id != input.Id).FirstOrDefault();

            if (val == null)
            {
                await _productTypeRepository.UpdateAsync(product);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in ProductType Name '" + input.ProductTypeName + "' or ProductType Code '" + input.ProductTypeCode + "'...");
            }

        }



        public async Task GetDeleteProductType(EntityDto input)
        {
            await _productTypeRepository.DeleteAsync(input.Id);
        }



    }
}
