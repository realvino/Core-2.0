using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TemporaryProductss.Dto;

namespace tibs.stem.TemporaryProductss
{
    public interface ITemporaryProductAppService : IApplicationService
    {
        Task<PagedResultDto<TemporaryProductList>> GetTemporaryProduct(GetTemporaryProductInput input);
        Task<GetTemporaryProduct> GetTemporaryProductForEdit(NullableIdDto input);
        Task CreateOrUpdateTemporaryProduct(TemporaryProductInput input);
        Task GetDeleteTemporaryProduct(EntityDto input);
        Task CreateTemporaryProductImage(TemporaryProductImageInput input);
        Task GetDeleteTemporaryProductImage(EntityDto input);
    }
}
