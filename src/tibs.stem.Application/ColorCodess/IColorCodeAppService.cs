using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.ColorCodess.Dto;

namespace tibs.stem.ColorCodess
{
   public interface IColorCodeAppService : IApplicationService
    {
        ListResultDto<ColorCodeList> GetColorcode(GetColorCodeListInput input);
        Task<GetColorCode> GetColorCodeForEdit(NullableIdDto input);
        Task CreateOrUpdateColorcode(CreateColorCodeInput input);

        Task DeleteColorcode(EntityDto input);
    }
    
}
