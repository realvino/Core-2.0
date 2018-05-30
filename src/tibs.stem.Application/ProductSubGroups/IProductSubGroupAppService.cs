using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.ProductSubGroups.Dto;

namespace tibs.stem.ProductSubGroups
{
   public interface IProductSubGroupAppService : IApplicationService
    {
        Task<PagedResultDto<ProductSubGroupListDto>> GetProductSubGroup(GetProductSubGroupInput input);
        Task<GetProductSubGroup> GetProductSubGroupForEdit(NullableIdDto input);
        Task CreateOrUpdateProductSubGroup(ProductSubGroupInputDto input);
        Task DeleteProductSubGroup(EntityDto input); 

    }
}
