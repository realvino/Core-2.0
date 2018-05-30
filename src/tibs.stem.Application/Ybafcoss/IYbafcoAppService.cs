using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Dto;

using tibs.stem.Ybafcoss.Dto;

namespace tibs.stem.Ybafcoss
{
  
   public interface IYbafcoAppService : IApplicationService
    {
        ListResultDto<YbafcoList> GetYbafco(GetYbafcoListInput input);
        Task<GetYbafco> GetYbafcoForEdit(NullableIdDto input);
        Task CreateOrUpdateYbafco(CreateYbafcoInput input);
        Task GetDeleteYbafco(EntityDto input);
        Task<FileDto> GetYbafcoToExcel();

    }
}
