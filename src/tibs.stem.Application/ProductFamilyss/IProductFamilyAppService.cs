using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;
using tibs.stem.ProductFamilyss.Dto;

namespace tibs.stem.ProductFamilyss
{
    public interface IProductFamilyAppService : IApplicationService
    {
        ListResultDto<ProductFamilyListDto> GetProductFamily(GetProductFamilyInput input);
        Task<GetProductFamily> GetProductFamilyForEdit(NullableIdDto input);
        Task CreateOrUpdateProductFamily(CreateProductFamilyInput input);
        Task GetDeleteProductFamily(EntityDto input);
        Task<FileDto> GetProductFamilyToExcel();
    }
}
