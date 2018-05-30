using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.AttributeGroupDetails.Dto;
using tibs.stem.Dto;
using tibs.stem.ProductAttributeGroupss.Dto;

namespace tibs.stem.ProductAttributeGroupss
{
    public interface IAttributeGroupAppService : IApplicationService
    {
        ListResultDto<AttributeGroupListDto> GetAttributeGroup(GetAttributeGroupInput input);
        Task<GetAttributeGroup> GetAttributeGroupForEdit(EntityDto input);
        Task CreateOrUpdateAttributeGroup(CreateAttributeGroupInput input);
        ListResultDto<AttributeGroupDetailList> GetAttributeGroupDetail(GetAttributeGroupDetailInput input);
        Task CreateOrUpdateAttributeGroupDetail(AttributeGroupDetailInput input);
        Task GetDeleteAttributeGroupDetail(EntityDto input);
        Task GetDeleteAttributeGroup(EntityDto input);
        Task<FileDto> GetAttributeGroupToExcel();

    }
}
