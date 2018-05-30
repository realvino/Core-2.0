using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.LineTypes.Dto;

namespace tibs.stem.LineTypes
{
   public interface ILineTypeAppService : IApplicationService
    {
        ListResultDto<LineTypeListDto> GetLineTypes(GetLineTypeInput input);
        Task<GetLineType> GetLineTypeForEdit(EntityDto input);
        Task CreateOrUpdateLineType(LineTypeInputDto input);
        Task DeleteLineType(EntityDto input);

    }
}
