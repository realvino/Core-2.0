using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ProductTypes.Dto;

namespace tibs.stem.ProductTypes
{
    public interface IProductTypeAppService : IApplicationService
    {
        ListResultDto<ProductTypeListDto> GetProductType(ProductTypeInput input);
        Task<GetProductType> GetProductTypeForEdit(NullableIdDto input);
        Task CreateOrUpdateProductType(ProductTypeInputDto input);
       
        Task GetDeleteProductType(EntityDto input);
    }

}
