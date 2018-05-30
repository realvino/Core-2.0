using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductGroups.Dto;

namespace tibs.stem.ProductGroups
{
    public interface IProductGroupAppService : IApplicationService
    {
        ListResultDto<ProductGroupListDto> GetProductGroup(GetProductGroupInput input);
        Task<GetProductGroup> GetProductGroupForEdit(EntityDto input);
        Task CreateOrUpdateProductGroup(ProductGroupInputDto input);
        Task GetDeleteProductGroup(EntityDto input);
        Task GetDeleteGroupDetail(EntityDto input);
        Task CreateOrUpdateProductGroupDetail(CreateProductGroupDetailInput input);
        Task<FileDto> GetProductGroupToExcel();

    }
}
