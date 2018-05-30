using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.UI;
using System.Data.SqlClient;
using tibs.stem.ProductGroups;
using tibs.stem.ProductSubGroups.Dto;
using tibs.stem.Tenants.Dashboard;
using Abp.Authorization;
using tibs.stem.Authorization;

namespace tibs.stem.ProductSubGroups
{
    public class ProductSubGroupAppService : stemAppServiceBase, IProductSubGroupAppService
    {
        private readonly IRepository<ProductSubGroup> _productsubGroupRepository;
        private readonly IRepository<ProductGroup> _productGroupRepository;
        public ProductSubGroupAppService(IRepository<ProductSubGroup> productsubGroupRepository, IRepository<ProductGroup> productGroupRepository)
        {
            _productsubGroupRepository = productsubGroupRepository;
            _productGroupRepository = productGroupRepository;
        }
        public async Task<PagedResultDto<ProductSubGroupListDto>> GetProductSubGroup(GetProductSubGroupInput input)
        {
            var query = _productsubGroupRepository.GetAll()
                .WhereIf(
                !input.Filter.IsNullOrEmpty(),
                p => p.ProductSubGroupCode.Contains(input.Filter) ||
                     p.ProductSubGroupName.Contains(input.Filter) ||
                     p.productGroups.ProductGroupName.Contains(input.Filter) 

                );
            var productsubGroup = (from a in query select new ProductSubGroupListDto { Id = a.Id, ProductSubGroupCode = a.ProductSubGroupCode, ProductSubGroupName = a.ProductSubGroupName, ProductGroupName = a.productGroups.ProductGroupName,GroupId = a.GroupId });
            var productsubCount = await productsubGroup.CountAsync();
            var productsubGrouplist = await productsubGroup
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            var grouplistoutput = productsubGrouplist.MapTo<List<ProductSubGroupListDto>>();
            return new PagedResultDto<ProductSubGroupListDto>(
                productsubCount, grouplistoutput); 
        }

        public async Task<GetProductSubGroup> GetProductSubGroupForEdit(NullableIdDto input)
        {
            var output = new GetProductSubGroup();
            var query = _productsubGroupRepository
               .GetAll().Where(p => p.Id == input.Id);

            if (query.Count() > 0)
            {
                var subGroup = (from a in query select new ProductSubGroupListDto { Id = a.Id, ProductSubGroupCode = a.ProductSubGroupCode, ProductSubGroupName = a.ProductSubGroupName, ProductGroupName = a.productGroups.ProductGroupName, GroupId = a.GroupId }).FirstOrDefault();
                output = new GetProductSubGroup
                {
                    productSubGroup = subGroup
                };
            }
            return output;
        }


        public async Task CreateOrUpdateProductSubGroup(ProductSubGroupInputDto input)
        {
            if (input.Id != 0)
            {
                await UpdateProductSubGroupAsync(input);
            }
            else
            {
                await CreateProductSubGroupAsync(input);
            }
        }
        public virtual async Task CreateProductSubGroupAsync(ProductSubGroupInputDto input)
        {
            var subGroup = input.MapTo<ProductSubGroup>();
            var val = _productsubGroupRepository
              .GetAll().Where(p => p.ProductSubGroupCode == input.ProductSubGroupCode || p.ProductSubGroupName == input.ProductSubGroupName).FirstOrDefault();

            if (val == null)
            {
                await _productsubGroupRepository.InsertAsync(subGroup);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in SubGroup Name '" + input.ProductSubGroupName + "' or SubGroup Code '" + input.ProductSubGroupCode + "'...");
            }
        }
        public virtual async Task UpdateProductSubGroupAsync(ProductSubGroupInputDto input)
        {
            var subgroup = input.MapTo<ProductSubGroup>();
            subgroup.ProductSubGroupCode = input.ProductSubGroupCode;
            subgroup.ProductSubGroupName = input.ProductSubGroupName;
            subgroup.GroupId = input.GroupId;
            var val = _productsubGroupRepository
              .GetAll().Where(p => (p.ProductSubGroupCode == input.ProductSubGroupCode || p.ProductSubGroupName == input.ProductSubGroupName) && p.Id != input.Id).FirstOrDefault();
            if (val == null)
            {
                await _productsubGroupRepository.UpdateAsync(subgroup);
            }
            else
            {
                throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in SubGroup Name '" + input.ProductSubGroupName + "' or SubGroup Code '" + input.ProductSubGroupCode + "'...");
            }
        }
        public async Task DeleteProductSubGroup(EntityDto input)
        {
      
          await _productsubGroupRepository.DeleteAsync(input.Id);
                  
        }
    }

    public class datadto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class Select2Result
    {
        public datadto[] select2data { get; set; }
    }
}