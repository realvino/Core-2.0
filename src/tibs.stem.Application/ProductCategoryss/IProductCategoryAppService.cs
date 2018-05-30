using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductCategoryss.Dto;

namespace tibs.stem.ProductCategoryss
{
    public interface IProductCategoryAppService : IApplicationService
    {
        ListResultDto<ProductCategoryList> GetProductCategory(GetProductCategoryInput input);
        Task<GetProductCategory> GetProductCategoryForEdit(NullableIdDto input);
        Task CreateOrUpdateProductCategory(ProductCategoryInput input);
        Task DeleteProductCategory(EntityDto input);

    }
}
