using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductAttributess.Dto;

namespace tibs.stem.ProductAttributess
{
   public interface IProductAttributeAppService : IApplicationService
    {
        ListResultDto<ProductAttributeListDto> GetProductAttribute(GetProductAttributeInput input);
        Task<GetProductAttribute> GetProductAttributeForEdit(NullableIdDto input);
        Task CreateOrUpdateProductAttribute(CreateProductAttributeInput input);
        Task GetDeleteProductAttribute(EntityDto input);
        Task<FileDto> GetProductAttributeToExcel();

        }
}
